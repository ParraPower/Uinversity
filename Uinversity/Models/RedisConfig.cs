﻿namespace Uinversity.Models
{
    public class RedisConfig
    {
        public RedisConfig() 
        {
            ServerName = "localhost";
            PortNumber = string.Empty;
        }
        public string ServerName { get; set; }

        public string PortNumber { get; set; }
    }
}
