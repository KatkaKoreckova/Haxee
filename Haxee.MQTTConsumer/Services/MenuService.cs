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
            List<int> validOptions = new List<int> { 1, 2 };

            while (!HelperService.ValidMenuOption(validOptions, option))
            {
                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                if (!HelperService.ValidMenuOption(validOptions, option))
                    DrawService.DrawErrorMessage($"Not a valid option. Valid options:\n  {string.Join("\n  ", validOptions)}");

                DrawService.DrawMainMenu();
            }

            return option;
        }
    }
}
