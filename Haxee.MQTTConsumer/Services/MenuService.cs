using Haxee.Entities.Entities.Mqtt;

namespace Haxee.MQTTConsumer.Services
{
    /// <summary>
    /// Trieda, ktorá ma na starosti funkcionality menu.
    /// </summary>
    public class MenuService
    {
        /// <summary>
        /// Hlavné menu.
        /// </summary>
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

        /// <summary>
        /// Menu pri aktuálnej konfigurácii.
        /// </summary>
        public static async Task CurrentSetup()
        {
            Console.Clear();

            if (!(await CurrentYear.IsSetUp()))
                return;

            await DrawService.ShowCurrentSettings();

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

                await DrawService.ShowCurrentSettings();

                Console.WriteLine("\n[0] Back");
            }
        }

        /// <summary>
        /// Menu na stránke o chýbajucej konfigurácii.
        /// </summary>
        public static void MQTTMissingInfoScreen()
        {
            Console.Clear();
            DrawService.DrawErrorMessage("You need to provide Hi-Fi Ralley setup first");
            Console.WriteLine("\nPress any key to return");
            Console.ReadLine();
        }

        /// <summary>
        /// Menu na stránke pri tvorbe konfigurácie.
        /// </summary>
        public static async void HifiSetUp()
        {
            Console.Clear();
            DrawService.DrawErrorMessage("Hi-Fi Ralley already set up\n");

            await DrawService.ShowCurrentSettings();

            Console.WriteLine("\n\n[D] delete configuration");
            Console.WriteLine("Press any other key to return");
            string input = Console.ReadLine() ?? String.Empty;

            if (input.Equals("D"))
                CurrentYear.Clear();
        }
    }
}
