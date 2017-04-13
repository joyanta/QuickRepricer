using Microsoft.Extensions.Configuration;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuickRepricer.Messaging.Impl
{
    public abstract class MessageQueueBase : IMessageQueue
    {
        private MessagingConfiguration _config;

        protected int _pollingInterval = 250;
        protected bool _isListening;
        internal Direction Direction { get; set; }

        public abstract string Name { get; }

        public string Address { get; protected set; }

        public bool IsTemporary { get; protected set; }

        public MessagePattern Pattern { get; protected set; }

        public Dictionary<string, string> Properties { get; protected set; }

        
        public MessageQueueBase(MessagingConfiguration config)
        {
            _config = config;
        }

        public virtual string GetAddress(string name)
        {
            var messageQueue = _config.MessageQueues.Single(x => x.Name == Name);
            var queue = messageQueue.Queues.SingleOrDefault(x => x.Name == name);
            return queue == null ? name : queue.Address;
        }

        public virtual IMessageQueue GetResponseQueue()
        {
            if (!(Pattern == MessagePattern.RequestResponse && Direction == Direction.Outbound))
                throw new InvalidOperationException("Cannot get a response queue except for outbound request-response");

            var address = CreateResponseQueue();
            return MessageQueueFactory.CreateInbound(address, MessagePattern.RequestResponse, _config, true, this);
        }

        public virtual IMessageQueue GetReplyQueue(Message message)
        {
            if (!(Pattern == MessagePattern.RequestResponse && Direction == Direction.Inbound))
                throw new InvalidOperationException("Cannot get a reply queue except for inbound request-response");

            return MessageQueueFactory.CreateOutbound(message.ResponseAddress, MessagePattern.RequestResponse, _config, true, this);
        }

        protected abstract string CreateResponseQueue();

        public abstract void InitialiseOutbound(string name, MessagePattern pattern, bool isTemporary);

        public abstract void InitialiseInbound(string name, MessagePattern pattern, bool isTemporary);

        public abstract void Send(Message message);

        public abstract void DeleteQueue();

        public virtual void Listen(Action<Message> onMessageReceived, CancellationToken cancellationToken)
        {
            if (_isListening)
                return;

            Task.Factory.StartNew(() => ListenInternal(onMessageReceived, cancellationToken), 
                cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        protected virtual void ListenInternal(Action<Message> onMessageReceived, CancellationToken cancellationToken)
        {
            _isListening = true;
            while (_isListening)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _isListening = false;
                    cancellationToken.ThrowIfCancellationRequested();
                }
                try
                {
                    Receive(onMessageReceived, true);
                    Thread.Sleep(_pollingInterval);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex);
                }

            }
        }

        public virtual void Receive(Action<Message> onMessageReceived, int maximumWaitMilliseconds = 0)
        {
            Receive(onMessageReceived, false, maximumWaitMilliseconds);
        }

        public abstract void Receive(Action<Message> onMessageReceived, bool processAsync, 
            int maximumWaitMilliseconds = 0);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        protected void Initialise(Direction direction, string name, MessagePattern pattern, bool isTemporary)
        {
            Direction = direction;
            Pattern = pattern;
            IsTemporary = isTemporary;
            Address = GetAddress(name);
            Properties = new Dictionary<string, string>();
            var messageQueue = _config.MessageQueues.Single(x => x.Name == Name);
            if (messageQueue.Properties != null)
            {
                foreach (var property in messageQueue.Properties)
                {
                    Properties.Add(property.Name, property.Value);
                }
            }
        }

        protected string RequirePropertyValue(string name)
        {
            var value = GetPropertyValue(name);
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException(string.Format("Property named: {0} is required for: {1}", name, Pattern));
            }
            return value;
        }

        protected string GetPropertyValue(string name)
        {
            string value = null;
            if (Properties != null && Properties.Count(x => x.Key == name) == 1)
            {
                value = Properties[name];
            }
            return value;
        }
    }
}
