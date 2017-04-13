using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuickRepricer.Messaging.Configuration
{
    public class Queue
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Property
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class MessageQueue
    {
        public string Name { get; set; }
        public List<Queue> Queues { get; set; }
        public string Type { get; set; }
        public List<Property> Properties { get; set; }
    }

    [JsonObject("Message")]
    public class MessageItem
    {
        public string Name { get; set; }
        public string MessageQueueName { get; set; }
    }

    public class MessagingConfiguration
    {
        public string DefaultMessageQueueName { get; set; }
        public List<MessageQueue> MessageQueues { get; set; }
        public List<MessageItem> Messages { get; set; }
    }


    public class MessagingConfigurationRoot
    {
        public MessagingConfiguration MessagingConfiguration { get; set; }
    }
}
