using QuickRepricer.Integration.Workflows.Spec;
using QuickRepricer.Messages.Events;
using QuickRepricer.Messages.ReqRes;
using QuickRepricer.Messaging;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;
using System.Collections.Generic;
using System.Threading;

namespace QuickRepricer.Integration.Workflows
{
    public class SubscribeWorkflow : IWorkflow
    {
        private MessagingConfiguration _messageConfiguration;
        private SubscribeRequest _subscribeRequest;

        public SubscribeWorkflow(SubscribeRequest subscribeRequest, 
            MessagingConfiguration messageConfiguration)
        {
            _subscribeRequest = subscribeRequest;
            _messageConfiguration = messageConfiguration;
        }

        public void Run()
        {
            FetchListedItems();
            SaveListedItems();
            SendNotification();
        }

        private void FetchListedItems()
        {
            Thread.Sleep(1500);
        }

        private void SaveListedItems()
        {
            Thread.Sleep(1500);
        }

        private void SendNotification()
        {
            var items = new List<CatalogueChangeEvent>
            {
                new CatalogueChangeEvent(asim:"9493614a-c1ad-4706-94db-e795aba905e3", name: "Calc", price: 30),
                new CatalogueChangeEvent(asim:"eff9c43c-e82e-4972-b9ed-846cab4d4e9c", name: "Motor Bike", price: 35 ),
                new CatalogueChangeEvent(asim:"82add376-4d62-48bb-bdd2-a51a955ba738", name: "Hoover", price: 34)
            };

            var repricedEvent = new ChangeEventsCollection<CatalogueChangeEvent>(items);

            var queue = MessageQueueFactory.CreateOutbound("repriced-event", 
                MessagePattern.PublishSubscribe, _messageConfiguration);

            queue.Send(new Message
            {
                Body = repricedEvent
            });
        }
    }
}
