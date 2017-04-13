using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuickRepricer.Catalogue.Persistance;
using QuickRepricer.Messaging;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuickRepricer.Publisher
{
    public class Publisher<S, D>
        where S : class
        where D : class
    {
        private CancellationTokenSource _cancellationTokenSource;

        private IServiceProvider _serviceProvider;

        private IMapper _mapper;

        public Publisher(IServiceCollection services, MapperConfiguration config)
        {
            _serviceProvider = services.BuildServiceProvider();
            _mapper = config.CreateMapper();
        }
        
        public void Start(string queueName)
        {
            Console.WriteLine("Publisher starting...");
       
            Console.WriteLine("Starting with queueName: {0}", queueName);

            _cancellationTokenSource = new CancellationTokenSource();
            switch (queueName)
            {
                case "repriced-event":
                    {
                        Task.Factory.StartNew(async () =>
                                {
                                    await StartSendingUpdatesToQueue(typeof(S).Name, queueName);
                                }, _cancellationTokenSource.Token);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }


        private async Task StartSendingUpdatesToQueue(string table, string queueName)  
        {
            var messageConfig = _serviceProvider.GetRequiredService<MessagingConfiguration>();
            var queue = MessageQueueFactory.CreateOutbound(queueName,
                  MessagePattern.PublishSubscribe, messageConfig);

            var rethinkDbConnectionFactory = _serviceProvider.GetService<IRethinkDbConnectionFactory>();
            
            var feed = await rethinkDbConnectionFactory.GetFeed<S>(table);

            while (await feed.MoveNextAsync())
            {
                var newVal = feed.Current.NewValue;

                


                //var mappedVal = _mapper.Map<D>(newVal);

                //var changeEvents = new ChangeEventsCollection<D>(new List<D> { mappedVal });

                //queue.Send(new Message
                //{
                //    Body = changeEvents
                //});
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
