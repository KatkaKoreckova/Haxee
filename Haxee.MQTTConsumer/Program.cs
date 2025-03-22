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
                    await MqttService.SetupAndRunMQTT();
                    break;
                case 2:
                    quit = true;
                    break;
            }
        }

        Console.Clear();

        
    }
}