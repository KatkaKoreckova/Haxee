using Haxee.Entities.Entities.Mqtt;

namespace Haxee.MQTTConsumer.Services
{
    class SetupService
    {
        /// <summary>
        /// Funkcia pre vytvorenie konfigurácie kočníka pre aplikáciu MQTT Consumer.
        /// </summary>
        public static async Task SetupCurrentYear()
        {
            if (await CurrentYear.IsSetUp())
            {
                MenuService.HifiSetUp();
                return;
            }

            var validSetup = false;
            var year = string.Empty;
            var ip = string.Empty;
            var port = string.Empty;
            //var name = string.Empty;
            var topic = string.Empty;

            Console.Clear();
            Console.WriteLine($"Hi-Fi Ralley SETUP");

            while (!validSetup)
            {
                Console.Write("Year:\n   ");
                year = Console.ReadLine() ?? string.Empty;

                Console.Write("Broker IPv4 :\n   ");
                ip = Console.ReadLine() ?? string.Empty;

                Console.Write("Broker port:\n   ");
                port = Console.ReadLine() ?? string.Empty;

                //Console.Write("Client name:\n   ");
                //name = Console.ReadLine() ?? string.Empty;

                Console.Write("Topic (topicname/#):\n   ");
                topic = Console.ReadLine() ?? string.Empty;

                Console.Clear();
                Console.WriteLine($"Hi-Fi Ralley SETUP");

                if (HelperService.ValidateCurrentYearSetup(year, topic, port, ip))
                    validSetup = true;
            }

            CurrentYear currentYear = CurrentYear.GetInstance();
            currentYear.BrokerIP = ip;
            //currentYear.ClientName = name;
            currentYear.GlobalTopic = topic;
            currentYear.BrokerPort = Convert.ToInt32(port);
            currentYear.Year = Convert.ToInt32(year);

            Console.Clear();
            await DrawService.ShowCurrentSettings();

            Console.WriteLine("\n[1] Save & quit");
            Console.WriteLine("\n[2] Discard & quit");

            int option = 0;
            List<int> validOptions = new List<int> { 1, 2 };

            while (!HelperService.ValidMenuOption(validOptions, option))
            {
                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                if (!HelperService.ValidMenuOption(validOptions, option))
                {
                    DrawService.DrawBonk();
                    DrawService.DrawErrorOption(validOptions);
                }

                await DrawService.ShowCurrentSettings();

                Console.WriteLine("\n[1] Save & quit");
                Console.WriteLine("\n[2] Discard & quit");
            }

            if (option == 2)
                CurrentYear.Clear();
            else
            {
                using var client = new HttpClient();
                using StringContent jsonContent = new(JsonSerializer.Serialize(currentYear), Encoding.UTF8, "application/json");

                var _ = await client.PostAsync("https://localhost:7044/api/setup", jsonContent);
            }

        }
    }
}
