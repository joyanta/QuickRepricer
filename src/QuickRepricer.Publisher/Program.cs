using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messages.Events;
using QuickRepricer.Catalogue.Persistance;
using QuickRepricer.Catalogue.Core.Model;

namespace QuickRepricer.Publisher
{
    public class Program
    {
        private static IConfigurationRoot BuildConifg()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return configurationBuilder.Build();
        }

        private static MessagingConfiguration BuildMessageConfiguration(IServiceCollection services, IConfigurationRoot config)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), config["MessagingConfig"]);
            var root = JsonConvert.DeserializeObject<MessagingConfigurationRoot>(File.ReadAllText(filePath));
            return root.MessagingConfiguration;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var config = BuildConifg();
            services.AddSingleton(config);

            var messagingConfiguration = BuildMessageConfiguration(services, config);
            services.AddSingleton(messagingConfiguration);

            services.AddOptions();
            services.Configure<RethinkDbOptions>(config.GetSection("RethinkDbDev"));
            
            services.AddSingleton<IRethinkDbConnectionFactory, RethinkDbConnectionFactory>();
            services.AddSingleton<IDbContext, RethinkDbStore>();
        }

        private static void SeedDatabase(IServiceCollection services)
        {
            var dbStore = services.BuildServiceProvider().GetService<IDbContext>();
            dbStore.InitDatabase();
        }

        public static void Main(string[] args)
        {

            var services = new ServiceCollection();
            ConfigureServices(services);

            SeedDatabase(services);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Inventory, ChangeEventsCollection<InvetoryChangeEvent>>();
            });

            var publisher = new Publisher<Inventory, ChangeEventsCollection<InvetoryChangeEvent>>(services, config);

            Console.WriteLine("Starting.");

            var command = string.Empty;
            while (command != "x")
            {
                publisher.Start(args[0]);

                Console.WriteLine("Running. Any key to pause, x to exit");
                command = Console.ReadLine();
                publisher.Stop();

                if (command != "x")
                {
                    Console.WriteLine("Paused. Any key to resume, x to exit");
                    command = Console.ReadLine();
                }
            }
        }
    }
}
