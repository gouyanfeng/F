using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RedisPub
{
    class Program
    {
        static void Main(string[] args)
        {

            //new MSMessageQueueHelper(QueueName.Log).Send("123445677");
            //new MSMessageQueueHelper(QueueName.Log).Receive();

            Console.WriteLine("发送消息");
            new RedisPubSubServerHelper(PubSubChannel.LogChannel).Pub(Console.ReadLine());
            new RedisPubSubServerHelper(PubSubChannel.LogChannel).Pub(Console.ReadLine());
            new RedisPubSubServerHelper(PubSubChannel.LogChannel).Pub(Console.ReadLine());
            new RedisPubSubServerHelper(PubSubChannel.LogChannel).Pub(Console.ReadLine());
            new RedisPubSubServerHelper(PubSubChannel.LogChannel).Pub(Console.ReadLine());

            Console.ReadKey();
        }
    }
}
