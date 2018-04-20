using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisSub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("等待接受消息");
            new RedisPubSubServerHelper(PubSubChannel.LogChannel).Sub(o => Console.WriteLine("接收到消息:" + o));
            Console.ReadKey();
        }
    }
}
