class Program
{
    public static CurrentYear CurrentYear = new CurrentYear()
    {
        BrokerIP = "",
        ClientName = "",
        GlobalTopic = "",
        BrokerPort = 0,
        Year = 0,
        SetupDone = false
    };

    static void Main(string[] args)
    {
        bool quit = false;

        while (!quit)
        {
            int option = MenuService.MainMenu();

            switch (option)
            {
                case 1:
                    SetupService.SetupCurrentYear();
                    break;
                case 2:
                    MenuService.CurrentSetup(CurrentYear);
                    break;
                case 3:
                    SetupService.SetupAndRunMQTT();
                    break;
                case 4:
                    quit = true;
                    break;
            }
        }

        Console.Clear();

        
    }
}