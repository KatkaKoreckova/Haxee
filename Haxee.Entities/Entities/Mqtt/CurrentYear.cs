using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

namespace Haxee.Entities.Entities.Mqtt
{
    public class CurrentYear
    {
        public string BrokerIP { get; set; } = string.Empty;
        public int BrokerPort { get; set; }
        public string ClientName { get; set; } = "MQTTConsumer";
        public string GlobalTopic { get; set; } = string.Empty;
        public int Year { get; set; }

        private static CurrentYear? _instance = null;

        public CurrentYear() { }

        public static CurrentYear GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CurrentYear();
            }
            return _instance;
        }

        public static async Task<bool> IsSetUp()
        {
            var instance = GetInstance();

            if (instance.Year != 0)
                return true;

            using var client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7044/api/setup");

            if (!response.IsSuccessStatusCode)
                return false;

            var currentYear = await response.Content.ReadFromJsonAsync<Year>();

            if (currentYear is null)
                return false;

            instance.BrokerIP = currentYear.BrokerIp ?? string.Empty;
            instance.BrokerPort = currentYear.BrokerPort ?? 0;
            instance.GlobalTopic = currentYear.GlobalTopic ?? string.Empty;
            instance.Year = currentYear.YearValue;

            return true;
        }

        public static void Clear()
        {
            _instance = null;
        }
    }
}
