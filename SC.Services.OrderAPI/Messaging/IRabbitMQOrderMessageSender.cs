namespace SC.Services.OrderAPI.Messaging
{
    public interface IRabbitMQOrderMessageSender
    {
        //void SendMessage(object message, string queueName);         // RabbitMQ with Queue
        void SendMessage(object message, string exchangeName);        // RabbitMQ with Fanout
    }
}
