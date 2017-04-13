using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using QuickRepricer.Core.Services.Repricer;
using QuickRepricer.Messages.Events;
using QuickRepricer.Messaging;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;
using QuickRepricer.Services.Repricer.RepriceStrategies;
using QuickRepricer.Core.Services.Repricer.Messages;

namespace QuickRepricer.Services.Repricer
{
    public class RepricerServiceFake<S> : IRepricerService<S>
        where S : class
    {
        private const string STRATEGY_PREFIX = "QuickRepricer.Services.Repricer.RepriceStrategies.";

        private IConfigurationRoot _configuration;
        private MessagingConfiguration _messagingConfig;
        private CancellationTokenSource _cancellationTokenSource;

        private MerchantManagementActor _merchantManagementActor;

        private RemoteUpdateActor _remoteUpdateActor;

        private IMapper _mapper;

        public RepricerServiceFake(IConfigurationRoot configuration, 
            MessagingConfiguration messagingConfig)
        {
            _configuration = configuration;
            _messagingConfig = messagingConfig;
            _cancellationTokenSource = new CancellationTokenSource();
           
            _merchantManagementActor = new MerchantManagementActor(_configuration);

            _remoteUpdateActor = new RemoteUpdateActor(_messagingConfig);
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<S, RepricedData>();
            });
            _mapper = config.CreateMapper();
        }

            
        public void Start() 
        {
            // start management actor;
            _merchantManagementActor.Start();

            // remote start actor;
            _remoteUpdateActor.Start();

            var queue = MessageQueueFactory.CreateInbound("repriced", MessagePattern.PublishSubscribe, _messagingConfig);
            
            queue.Listen(x =>
            {
                if (x.BodyType == typeof(ChangeEventsCollection<S>)) 
                {
                    var repricedEvents = x.BodyAs<ChangeEventsCollection<S>>(); 

                    var changedItems = repricedEvents.Changes.Select(item =>
                                            _mapper.Map<RepricedData>(item))
                                            .ToList();
                    
                    foreach (var change in changedItems)
                    {
                        // look up locally
                        
                        _merchantManagementActor.SendLookupPayload(new LookupMessage(change.ASIM));
                        var merchantMapList = _merchantManagementActor.GetMechantLookupPayLoad();

                        if (merchantMapList != null)
                        {
                            foreach (var merchantMap in merchantMapList)
                            {
                                var data = new RepricedMessage(change.ASIM, merchantMap.MerchantSku,
                                    merchantMap.CurrentPrice, change.Price, merchantMap.Strategy);

                                // get repricing stategy
                                Type repriceStategyType = Type.GetType(string.Concat(STRATEGY_PREFIX + data.Strategy));
                                var repriceStrategy = Activator.CreateInstance(repriceStategyType) as RepriceStrategy;

                                if (repriceStrategy != null)
                                {
                                    // reprice
                                    var repricerActor = new RepricerActor();
                                    repricerActor.Start();
                                    repricerActor.SendPayload(data, repriceStrategy);
                                    double repicedPrice = repricerActor.GetPayLoad();
                                    repricerActor.Stop();
                                    
                                    // update locally
                                    var priceUpdateMessage = new PriceUpdateMessage(change.ASIM,
                                        merchantMap.MerchantSku, repicedPrice);

                                    _merchantManagementActor.SendPriceUpdatePayload(priceUpdateMessage);

                                    _remoteUpdateActor.SendUpdatePricePayload(priceUpdateMessage);

                                }
                            }
                        }
                    }
                }
            }, _cancellationTokenSource.Token);

        }
        
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _merchantManagementActor.Stop();
        }
    } 
}
