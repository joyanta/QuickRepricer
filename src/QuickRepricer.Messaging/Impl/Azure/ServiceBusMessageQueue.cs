using System;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;

namespace QuickRepricer.Messaging.Impl.Azure
{
    public class ServiceBusMessageQueue : MessageQueueBase
    {
        public ServiceBusMessageQueue(MessagingConfiguration config) : base(config)
        {
        }

        public override string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void DeleteQueue()
        {
            throw new NotImplementedException();
        }

        public override void InitialiseInbound(string name, MessagePattern pattern, bool isTemporary)
        {
            throw new NotImplementedException();
        }

        public override void InitialiseOutbound(string name, MessagePattern pattern, bool isTemporary)
        {
            throw new NotImplementedException();
        }

        public override void Receive(Action<Message> onMessageReceived, bool processAsync, int maximumWaitMilliseconds = 0)
        {
            throw new NotImplementedException();
        }

        public override void Send(Message message)
        {
            throw new NotImplementedException();
        }

        protected override string CreateResponseQueue()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}
