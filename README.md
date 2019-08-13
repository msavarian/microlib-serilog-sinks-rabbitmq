# micro-serilog-sinks-rabbitmq
It's a very simple ***serilog sink for rabbitmq***, but **it is working** :)


# How to use

### In .net core *console* app
1. Install the nuget package
```
Install-Package Micro.Serilog.Sinks.RabbitMQ -Version 1.0.1
```
2. **In program.cs class file**
```
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
            ...
            ...
            ...

```
---
### In *aspnetcore* app
1. Install the nuget package
```
Install-Package Micro.Serilog.Sinks.RabbitMQ -Version 1.0.1
```
2. **In startup.cs class file**
```
public class Startup
    {        
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

                       RabbitMqExchange = "MicroLogger-ExchangeName",
                       RabbitMqExchangeType = RabbitMQ.Client.ExchangeType.Direct,
                       RabbitMqRouteKey = "MicroLogger-RoutKeyName",
                       RabbitMqQueueName = "MicroLogger-QueueName"
                   })
                   .CreateLogger();

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog(logger);
            services.AddSingleton<ILoggerFactory>(loggerFactory);
            ...
            ...
            ...
        }
```

3. **And in the Controllers or anywhere that you want to add logs (send logs to rabbitmq)**
```
public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }
        public string Index()
        {
            var ErrorMessage = new { msgId = 1, Description = "This is test error message" };
            var WarnMessage = new { msgId = 2, Description = "This is test warn message" };

            logger.Log(LogLevel.Critical,"Hello RabbitMQ, I'm from Serilog (aspnetcore app)!");
            logger.LogInformation("Hello RabbitMQ, I'm from Serilog (aspnetcore app)!");
            logger.LogError("Sample Error Msg {Message}", ErrorMessage);
            logger.LogWarning("Sample Error Msg {Message}", WarnMessage);

            return "Some sample logs sent to RabbitMQ";
        }
```
---
### Follow Samples in this repository (source code).
