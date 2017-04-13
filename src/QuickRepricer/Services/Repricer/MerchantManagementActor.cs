using Microsoft.Extensions.Configuration;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using QuickRepricer.Core.Services.Repricer.Messages;
using QuickRepricer.Core.Services.Repricer.Payloads;
using QuickRepricer.Services.Repricer.RepriceStrategies;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuickRepricer.Services.Repricer
{
    public class MerchantManagementActor
    {
        private const string STRATEGY_PREFIX = "QuickRepricer.Services.Repricer.RepriceStrategies.";

        /// <summary>
        /// ASIM -> merchant_sku -> [current_price, strategy] 
        /// </summary>
        private static Dictionary<string, Dictionary<string, KeyValuePair<double, string>>> _lookUpTable;
        private IConfigurationRoot _configuration;

        private NetMQActor actor;
      
        public MerchantManagementActor(IConfigurationRoot configuration)
        {
            _configuration = configuration;
            _lookUpTable = LoadAsimMerchantMap();
        }

        private Dictionary<string, Dictionary<string, KeyValuePair<double, string>>> LoadAsimMerchantMap()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory()
                   + "/Data", _configuration["Asim-Sku-Lookup-Fake"]);

            var maps = JsonConvert.DeserializeObject<List<AsimSkuMap>>(File.ReadAllText(filePath));


            var marchantMap = new Dictionary<string, Dictionary<string, KeyValuePair<double, string>>>();


            if (maps != null)
            {
                foreach (var mapItem in maps)
                {
                    if (marchantMap.ContainsKey(mapItem.Asim))
                    {
                        var dictionary = marchantMap[mapItem.Asim];

                        if (mapItem.MerchantMaps != null)
                        {
                            foreach (var map in mapItem.MerchantMaps)
                            {
                                if (dictionary.ContainsKey(map.MerchantSku))
                                {
                                    dictionary[map.MerchantSku] = new KeyValuePair<double, string>(map.CurrentPrice, map.Strategy);
                                }
                                else
                                {
                                    dictionary.Add(map.MerchantSku, new KeyValuePair<double, string>(map.CurrentPrice, map.Strategy));
                                }
                            }
                        }
                    }
                    else
                    {
                        var dictionary = new Dictionary<string, KeyValuePair<double, string>>();

                        if (mapItem.MerchantMaps != null)
                        {
                            foreach (var map in mapItem.MerchantMaps)
                            {
                                dictionary.Add(map.MerchantSku, new KeyValuePair<double, string>(map.CurrentPrice, map.Strategy));
                            }
                            marchantMap.Add(mapItem.Asim, dictionary);
                        }
                    }
                }
            }

            return marchantMap;
        }



        public void Start()
        {
            if (actor != null)
                return;

            actor = NetMQActor.Create(new MerchantManagementHandler());
        }

        public void Stop()
        {
            if (actor != null)
            {
                actor.Dispose();
                actor = null;
            }
        }
        
        public void SendLookupPayload(LookupMessage data)
        {
            if (actor == null)
                return;
            var message = new NetMQMessage();
            message.Append("Lookup");
            message.Append(JsonConvert.SerializeObject(data));
            actor.SendMultipartMessage(message);
        }

        public void SendPriceUpdatePayload(PriceUpdateMessage data)
        {
            if (actor == null)
                return;
            var message = new NetMQMessage();
            message.Append("UpdatePrice");
            message.Append(JsonConvert.SerializeObject(data));
            actor.SendMultipartMessage(message);
        }

        public List<MerchantMap> GetMechantLookupPayLoad()
        {
            return JsonConvert.DeserializeObject<List<MerchantMap>>(actor.ReceiveFrameString());
        }
        
        public class MerchantManagementHandler : IShimHandler
        {
            private PairSocket shim;
            private NetMQPoller poller;

            public void Initialise(object state)
            {
            }

            public void Run(PairSocket shim)
            {
                this.shim = shim;
                shim.ReceiveReady += OnShimReady;
                shim.SignalOK();

                poller = new NetMQPoller { shim };
                poller.Run();
            }

            private void OnShimReady(object sender, NetMQSocketEventArgs e)
            {
                string command = e.Socket.ReceiveFrameString();

                switch (command)
                {
                    case NetMQActor.EndShimMessage:
                        {
                            poller.Stop();
                            break;
                        }
                    case "Lookup":
                        {
                            string lookUpIdJson = e.Socket.ReceiveFrameString();
                            var lookUpMessage = JsonConvert.DeserializeObject<LookupMessage>(lookUpIdJson);

                            Dictionary<string, KeyValuePair<double, string>> results;
                            if (_lookUpTable.TryGetValue(lookUpMessage.ASIM, out results))
                            {
                                var message = new NetMQMessage();

                                var merchantMapList = new List<MerchantMap>();
                                foreach (var entry in results)
                                {
                                    merchantMapList.Add(new MerchantMap(entry.Key, entry.Value.Key, entry.Value.Value));
                                }
                                message.Append(JsonConvert.SerializeObject(merchantMapList));
                                shim.SendMultipartMessage(message);
                            }

                            break;
                        }
                    case "UpdatePrice":
                        {
                            string updatePriceMessageJson = e.Socket.ReceiveFrameString();
                            var updatePriceMessage = JsonConvert.DeserializeObject<PriceUpdateMessage>(updatePriceMessageJson);

                            var priceAndStrategy = _lookUpTable[updatePriceMessage.ASIM][updatePriceMessage.MerchantSku];
                            var newPriceAndStrategy = new KeyValuePair<double, string>(updatePriceMessage.CurrentPrice,
                                priceAndStrategy.Value);
                            _lookUpTable[updatePriceMessage.ASIM][updatePriceMessage.MerchantSku] = newPriceAndStrategy;

                            break;
                        }
                    case "UpdateStrategy":
                        {
                            string stategyUpdateJson = e.Socket.ReceiveFrameString();
                            var strategyUpdateMessage = JsonConvert.DeserializeObject<StrategyUpdateMessage>(stategyUpdateJson);

                            var priceAndStrategy = _lookUpTable[strategyUpdateMessage.ASIM][strategyUpdateMessage.MerchantSku];

                            var newPriceAndStrategy = new KeyValuePair<double, string>(priceAndStrategy.Key,
                                strategyUpdateMessage.Strategy);

                            // update strategy locally
                            _lookUpTable[strategyUpdateMessage.ASIM][strategyUpdateMessage.MerchantSku] = newPriceAndStrategy;

                            // claculate new price, update locally
                            var data = new RepricedMessage(strategyUpdateMessage.ASIM, strategyUpdateMessage.MerchantSku,
                                   newPriceAndStrategy.Key, newPriceAndStrategy.Key, newPriceAndStrategy.Value);


                            Type repriceStategyType = Type.GetType(string.Concat(STRATEGY_PREFIX + data.Strategy));
                            var repriceStrategy = Activator.CreateInstance(repriceStategyType) as RepriceStrategy;


                            if (repriceStrategy != null)
                            {
                                var repricerActor = new RepricerActor();
                                repricerActor.Start();
                                repricerActor.SendPayload(data, repriceStrategy);
                                double repicedPrice = repricerActor.GetPayLoad();
                                repricerActor.Stop();

                                // update locally
                                var updatedPriceAndStrategy = new KeyValuePair<double, string>(repicedPrice, newPriceAndStrategy.Value);
                                _lookUpTable[strategyUpdateMessage.ASIM][strategyUpdateMessage.MerchantSku] = updatedPriceAndStrategy;
                            }

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}
