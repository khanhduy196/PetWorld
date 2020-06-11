using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Infrastructure
{
    class RabbitMQExchangeTypes
    {
        public static string DIRECT = "direct";
        public static string FANOUT = "fanout";
        public static string HEADERS = "headers";
        public static string TOPIC = "topic";
    }
}
