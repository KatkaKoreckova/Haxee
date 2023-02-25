using Haxee.Entities.DTOs;

namespace Haxee.MQTTConsumer.Services
{
    class SetupService
    {
        public static CurrentYear SetupCurrentYear()
        {
            Console.WriteLine($"SETUP Hi-Fi Ralley {DateTime.Today.Year}");

            CurrentYear currentYear = new CurrentYear() {
                BrokerIP = "",
                ClientName = "",
                GlobalTopic = "",
                BrokerPort = 0,
                Year = 0
            };
            return currentYear;
        }
    }
}
