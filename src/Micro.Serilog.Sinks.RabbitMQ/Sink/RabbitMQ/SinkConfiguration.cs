using RabbitMQ.Client;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Raw;
using System;
using System.Collections.Generic;
using System.Text;

namespace Micro.Serilog.Sinks.RabbitMQ
{
    public class SinkConfiguration
    {
        public string ClientName { get; set; } = string.Empty;

        public string RabbitMqHostName { get; set; } = string.Empty;
        public int RabbitMqPort { get; set; }

        public string RabbitMqUsername { get; set; } = string.Empty;
        public string RabbitMqPassword { get; set; } = string.Empty;

        public string RabbitMqExchange { get; set; } = string.Empty;
        public string RabbitMqExchangeType { get; set; } = string.Empty;
        public string RabbitMqRouteKey { get; set; } = string.Empty;
        public string RabbitMqQueueName { get { return RabbitMqRouteKey + "Queue"; } }
    }
}
