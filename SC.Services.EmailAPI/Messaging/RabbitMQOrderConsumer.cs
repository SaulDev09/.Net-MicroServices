using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SC.Services.EmailAPI.Message;
using SC.Services.EmailAPI.Services;
using System.Text;

namespace SC.Services.EmailAPI.Messaging
{
    public class RabbitMQOrderConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        private IConnection _connection;
        private IModel _channel;
        string queueName = string.Empty;

        #region [RabbitMQ with Direct]
        private string ExchangeName = string.Empty;
        private const string OrderCreated_EmailUpdateQueue = "EmailUpdateQueue";
        #endregion
        private bool _rabbitMQIsFanout = false;


        public RabbitMQOrderConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
            if (!_rabbitMQIsFanout)
                ExchangeName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic"); // RabbitMQ with Direct

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            //_channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic"), false, false, false, null);

            if (_rabbitMQIsFanout)
            {
                #region [RabbitMQ with Fanout]
                _channel.ExchangeDeclare(_configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic"), ExchangeType.Fanout);
                queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queueName, configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic"), "");
                #endregion
            }
            else
            {
                #region [RabbitMQ with Direct]
                _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
                _channel.QueueDeclare(OrderCreated_EmailUpdateQueue, false, false, false, null);
                _channel.QueueBind(OrderCreated_EmailUpdateQueue, ExchangeName, "EmailUpdate");
                #endregion
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                RewardsMessage rewardsMessage = JsonConvert.DeserializeObject<RewardsMessage>(content);
                HandleMessage(rewardsMessage).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            if (_rabbitMQIsFanout)
                _channel.BasicConsume(queueName, false, consumer);                    // RabbitMQ with Fanout
            else
                _channel.BasicConsume(OrderCreated_EmailUpdateQueue, false, consumer);  // RabbitMQ with Direct

            return Task.CompletedTask;
        }

        private async Task HandleMessage(RewardsMessage rewardsMessage)
        {
            _emailService.LogOrderPlaced(rewardsMessage).GetAwaiter().GetResult();
        }
    }
}
