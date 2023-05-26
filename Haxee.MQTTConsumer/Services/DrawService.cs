using Haxee.Entities.Entities.Mqtt;

namespace Haxee.MQTTConsumer.Services
{
    /// <summary>
    /// Trieda obsahujúce funkcie na vykresľovanie dát do konzoly.
    /// </summary>
    public class DrawService
    {
        /// <summary>
        /// Zoznam tém a ich farieb.
        /// </summary>
        private static Dictionary<string, ConsoleColor> _topicColor = new Dictionary<string, ConsoleColor>();

        /// <summary>
        /// Funkcia na vykreslenie loga
        /// </summary>
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

        /// <summary>
        /// Funkcia na informovanie o zle zadanej možnosti a výpis valídnych možností
        /// </summary>
        /// <param name="validOptions">Zoznam valídnych možností</param>
        public static void DrawErrorOption(List<int> validOptions)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not valid option. Valid options:");

            foreach (int option in validOptions)
                Console.WriteLine($"  {option}");

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Funkcia na zozbrazenie aktuálnej konfigurácie
        /// </summary>
        public static async Task ShowCurrentSettings()
        {
            Console.Clear();

            if (!(await CurrentYear.IsSetUp()))
            {
                DrawErrorMessage("Missing current year setup");
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

        /// <summary>
        /// Funkcia na vykreslenie hlavného menu.
        /// </summary>
        public static void DrawMainMenu()
        {
            DrawLogo();
            Console.WriteLine("MENU");
            Console.WriteLine($"[1] Setup Hi-Fi {DateTime.Today.Year}");
            Console.WriteLine($"[2] Show current setup Hi-Fi {DateTime.Today.Year}");
            Console.WriteLine($"[3] Connect to broker & start consumer");
            Console.WriteLine($"[4] Quit\n");
        }

        /// <summary>
        /// Funkcia na vykreslenie ilustrácie pri zle zadanej možnosti.
        /// </summary>
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

        /// <summary>
        /// Funkcia na výpis chybovej správy.
        /// </summary>
        /// <param name="text">Text chybovej správy</param>
        public static void DrawErrorMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Funkcia na výpis správy pri úspešnom úkone.
        /// </summary>
        /// <param name="text">Text správy na výpis</param>
        public static void DrawSuccessMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Funkcia na výpis informačnej správy.
        /// </summary>
        /// <param name="text">Text informačnej správy</param>
        public static void DrawInfoMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Funkcia, ktorá priradí zadanej téme farbu ak ju náhodou ešte nemá priradenú.
        /// </summary>
        /// <param name="topic">Názov témy</param>
        private static void AddTopicColor(string topic)
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            ConsoleColor consoleColor = (_topicColor.Count() > 15) ? ConsoleColor.White : colors[_topicColor.Count() + 1];
            _topicColor.Add(topic, consoleColor);
        }

        /// <summary>
        /// Funkcia na vykreslenie prijatej správy
        /// </summary>
        /// <param name="topic">Téma pod ktorou správa prišla</param>
        /// <param name="message">Správa ktorá prišla</param>
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
