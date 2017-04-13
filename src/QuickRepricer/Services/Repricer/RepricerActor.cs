using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using QuickRepricer.Services.Repricer.Helpers;
using QuickRepricer.Services.Repricer.RepriceStrategies;
using QuickRepricer.Core.Services.Repricer.Messages;

namespace QuickRepricer.Services.Repricer
{
    public class RepricerActor
    {
        public class ShimHandler : IShimHandler
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

            public void OnShimReady(object sender, NetMQSocketEventArgs e)
            {
                string command = e.Socket.ReceiveFrameString();

                switch (command)
                {
                    case NetMQActor.EndShimMessage:
                        poller.Stop();
                        break;
                    case "Reprice":
                      
                        string repricedDataJson = e.Socket.ReceiveFrameString();
                        var repricedMessage = JsonConvert.DeserializeObject<RepricedMessage>(repricedDataJson);

                        string repriceContextJson = e.Socket.ReceiveFrameString();
            
                        var converter = new RepriceStrategyCreationConverter();
                        
                        var repriceStrategy = JsonConvert.DeserializeObject<RepriceStrategy>(repriceContextJson, converter);
                        
                        var newPrice = Reprice(repricedMessage, repriceStrategy);

                        shim.SendFrame(JsonConvert.SerializeObject(newPrice));

                        break;
                }
            }

            private double Reprice(RepricedMessage repricedMessage, RepriceStrategy repriceStrategy)
            {
                return repriceStrategy.Reprice(repricedMessage);
            }
        }

        private NetMQActor actor;

        public void Start()
        {
            if (actor != null)
                return;

            actor = NetMQActor.Create(new ShimHandler());
        }

        public void Stop()
        {
            if (actor != null)
            {
                actor.Dispose();
                actor = null;
            }
        }


        //ASIM -> {merchant_id + sku, current_price, algo}
        public void SendPayload(RepricedMessage repricedMessage, RepriceStrategy repriceStrategy)
        {
            if (actor == null)
                return;
 
            var message = new NetMQMessage();
            message.Append("Reprice");
            message.Append(JsonConvert.SerializeObject(repricedMessage));
            message.Append(JsonConvert.SerializeObject(repriceStrategy));
            actor.SendMultipartMessage(message);
        }

        public double GetPayLoad()
        {
            return JsonConvert.DeserializeObject<double>(actor.ReceiveFrameString());
        }
    }

}
