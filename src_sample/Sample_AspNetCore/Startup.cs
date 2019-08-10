using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Serilog.Sinks.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Sample_AspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // config serilog
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                   .WriteTo.MySink(new SinkConfiguration
                   {
                       ClientName = "Sample Console App",
                       RabbitMqHostName = "127.0.0.1",
                       RabbitMqPort = 15672,
                       RabbitMqUsername = "guest",
                       RabbitMqPassword = "guest",

                       RabbitMqExchange = "MicroLoggerExchangeName",
                       RabbitMqExchangeType = RabbitMQ.Client.ExchangeType.Direct,
                       RabbitMqRouteKey = "MicroLoggerRoutKeyName"
                   })
                   .CreateLogger();

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog(logger);
            services.AddSingleton<ILoggerFactory>(loggerFactory);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
