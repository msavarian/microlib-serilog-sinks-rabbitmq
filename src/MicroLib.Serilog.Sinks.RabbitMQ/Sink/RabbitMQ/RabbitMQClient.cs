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
        private readonly RabbitMqFunctions _rabbitMQFunctions;
        private readonly IConnection _connection;

        public RabbitMQClient(SinkConfiguration sinkConfiguration)
        {
            _sinkConfiguration = sinkConfiguration;
            _rabbitMQFunctions = new RabbitMqFunctions();
            _connection = Init();
        }
        public IConnection Init()
        {
            try
            {

                // Initialize a connection
                var connection= _rabbitMQFunctions.CreateConnection(
                new ConnectionInputModel
                {
                    ClientName = _sinkConfiguration.ClientName,
                    ServerIP = _sinkConfiguration.RabbitMqHostName,
                    ServerPort = _sinkConfiguration.RabbitMqPort,
                    Username = _sinkConfiguration.RabbitMqUsername,
                    Password = _sinkConfiguration.RabbitMqPassword
                });



                // Be sure to exist the exchange and It's binds
                _rabbitMQFunctions.CreateAndBindExchange(
                connection,
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


                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendMessage(string message)
        {
            _rabbitMQFunctions.SendMessage(_connection, _sinkConfiguration.RabbitMqExchangeName, _sinkConfiguration.RabbitMqRouteKey, message);
        }


        public void Dispose()
        {
            _connection?.Dispose();
        }

    }
}
