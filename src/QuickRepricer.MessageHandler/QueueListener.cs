using Microsoft.Extensions.Configuration;
using QuickRepricer.Integration.Workflows;
using QuickRepricer.Integration.Workflows.Spec;
using QuickRepricer.Messages.Commands;
using QuickRepricer.Messages.Events;
using QuickRepricer.Messages.ReqRes;
using QuickRepricer.Messaging;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;
using System;
using System.Threading;

namespace QuickRepricer.MessageHandler
{
    public class QueueListener
    {
        private CancellationTokenSource _cancellationTokenSource;

        private IConfigurationRoot _configuration;

        private static MessagingConfiguration _messageConfiguration;

        public QueueListener(IConfigurationRoot configuration, MessagingConfiguration messageConfiguration)
        {
            _configuration = configuration;
            _messageConfiguration = messageConfiguration;
        }

        public void Start(string queueName)
        {
            Console.WriteLine("QueueListener starting...");

            //query to warm up the EF:
            // accesss the database
            if (string.IsNullOrEmpty(queueName))
            {
                queueName = _configuration["listenOnQueueName"];

                throw new ArgumentException(
                     "'listenOnQueueName:[queueName]' must be specified as an sppSetting or command line argument");
            }

            Console.WriteLine("Starting with queueName: {0}", queueName);

            _cancellationTokenSource = new CancellationTokenSource();
            switch (queueName)
            {
                case "unsubscribe":
                    {
                        StartListening("unsubscribe", MessagePattern.FireAndForget);
                        break;
                    }
                case "subscribe":
                    {
                        StartListening("subscribe", MessagePattern.RequestResponse);
                        break;
                    }
                case "reprice":
                    {
                        StartListening("reprice", MessagePattern.FireAndForget);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }


        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private void StartListening(string name, MessagePattern pattern)
        {
            var queue = MessageQueueFactory.CreateInbound(name, pattern, _messageConfiguration);
            Console.WriteLine("Listening on: {0}", queue.Address);
            queue.Listen(x =>
            {
                if (x.BodyType == typeof(SubscribeRequest))
                {
                    Subscribe(x.BodyAs<SubscribeRequest>(), x, queue);
                }
                else if (x.BodyType == typeof(UnsubscribeCommand))
                {
                    Unsubscribe(x.BodyAs<UnsubscribeCommand>());
                }
                else if (x.BodyType == typeof(RepriceCommand))
                {
                    var repriceCommnad = x.BodyAs<RepriceCommand>();
                    Console.WriteLine("recived  {0}", typeof(RepriceCommand).FullName);

                    Reprice(repriceCommnad);
                }
            }, _cancellationTokenSource.Token);

        }

        private void Reprice(RepriceCommand repriceCommnad)
        {
            Console.WriteLine("Repricing sku: {0} to price: {1}: at: {2}",
              repriceCommnad.MerchantSku, repriceCommnad.Price, DateTime.Now.TimeOfDay);

            var workflow = new RepriceWorkflow();

            workflow.Run();
        }

        private static void Subscribe(SubscribeRequest subscribeRequest,
                  Message requestMessage, IMessageQueue requestQueue)
        {
            Console.WriteLine("Starting the set up for: {0} with {1} at: {2}",
                 subscribeRequest.UserName, subscribeRequest.SellerId, 
                 DateTime.Now.TimeOfDay);

            var responseBody = new SubscribeStartResponse(status: "OK");

            var responseQueue = requestQueue.GetReplyQueue(requestMessage);
            responseQueue.Send(new Message
            {
                Body = responseBody
            });

            var workflow = new SubscribeWorkflow(subscribeRequest, _messageConfiguration);
            workflow.Run();
        }

        private static void Unsubscribe(UnsubscribeCommand unsubscribeCommand)
        {
            Console.WriteLine("Starting unsubscribe for user: {0} at: {1}",
                unsubscribeCommand.UserName, DateTime.Now.TimeOfDay);

            var workflow = new UnsubscribeWorkflow(unsubscribeCommand.UserName);

             workflow.Run();

            Console.WriteLine("Unsubscribe complete for customerid: {0}, at: {1}", 
                unsubscribeCommand.UserName, DateTime.Now.TimeOfDay);
        }
        
        //private void StartListening<TWorkflow>(string name, MessagePattern pattern)
        //    where TWorkflow : IWorkflow, new()
        //{
        //}
    }
}
