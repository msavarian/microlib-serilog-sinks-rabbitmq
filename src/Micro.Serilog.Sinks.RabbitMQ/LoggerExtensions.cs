using Serilog;
using Serilog.Configuration;
using System;

namespace Micro.Serilog.Sinks.RabbitMQ
{
    public static class LoggerExtensions
    {
        public static LoggerConfiguration MySink(
                     this LoggerSinkConfiguration loggerConfiguration,
                     SinkConfiguration sinkConfiguration,
                     IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new MySink(sinkConfiguration, formatProvider));
        }
    }
}
