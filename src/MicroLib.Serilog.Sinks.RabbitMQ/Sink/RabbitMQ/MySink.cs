using System;
using Serilog.Core;
using Serilog.Events;

namespace Micro.Serilog.Sinks.RabbitMQ
{
    public class MySink : ILogEventSink, IDisposable
    {
        private readonly IFormatProvider _formatProvider;
        private readonly RabbitMQClient _rabbitMQClient;

        public MySink(SinkConfiguration sinkConfiguration, IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
            _rabbitMQClient = new RabbitMQClient(sinkConfiguration);
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            _rabbitMQClient.SendMessage(message);
        }

        public void Dispose()
        {
            _rabbitMQClient.Dispose();
        }
    }
}
