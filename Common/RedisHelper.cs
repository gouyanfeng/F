using ServiceStack.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisHelper
    {



        private static RedisClient Client;
        private static IRedisClientsManager ClientManager;


        private static string Host = "127.0.0.1";
        private static int port = 6379;
        private RedisHelper()
        {
        }

        public static RedisClient Singleton()
        {
            if (Client == null)
            {
                Client = new RedisClient(Host, port);
            }
            return Client;
        }

        public static IRedisClientsManager PooledRedisClientManager
        {
            get { return ClientManager == null ? new PooledRedisClientManager(string.Format("{0}:{1}", Host, port)) : ClientManager; }
        }


        public T Get<T>(string key)
        {
            Client.AddItemToList("bbbbbbbbb", "vvvvvvvv");
            return Client.Get<T>(key);
        }

  

    }
}
