using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickRepricer.Messaging
{
    public static class MessageQueueFactory
    {
        private static Dictionary<string, IMessageQueue> _Queues 
            = new Dictionary<string, IMessageQueue>();

        public static IMessageQueue CreateInbound(string name, MessagePattern pattern,
            MessagingConfiguration messageConfig, bool isTemporary = false, 
            IMessageQueue originator = null)
        {
            var key = string.Format("{0}:{1}:{2}", Direction.Inbound, name, pattern);
            if (_Queues.ContainsKey(key))
                return _Queues[key];

            var queue = Create(name, messageConfig, originator);
            queue.InitialiseInbound(name, pattern, isTemporary);
            _Queues[key] = queue;
            return _Queues[key];
        }

        public static IMessageQueue CreateOutbound(string name, MessagePattern pattern,
            MessagingConfiguration messageConfig, bool isTemporary = false, 
            IMessageQueue originator = null)
        {
            var key = string.Format("{0}:{1}:{2}", Direction.Outbound, name, pattern);
            if (_Queues.ContainsKey(key))
                return _Queues[key];

            var queue = Create(name, messageConfig, originator);
            queue.InitialiseOutbound(name, pattern, isTemporary);
            _Queues[key] = queue;
            return _Queues[key];
        }

        public static void Delete(IMessageQueue queue)
        {
            queue.DeleteQueue();
            var queueEntries = _Queues.Where(x => x.Value.Name == queue.Name).ToList();
            queueEntries.ForEach(x =>
            {
                x.Value.Dispose();
                _Queues.Remove(x.Key);
            });
        }


        private static IMessageQueue Create(string name,
            MessagingConfiguration messageConfig, 
            IMessageQueue originator = null)
        {
            //for response/reply queues, use the same type as the originator:
            if (originator != null)
            {
                return Activator.CreateInstance(originator.GetType(), messageConfig) as IMessageQueue;
            }
            
            var messageItem = messageConfig.Messages.SingleOrDefault(x => x.Name == name);
            var queueName = messageItem != null
                                ? messageItem.MessageQueueName
                                : messageConfig.DefaultMessageQueueName;
            var queueType = messageConfig.MessageQueues.Single(x => x.Name == queueName).Type;
            var type = Type.GetType(queueType);
            return Activator.CreateInstance(type, messageConfig) as IMessageQueue;
        }
    }
}
