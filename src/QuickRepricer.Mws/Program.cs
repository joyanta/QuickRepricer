using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Catalogue.Persistance;

namespace QuickRepricer.Mws
{
    public class Program
    {
        private static IConfigurationRoot _Configuration;
        private static MessagingConfiguration _MessageConfiguration;
        private static QueueListener _QueueListener;

        private static void BuildConifg()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        
            if (configurationBuilder.Build()["Environment"] == "Development")
            {
                configurationBuilder.AddUserSecrets();
            }

            configurationBuilder.AddEnvironmentVariables();

            _Configuration = configurationBuilder.Build();
        }

        private static void BuildMessageConfiguration()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _Configuration["MessagingConfig"]);
            var root = JsonConvert.DeserializeObject<MessagingConfigurationRoot>(File.ReadAllText(filePath));
            _MessageConfiguration = root.MessagingConfiguration;
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            BuildConifg();

            BuildMessageConfiguration();

            services.AddSingleton(_Configuration);

            services.AddOptions();

            services.Configure<RethinkDbOptions>(_Configuration.GetSection("RethinkDbDev"));


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

            //var fetcher = new MwsFetcher(services);
            //fetcher.Start();
            
            _QueueListener = new QueueListener(_Configuration, _MessageConfiguration, services);
            
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
