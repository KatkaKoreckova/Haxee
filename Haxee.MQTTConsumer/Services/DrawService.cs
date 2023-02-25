namespace Haxee.MQTTConsumer.Services
{
    public class DrawService
    {
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

        public static void ShowCurrentSettings(CurrentYear currentYear)
        {
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
            Console.WriteLine(currentYear.SetupDone);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrawMainMenu()
        {
            Console.WriteLine("MENU");
            Console.WriteLine($"[1] Setup Hi-Fi {DateTime.Today.Year}");
            Console.WriteLine($"[2] Show current setup Hi-Fi {DateTime.Today.Year}");
            Console.WriteLine($"[3] Connect to broker");
            Console.WriteLine($"[4] Start Hi-Fi {DateTime.Today.Year}");
            Console.WriteLine($"[5] Quit");
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

    }
}
