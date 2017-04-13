using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using QuickRepricer.Messaging.Configuration;
using Newtonsoft.Json;

namespace QuickRepricer.MessageHandler
{
    public class Program
    { 
        private static MessagingConfiguration _MessageConfiguration;

        private static QueueListener _QueueListener;
        public static IConfigurationRoot Configuration { get; private set; }

      
        private static void BuildConifg()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            Configuration = configurationBuilder.Build();
        }

        private static void BuildMessageConfiguration()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), Configuration["MessagingConfig"]);
            var root = JsonConvert.DeserializeObject<MessagingConfigurationRoot>(File.ReadAllText(filePath));
            _MessageConfiguration = root.MessagingConfiguration;
        }
        
        public static void Main(string[] args)
        {
            BuildConifg();
            BuildMessageConfiguration();
            
            _QueueListener = new QueueListener(Configuration, _MessageConfiguration);

            Console.WriteLine("Starting.");
            var command = string.Empty;

            while (command != "x")
            {
                _QueueListener.Start(args[0]);

                Console.WriteLine("Running. Any key to pause, x to exit");
                command = Console.ReadLine();
                _QueueListener.Stop();
                if (command != "x")
                {
                    Console.WriteLine("Paused. Any key to resume, x to exit");
                    command = Console.ReadLine();
                }
            }
        }
    }
}
