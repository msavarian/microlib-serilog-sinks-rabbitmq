using MicroLib.RabbitMQ.Client.Helper.Standard.Functions;
using MicroLib.RabbitMQ.Client.Helper.Standard.Model;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Micro.Serilog.Sinks.RabbitMQ
{
    public class RabbitMQClient : IDisposable
    {
        private readonly SinkConfiguration _sinkConfiguration;
        private readonly RabbitMqDefinationFunctions _rabbitMqDefinationFunctions;
        private readonly RabbitMqMessagesFunctions _rabbitMqMessagesFunctions;
        private readonly IModel _model;

        public RabbitMQClient(SinkConfiguration sinkConfiguration)
        {
            _sinkConfiguration = sinkConfiguration;
            _rabbitMqDefinationFunctions = new RabbitMqDefinationFunctions();
            _rabbitMqMessagesFunctions = new RabbitMqMessagesFunctions();
            _model = Init();
        }
        public IModel Init()
        {
            try
            {

                // Initialize a connection
                var connection= _rabbitMqDefinationFunctions.CreateConnection(
                new ConnectionInputModel
                {
                    ClientName = _sinkConfiguration.ClientName,
                    ServerIP = _sinkConfiguration.RabbitMqHostName,
                    ServerPort = _sinkConfiguration.RabbitMqPort,
                    Username = _sinkConfiguration.RabbitMqUsername,
                    Password = _sinkConfiguration.RabbitMqPassword
                });

                var model = _rabbitMqDefinationFunctions.GetModelFromConnection(connection);


                // Be sure to exist the exchange and It's binds
                _rabbitMqDefinationFunctions.CreateAndBindExchange(
                model,
                new ExchangeModel
                {
                    ExchangeName = _sinkConfiguration.RabbitMqExchangeName,
                    ExchangeType = _sinkConfiguration.RabbitMqExchangeType
                },
                _sinkConfiguration.RabbitMqRouteKey,
                new QueueModel
                {
                    QueueName = _sinkConfiguration.RabbitMqQueueName
                });


                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendMessage(string message)
        {
            _rabbitMqMessagesFunctions.SendMessage(_model, _sinkConfiguration.RabbitMqExchangeName, _sinkConfiguration.RabbitMqRouteKey, message);
        }


        public void Dispose()
        {
            _model?.Dispose();
        }

    }
}
