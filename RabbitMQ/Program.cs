using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitPub
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadKey();
            //Producer.Send("aaaaaaaaaaaaaa");
            //Console.ReadKey();
            //Producer.Send("bbbbbbbbbbb");
            //Console.ReadKey();
            //Producer.Send("ccccccccccc");
            //Console.ReadKey();
            //Producer.Send("eeeeeeeeeeee");

            new RabbitHelper().Pub("ttttttttttttt");
            //Console.ReadKey();
            //RabbitMQHelper.Pub("bbbbbbbbbbb");
            //Console.ReadKey();
            //RabbitMQHelper.Pub("ccccccccccc");
            //Console.ReadKey();
            //RabbitMQHelper.Pub("ddddddddddd");
            //Console.ReadKey();
            //RabbitMQHelper.Pub("eeeeeeeeeeee");
            //Console.ReadKey();
        }
    }



}
