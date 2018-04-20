using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EasyNetQ;

namespace Common
{
    public class RabbitHelper
    {

        private string QueueName = RabbitQueueEnum.ExceptionLog.ToString();
        private static string ConnectionString = "host=127.0.0.1;port=5672;virtualHost=develop;username=gyf;password=123456;requestedHeartbeat=10";

        private ConnectionFactory Factory = new ConnectionFactory
        {
            HostName = "127.0.0.1",
            UserName = "gyf",
            Password = "123456",
            VirtualHost = "develop",
            Port = 5672,
        };
        /// <summary>
        /// 原生版本 发布消息
        /// </summary>
        /// <param name="message"></param>
        public void Pub(string message)
        {
            using (var conn = Factory.CreateConnection())
            using (var channel = conn.CreateModel())
            {

                //声明队列
                channel.QueueDeclare(queue: QueueName,    //队列名
                   durable: false,     //持久性
                   exclusive: false,   //排他性
                   autoDelete: false,  //自动删除
                   arguments: null);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",  //交换机名
                    routingKey: QueueName,    //路由键
                    basicProperties: null,
                    body: body);
            }
        }
        /// <summary>
        /// 原生版本 订阅消息
        /// </summary>
        /// <param name="action"></param>
        public void Sub(Action<string> action)
        {
            using (var conn = Factory.CreateConnection())
            using (var channel = conn.CreateModel())
            {

                channel.QueueDeclare(queue: RabbitQueueEnum.ExceptionLog.ToString(),
                                               durable: false,
                                               exclusive: false,
                                               autoDelete: false,
                                               arguments: null);

                //创建基于该队列的消费者，绑定事件
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;     //消息主体
                    var message = Encoding.UTF8.GetString(body);
                    action(message);
                };

                //启动消费者
                channel.BasicConsume(queue: RabbitQueueEnum.ExceptionLog.ToString(),    //队列名
                                     noAck: true,   //false：手动应答；true：自动应答
                                     consumer: consumer);



                Console.Read();
            }
        }




        /// <summary>
        /// EasyNetQ版本 发布消息    利用routing广播  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="routing"></param>
        public void PubEasy<T>(T message, string routing = "") where T : class
        {
            using (var ibus = RabbitHutch.CreateBus(ConnectionString))
            {
                ibus.Publish(message, routing);
            }
        }
        /// <summary>
        /// EasyNetQ版本 订阅消息   当启动多个消费端的时候  每个消费都会收到消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="routing"></param>
        public void SubEasy<T>(Action<T> action, string routing = "") where T : class
        {
            using (var ibus = RabbitHutch.CreateBus(ConnectionString))
            {
                ibus.Subscribe<T>(Guid.NewGuid().ToString(), o => action(o), o => o.WithTopic(routing));
                Console.Read();
                
            }
        }

        public void SendEasy<T>(RabbitQueueEnum queue, T t) where T : class
        {
            using (var ibus = RabbitHutch.CreateBus(ConnectionString))
            {
                var name = queue.ToString();
                ibus.Send(name, t);
                Console.Read();
            }
        }
        public void ReceiveEasy<T>(RabbitQueueEnum queue, Action<T> action) where T : class
        {
            using (var ibus = RabbitHutch.CreateBus(ConnectionString))
            {
                var name = queue.ToString();
                ibus.Receive<T>(name, o => action(o));
                Console.Read();
            }
        }
    }


    public enum RabbitMQExchangeEnum
    {
        LogExchange
    }
    public enum RabbitQueueEnum
    {
        ExceptionLog
    }
}





