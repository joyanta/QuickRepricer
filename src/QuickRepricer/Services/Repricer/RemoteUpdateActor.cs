using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using QuickRepricer.Core.Services.Repricer.Messages;
using QuickRepricer.Messages.Commands;
using QuickRepricer.Messaging;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;

namespace QuickRepricer.Services.Repricer
{
    public class RemoteUpdateActor
    {
        private NetMQActor actor;
        
        private static IMessageQueue _outBoundQ; 

        public RemoteUpdateActor(MessagingConfiguration messagingConfiguration)
        {
            _outBoundQ = MessageQueueFactory.CreateOutbound("reprice",
             MessagePattern.FireAndForget, messagingConfiguration);
        }

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

        public void SendUpdatePricePayload(PriceUpdateMessage priceUpdateMessage)
        {
            if (actor == null)
                return;
            
            var message = new NetMQMessage();
            message.Append("UpdatePrice");
            message.Append(JsonConvert.SerializeObject(priceUpdateMessage));

            actor.SendMultipartMessage(message);
        }

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
                    case "UpdatePrice":
                        {
                            string priceUpdateJson = e.Socket.ReceiveFrameString();
                            //ASIM, MerchantSku, Price

                            var priceUpdateMessage = JsonConvert.DeserializeObject<PriceUpdateMessage>(priceUpdateJson);
                            
                            var repriceCommnad = new RepriceCommand(asim: priceUpdateMessage.ASIM,
                                merchantSku: priceUpdateMessage.MerchantSku, price: priceUpdateMessage.CurrentPrice);

                            _outBoundQ.Send(new Message
                            {
                                Body = repriceCommnad
                            });
                            
                            break;
                        }
                }
            }
        }
    }
}
