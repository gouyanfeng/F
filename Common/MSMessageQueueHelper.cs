using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MSMessageQueueHelper
    {

        //创建队列：
        //创建本机队列：@".\private$\队列名称"
        //创建远程队列：@"FormatName:DIRECT=TCP:远程机器IP\private$\队列名称"
        public string QueuePath = ".\\Private$\\";
        public MSMessageQueueHelper(QueueName queueName)
        {
            QueuePath += queueName.ToString();
        }
        public void Send(string t)
        {
            if (!MessageQueue.Exists(QueuePath))
                MessageQueue.Create(QueuePath);
            var queue = new MessageQueue(QueuePath);
            queue.Send(t);

        }

        public void Receive<T>(Action<T> action) where T : class
        {

            if (MessageQueue.Exists(QueuePath))
            {
                using (MessageQueue mq = new MessageQueue(QueuePath))
                {
                    mq.ReceiveCompleted += (a, b) => { };
                    mq.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                    while (true)
                    {
                        Message firstmsg = mq.Receive();
                        action(firstmsg.Body as T);
                    }


                }
            }
        }
    }

    public enum QueueName
    {
        EMS,
        SMS,
        Log
    }
}

