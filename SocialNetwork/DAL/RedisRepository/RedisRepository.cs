using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using MongoDB.Bson.IO;

namespace DAL.RedisRepository
{
    class RedisRepository
    {
        ServiceStack.Redis.RedisClient client;
        public RedisRepository()
        {
            client = new RedisClient("localhost", 6379); // create redis client
        }


        public List<Person> GetFriendsOfFriendCache(string key)
        {
            return JsonConvert.DeserializeObject<List<Person>>(client.Get<string>(key));
        }

        public void SetFriendsOfFriendCache(string key, List<Person> value, int ttl)
        {
            var serialized = JsonConvert.SerializeObject(value); 
            client.Add<string>(key, serialized, DateTime.Now.AddSeconds(ttl)); 
        }


        public bool HasCache(string key)
        {
            return client.Exists(key) == 1;
        }
    }
}