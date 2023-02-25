namespace Haxee.MQTTConsumer.Services
{
    public class DrawService
    {
        private static Dictionary<string, ConsoleColor> _topicColor = new Dictionary<string, ConsoleColor>();
        public static void DrawLogo()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  _    _ _        ______ _   _____       _ _");
            Console.WriteLine(" | |  | (_)      |  ____(_) |  __ \\     | | |");
            Console.WriteLine(" | |__| |_ ______| |__   _  | |__) |__ _| | | ___ _   _");
            Console.WriteLine(" |  __  | |______|  __| | | |  _  // _` | | |/ _ \\ | | |");
            Console.WriteLine(" | |  | | |      | |    | | | | \\ \\ (_| | | |  __/ |_| |");
            Console.WriteLine(" |_|  |_|_|      |_|    |_| |_|  \\_\\__,_|_|_|\\___|\\__, |");
            Console.WriteLine("                                                  __ / |");
            Console.WriteLine("                                                 | ___/ ");

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrawErrorOption(List<int> validOptions)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not valid option. Valid options:");

            foreach (int option in validOptions)
                Console.WriteLine($"  {option}");

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowCurrentSettings()
        {

            if (!CurrentYear.SettedUp())
            {
                DrawErrorMessage("Missin current year setup");
                Console.WriteLine("\nPress any key to return ...");
                Console.ReadLine();
                return;
            }

            CurrentYear currentYear = CurrentYear.GetInstance();

            Console.Write("Year ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(currentYear.Year);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Client name ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(currentYear.ClientName);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Broker IP ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(currentYear.BrokerIP);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Broker port ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(currentYear.BrokerPort);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Global topic ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(currentYear.GlobalTopic);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Setup done ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("true");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrawMainMenu()
        {
            DrawLogo();
            Console.WriteLine("MENU");
            Console.WriteLine($"[1] Setup Hi-Fi {DateTime.Today.Year}");
            Console.WriteLine($"[2] Show current setup Hi-Fi {DateTime.Today.Year}");
            Console.WriteLine($"[3] Connect to broker & start consumer");
            Console.WriteLine($"[4] Quit\n");
        }
        public static void DrawBonk()
        {
            Console.WriteLine("         .       .");
            Console.WriteLine("        /|-------|\\");
            Console.WriteLine("       /           \\                __");
            Console.WriteLine("      /             |               / /");
            Console.WriteLine("     /              \\            /  /");
            Console.WriteLine("    /                 |          /  /");
            Console.WriteLine("  _/               __/         / / -----\\");
            Console.WriteLine(" /                |          /  //       \\");
            Console.WriteLine("|                 |        / / /          |");
            Console.WriteLine("|                 |       / / _|          \\");
            Console.WriteLine("\\                 \\    / / /              |");
            Console.WriteLine("  \\                ----//   \\              \\");
            Console.WriteLine("    \\__    ______      /     \\             |");
            Console.WriteLine("        \\_/      \\___/        |            |");
        }

        public static void DrawErrorMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrawSuccessMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrawInfoMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void AddTopicColor(string topic)
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            ConsoleColor consoleColor = (_topicColor.Count() > 15) ? ConsoleColor.White : colors[_topicColor.Count() + 1];
            _topicColor.Add(topic, consoleColor);
        }

        public static void DrawReceivedMessage(string topic, string message)
        {
            ConsoleColor consoleColor;

            while(!_topicColor.TryGetValue(topic, out consoleColor))
                AddTopicColor(topic);

            Console.ForegroundColor = consoleColor;
            Console.Write($"[{topic}]  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

    }
}
