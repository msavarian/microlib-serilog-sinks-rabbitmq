using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sample_AspNetCore.Controllers
{
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
    }
}