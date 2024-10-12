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

        #region [RabbitMQ | Direct]
        private const string OrderCreated_RewardsUpdateQueue = "RewardsUpdateQueue";
        private const string OrderCreated_EmailUpdateQueue = "EmailUpdateQueue";
        #endregion

        public RabbitMQOrderMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }
        private bool _rabbitMQIsFanout = false;

        //public void SendMessage(object message, string queueName)         // RabbitMQ with Queue
        public void SendMessage(object message, string exchangeName)        // RabbitMQ with Fanout
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                //channel.QueueDeclare(queueName, false, false, false, null);                       // RabbitMQ with Queue

                if (_rabbitMQIsFanout)
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: false);       // RabbitMQ with Fanout
                else
                {
                    #region [RabbitMQ with Direct]
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: false);

                    channel.QueueDeclare(OrderCreated_EmailUpdateQueue, false, false, false, null);
                    channel.QueueDeclare(OrderCreated_RewardsUpdateQueue, false, false, false, null);

                    channel.QueueBind(OrderCreated_EmailUpdateQueue, exchangeName, "EmailUpdate");
                    channel.QueueBind(OrderCreated_RewardsUpdateQueue, exchangeName, "RewardsUpdate");
                    #endregion
                }

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                //channel.BasicPublish(exchange: "", routingKey: queueName, null, body: body);      // RabbitMQ with Queue

                if (_rabbitMQIsFanout)
                    channel.BasicPublish(exchange: exchangeName, routingKey: "", null, body: body);     // RabbitMQ with Fanout
                else
                {
                    #region [RabbitMQ with Direct]
                    channel.BasicPublish(exchange: exchangeName, "EmailUpdate", null, body: body);
                    channel.BasicPublish(exchange: exchangeName, "RewardsUpdate", null, body: body);
                    #endregion
                }
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
