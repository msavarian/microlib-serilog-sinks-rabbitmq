using RabbitMQ.Client;
using System;
using System.Text;

namespace Micro.Serilog.Sinks.RabbitMQ
{
    public class RabbitMQClient : IDisposable
    {
        ConnectionFactory _factory;
        IConnection _connection;
        IModel _model;
        private readonly SinkConfiguration _sinkConfiguration;

        public RabbitMQClient(SinkConfiguration sinkConfiguration)
        {
            _sinkConfiguration = sinkConfiguration;
            Init();
        }
        public void Init()
        {
            _factory = new ConnectionFactory
            {
                HostName = _sinkConfiguration.RabbitMqHostName,
                UserName = _sinkConfiguration.RabbitMqUsername,
                Password = _sinkConfiguration.RabbitMqPassword
            };

            _connection = _factory.CreateConnection(_sinkConfiguration.ClientName);
            _model = _connection.CreateModel();

            try
            {
                _model.QueueDeclare(_sinkConfiguration.RabbitMqQueueName, true, false, false, null);
                _model.ExchangeDeclare(_sinkConfiguration.RabbitMqExchange, _sinkConfiguration.RabbitMqExchangeType, true, false, null);
                _model.QueueBind(_sinkConfiguration.RabbitMqQueueName, _sinkConfiguration.RabbitMqExchange, _sinkConfiguration.RabbitMqRouteKey);
            }
            catch (Exception ex)
            {
            }

        }

        public void SendMessage(string message)
        {
            var properties = _model.CreateBasicProperties();
            properties.Persistent = true;
            _model.BasicPublish(_sinkConfiguration.RabbitMqExchange, _sinkConfiguration.RabbitMqRouteKey, properties, Encoding.ASCII.GetBytes(message));
        }


        public void Dispose()
        {
            _model?.Dispose();
            _connection?.Dispose();
        }

    }
}
