using System;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using QuickRepricer.Messaging.Spec;
using QuickRepricer.Messaging.Extensions;
using QuickRepricer.Messaging.Configuration;

namespace QuickRepricer.Messaging.Impl.ZeroMq
{
    public class ZeroMqMessageQueue : MessageQueueBase
    {
        private NetMQSocket _socket;

        public ZeroMqMessageQueue(MessagingConfiguration config) : base(config)
        {
        }

        public override string Name
        {
            get { return "ZeroMQ"; }
        }

        public override void InitialiseOutbound(string name, MessagePattern pattern, bool isTemporary)
        {
            Initialise(Direction.Outbound, name, pattern, isTemporary);
         
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    _socket = new RequestSocket();
                    _socket.Connect(Address);
                    break;

                case MessagePattern.FireAndForget:
                    _socket = new PushSocket();
                    _socket.Connect(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    _socket = new PublisherSocket();
                    _socket.Bind(Address);
                    break;
            }
        }

        public override void InitialiseInbound(string name, MessagePattern pattern, bool isTemporary)
        {
            Initialise(Direction.Inbound, name, pattern, isTemporary);
          
            switch (Pattern)
            {
                case MessagePattern.RequestResponse:
                    _socket = new ResponseSocket();
                    _socket.Bind(Address);
                    break;

                case MessagePattern.FireAndForget:
                    _socket = new PullSocket();
                    _socket.Bind(Address);
                    break;

                case MessagePattern.PublishSubscribe:
                    _socket = new SubscriberSocket();
                    _socket.Connect(Address);
                    ((SubscriberSocket)_socket).Subscribe("", Encoding.UTF8);
                    break;
            }
        }

        public override IMessageQueue GetResponseQueue()
        {
            if (!(Pattern == MessagePattern.RequestResponse && Direction == Direction.Outbound))
                throw new InvalidOperationException("Cannot get a response queue except for outbound request-response");

            return this;
        }


        public override IMessageQueue GetReplyQueue(Message message)
        {
            if (!(Pattern == MessagePattern.RequestResponse && Direction == Direction.Inbound))
                throw new InvalidOperationException("Cannot get a reply queue except for inbound request-response");

            return this;
        }

        public override void Send(Message message)
        {
            var json = message.ToJsonString();
            _socket.SendFrame(json, false);
        }

        public override void Receive(
            Action<Message> onMessageReceived,
            bool processAsync, 
            int maximumWaitMilliseconds = 0)
        {
            string inbound = string.Empty;
            if (maximumWaitMilliseconds > 0)
            {
             
                _socket.TryReceiveFrameString(
                        TimeSpan.FromMilliseconds(maximumWaitMilliseconds),
                        Encoding.UTF8, out inbound);
            }
            else
            {
              
                inbound = _socket.ReceiveFrameString(Encoding.UTF8);
            }

            var message = Message.FromJson(inbound);

            //we can only process ZMQ async if the pattern supports it - we can't call Rec
            //twice on a REP socket without the Send in between:

            if (processAsync && Pattern != MessagePattern.RequestResponse)
            {
                Task.Factory.StartNew(() => onMessageReceived(message));
            }
            else
            {
                onMessageReceived(message);
            }
        }


        public override void DeleteQueue()
        {
            //do nothing - there is no queue
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _socket != null)
            {
                _socket.Dispose();
            }
        }

        protected override string CreateResponseQueue()
        {
            throw new InvalidOperationException("Cannot create a response queue for ZeroMQ");
        }
    }
}
