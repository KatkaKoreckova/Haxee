using Haxee.Entities.DTOs;

namespace Haxee.MQTTConsumer.Services
{
    class SetupService
    {
        public static void SetupCurrentYear()
        {
            bool validSetup = false;
            string year = String.Empty;
            string ip = String.Empty;
            string port = String.Empty;
            string name = String.Empty;
            string topic = String.Empty;

            Console.Clear();
            Console.WriteLine($"Hi-Fi Ralley SETUP");

            while (!validSetup)
            {
                Console.Write("Year:\n   ");
                year = Console.ReadLine() ?? String.Empty;

                Console.Write("Broker IPv4 :\n   ");
                ip = Console.ReadLine() ?? String.Empty;

                Console.Write("Broker port:\n   ");
                port = Console.ReadLine() ?? String.Empty;

                Console.Write("Client name:\n   ");
                name = Console.ReadLine() ?? String.Empty;

                Console.Write("Topic (topicname/#):\n   ");
                topic = Console.ReadLine() ?? String.Empty;

                Console.Clear();
                Console.WriteLine($"Hi-Fi Ralley SETUP");

                if (HelperService.ValidateCurrentYearSetup(year, topic, port, ip))
                    validSetup = true;
            }

            Program.CurrentYear = new CurrentYear() {
                BrokerIP = ip,
                ClientName = name,
                GlobalTopic = topic,
                BrokerPort = Convert.ToInt32(port),
                Year = Convert.ToInt32(year),
                SetupDone = true,
            };

            Console.Clear();
            DrawService.ShowCurrentSettings(Program.CurrentYear);

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

                DrawService.ShowCurrentSettings(Program.CurrentYear);

                Console.WriteLine("\n[1] Save & quit");
                Console.WriteLine("\n[2] Discard & quit");
            }

            if (option == 2)
                Program.CurrentYear.Clear();

        }
    }
}
