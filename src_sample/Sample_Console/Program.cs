using Serilog;
using System;
using Micro.Serilog.Sinks.RabbitMQ;

namespace Sample_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // How To use
            var log = new LoggerConfiguration()
                   .MinimumLevel.Information()
                   .WriteTo.MySink(new SinkConfiguration
                   {
                       ClientName = "Sample Console App",
                       RabbitMqHostName = "127.0.0.1",
                       RabbitMqPort = 15672,
                       RabbitMqUsername = "guest",
                       RabbitMqPassword = "guest",

                       RabbitMqExchange = "MicroLogger-ExchangeName",
                       RabbitMqExchangeType = RabbitMQ.Client.ExchangeType.Direct,
                       RabbitMqRouteKey = "MicroLogger-RoutKeyName",
                       RabbitMqQueueName = "MicroLogger-QueueName"
                   })
                   .CreateLogger();

            var ErrorMessage = new { msgId = 1, Description = "This is test error message" };
            var WarnMessage = new { msgId = 2, Description = "This is test warn message" };

            log.Information("Hello RabbitMQ, I'm from Serilog (console app)!");
            log.Error("Sample Error Msg {Message}" ,ErrorMessage);
            log.Warning("Sample Error Msg {Message}", WarnMessage);


            Console.WriteLine("Press any key for exit...");
            Console.ReadLine();
        }
    }
}
