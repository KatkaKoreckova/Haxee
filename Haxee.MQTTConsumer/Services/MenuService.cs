namespace Haxee.MQTTConsumer.Services
{
    public class MenuService
    {
        public static int MainMenu()
        {
            Console.Clear();

            DrawService.DrawMainMenu();

            int option = 0;
            List<int> validOptions = new List<int> { 1, 2, 3, 4 };

            while (!HelperService.ValidMenuOption(validOptions, option))
            {
                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                if (!HelperService.ValidMenuOption(validOptions, option))
                {
                    DrawService.DrawBonk();
                    DrawService.DrawErrorOption(validOptions);
                }

                DrawService.DrawMainMenu();
            }

            return option;
        }

        public static void CurrentSetup()
        {
            Console.Clear();
            DrawService.ShowCurrentSettings();


            if (!CurrentYear.SettedUp())
                return;

            CurrentYear currentYear = CurrentYear.GetInstance();

            Console.WriteLine("\n[0] Back");

            int option = -1;
            List<int> validOptions = new List<int> { 0 };

            while (!HelperService.ValidMenuOption(validOptions, option))
            {
                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                if (!HelperService.ValidMenuOption(validOptions, option))
                {
                    DrawService.DrawBonk();
                    DrawService.DrawErrorOption(validOptions);
                }

                DrawService.ShowCurrentSettings();

                Console.WriteLine("\n[0] Back");
            }
        }

        public static void MQTTMissingInfoScreen()
        {
            Console.Clear();
            DrawService.DrawErrorMessage("You need to provide Hi-Fi Ralley setup first");
            Console.WriteLine("\nPress any key to return");
            Console.ReadLine();
        }
        public static void HifiSettedUp()
        {
            Console.Clear();
            DrawService.DrawErrorMessage("Hi-Fi Ralley already setted up");
            Console.WriteLine("\n[D] delete configuration");
            Console.WriteLine("Press any other key to return");
            string input = Console.ReadLine() ?? String.Empty;

            if (input.Equals("D"))
                CurrentYear.Clear();
        }
    }
}
