using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class RedisPubSubServerHelper
    {
        RedisPubSubServer pubSubServer { get; set; }
        private string pubSubChannel { get; set; }
        public RedisPubSubServerHelper(PubSubChannel pubSubChannelName)
        {
            pubSubChannel = pubSubChannelName.ToString();
            pubSubServer = new RedisPubSubServer(RedisHelper.PooledRedisClientManager, pubSubChannel);
            pubSubServer.Start();
         
        }


        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public long Pub(string message)
        {
            return RedisHelper.Singleton().PublishMessage(pubSubChannel, message);
        }
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="fun"></param>
        public void Sub(Action<string> fun)
        {
            pubSubServer.OnMessage = (channel, message) => fun(message);

        }

    }

    /// <summary>
    /// 管道枚举
    /// </summary>
    public enum PubSubChannel
    {
        LogChannel
    }
}
