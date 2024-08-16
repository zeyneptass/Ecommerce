using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RedisService
    {
        readonly ConnectionMultiplexer _connection;

        public RedisService(IConfiguration configuration)
        {
            _connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
        }

        public IDatabase GetDatabase()
        {
            return _connection.GetDatabase();
        }
    }
}