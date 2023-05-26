class Program
{

    static async Task Main(string[] args)
    {
        bool quit = false;

        while (!quit)
        {
            int option = MenuService.MainMenu();

            switch (option)
            {
                case 1:
                    await SetupService.SetupCurrentYear();
                    break;
                case 2:
                    await MenuService.CurrentSetup();
                    break;
                case 3:
                    await MqttService.SetupAndRunMQTT();
                    break;
                case 4:
                    quit = true;
                    break;
            }
        }

        Console.Clear();

        
    }
}