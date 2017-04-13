using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;
using Stripe;
using System.IO;
using System;
using QuickRepricer.Core.Models;
using QuickRepricer.Services;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Services.Subscription.Plans;
using QuickRepricer.Services.Subscription.EmailService;
using QuickRepricer.Services.Subscription;
using QuickRepricer.Services.Repricer;
using QuickRepricer.Messages.Events;
using QuickRepricer.Persistence;
using QuickRepricer.Core;
using QuickRepricer.Core.Services.Subscription.Plans;
using QuickRepricer.Core.Services.Email;
using QuickRepricer.Core.Services.Subscription;
using QuickRepricer.Core.Services.Subscription.EmailService;
using QuickRepricer.Core.Services.Repricer;
using QuickRepricer.Services.Email;

namespace QuickRepricer
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        private MessagingConfiguration GetMessageConfiguration()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Configs", Configuration["MessagingConfig"]);
            var root = JsonConvert.DeserializeObject<MessagingConfigurationRoot>(File.ReadAllText(filePath));
            
            return root.MessagingConfiguration;
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Configs/appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"Configs/appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        

        private void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            services.AddTransient<SeedData>();
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            // bespoke partial views;
            services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            StripeConfiguration.SetApiKey(Configuration["PaymentSettings:Stripe:SecretKey"]);
            services.AddSingleton(GetMessageConfiguration());
        }


        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);


            //services.AddScoped<IPlanService, PlanServiceFake>();
            services.AddScoped<IPlanService, PlanService>(); // use the live one, no the fake even in dev as we have doen it already

            //services.AddScoped<ISubscriptionService, SubscriptionServiceFake>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddTransient<IEmailSender, AuthMessageSenderFake>();
            services.AddTransient<ISmsSender, AuthMessageSenderFake>();

            services.Configure<AuthMessageSenderOptions>(Configuration); // what is this agian????

            services.AddTransient<ISubscriptionEmailService, SubscriptionEmailServiceFake>();

            // nedd to read the configuration for the fake repricer service;
            services.AddSingleton<IRepricerService<InvetoryChangeEvent>, RepricerServiceFake<InvetoryChangeEvent>>();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<AuthMessageSenderOptions>(Configuration); // what is this agian???? 

            services.AddTransient<ISubscriptionEmailService, SubscriptionEmailService>();
            services.AddSingleton<IRepricerService<InvetoryChangeEvent>, RepricerService<InvetoryChangeEvent>>();
        }


        #region old working version, may need it when deploying to azure!!

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddSingleton(Configuration);

        //    // Add framework services.
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //    services.AddTransient<SeedData>();


        //    services.AddScoped<IPlanService, PlanService>();
        //    services.AddScoped<ISubscriptionService, SubscriptionService>();

        //    services.AddIdentity<ApplicationUser, IdentityRole>()
        //        .AddEntityFrameworkStores<ApplicationDbContext>()
        //        .AddDefaultTokenProviders();

        //    services.AddMvc(options =>
        //    {
        //        options.Filters.Add(new RequireHttpsAttribute());
        //    });

        //    // bespoke partial views;
        //    services.Configure<RazorViewEngineOptions>(options =>
        //    {
        //        options.ViewLocationExpanders.Add(new ViewLocationExpander());
        //    });

        //    services.AddScoped<IPlanService, PlanService>();
        //    services.AddScoped<ISubscriptionService, SubscriptionService>();

        //    // Add application services.
        //    services.AddTransient<IEmailSender, AuthMessageSender>();
        //    services.AddTransient<ISmsSender, AuthMessageSender>();

        //    services.Configure<AuthMessageSenderOptions>(Configuration);

        //    services.AddTransient<ISubscriptionEmailService, SubscriptionEmailService>();

        //    StripeConfiguration.SetApiKey(Configuration["PaymentSettings:Stripe:SecretKey"]);

        //    services.AddSingleton(GetMessageConfiguration());
        //}
        #endregion


        // This method gets called by the runtime. Use this method to configure 
        //the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, ILoggerFactory loggerFactory, 
            IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsEnvironment("Development") )
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                //app.UseDeveloperExceptionPage(); // for better trace
               app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();
            
            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            });

            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Configuration["Authentication:Google:ClientId"],
                ClientSecret = Configuration["Authentication:Google:ClientSecret"]
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                );
            });

            var repricerService = serviceProvider.GetService<IRepricerService<InvetoryChangeEvent>>();
            repricerService.Start();

            var subscriptionSeedData = serviceProvider.GetService<SeedData>();
            subscriptionSeedData.EnsureSeedData().Wait();
            
        }
    }
}
