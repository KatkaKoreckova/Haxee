﻿namespace Haxee.Entities.DTOs
{
    public class CurrentYear
    {
        public string BrokerIP { get; set; } = String.Empty;
        public int BrokerPort { get; set; }
        public string ClientName { get; set; } = String.Empty;
        public string GlobalTopic { get; set; } = String.Empty;
        public int Year { get; set; }

        private static CurrentYear? _instance = null;

        private CurrentYear() { }

        public static CurrentYear GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CurrentYear();
            }
            return _instance;
        }

        public static bool SettedUp()
        {
            if (_instance == null)
                return false;
            return true;
        }

        public static void Clear()
        {
            _instance = null;
        }
    }
}
