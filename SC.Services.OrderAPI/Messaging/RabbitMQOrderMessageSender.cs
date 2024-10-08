using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace SC.Services.OrderAPI.Messaging
{
    public class RabbitMQOrderMessageSender : IRabbitMQOrderMessageSender
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;

        public RabbitMQOrderMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }

        //public void SendMessage(object message, string queueName)         // RabbitMQ with Queue
        public void SendMessage(object message, string exchangeName)        // RabbitMQ with Exchange
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                //channel.QueueDeclare(queueName, false, false, false, null);                       // RabbitMQ with Queue
                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: false);         // RabbitMQ with Exchange
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                //channel.BasicPublish(exchange: "", routingKey: queueName, null, body: body);      // RabbitMQ with Queue
                channel.BasicPublish(exchange: exchangeName, routingKey: "", null, body: body);     // RabbitMQ with Exchange
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    Password = _password,
                    UserName = _userName,
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();
            return true;
        }
    }
}
