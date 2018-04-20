using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始接到数据");
            //new RabbitHelper().SubEasy<string>(o => Console.WriteLine("接到数据:" + o), "easy.log");
            //Console.WriteLine();
            new RabbitHelper().ReceiveEasy<string>(RabbitQueueEnum.ExceptionLog, o => Console.WriteLine("接到数据:" + o));
            Console.ReadKey();

        }




    }
}
