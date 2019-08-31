using Serilog;
using System;
using Micro.Serilog.Sinks.RabbitMQ;
using Microsoft.Extensions.Logging;

namespace Sample_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // How To use
            var logger = new LoggerConfiguration()
                   .MinimumLevel.Information()
                   .WriteTo.MySink(new SinkConfiguration
                   {
                       ClientName = "Sample Console App",
                       RabbitMqHostName = "127.0.0.1",
                       RabbitMqPort = 15672,
                       RabbitMqUsername = "guest",
                       RabbitMqPassword = "guest",

                       RabbitMqExchangeName = "MicroLogger-ExchangeName",
                       RabbitMqExchangeType = MicroLib.RabbitMQ.Client.Helper.Standard.Model.ExchangeType.Direct,
                       RabbitMqRouteKey = "MicroLogger-RoutKeyName",
                       RabbitMqQueueName = "MicroLogger-QueueName"
                   })
                   .CreateLogger();

            //var loggerFactory = new LoggerFactory();
            //loggerFactory.AddSerilog(logger);

            var ErrorMessage = new { msgId = 1, Description = "This is test error message" };
            var WarnMessage = new { msgId = 2, Description = "This is test warn message" };

            logger.Information("Hello RabbitMQ, I'm from Serilog (console app)!");
            logger.Error("Sample Error Msg {Message}" ,ErrorMessage);
            logger.Warning("Sample Error Msg {Message}", WarnMessage);


            Console.WriteLine("Press any key for exit...");
            Console.ReadKey();
        }
    }
}
