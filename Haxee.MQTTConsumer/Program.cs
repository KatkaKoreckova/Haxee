class Program
{
    private static CurrentYear _currentYear = new CurrentYear()
    {
        BrokerIP = "192.168.0.244",
        ClientName = "HaxeeConsumer",
        GlobalTopic = "2023/#",
        BrokerPort = 1883,
        Year = 2023
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
                    break;
                case 2:
                    MenuService.CurrentSetup(_currentYear);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    quit = true;
                    break;
            }
        }


        //CurrentYear currentYear = SetupService.SetupCurrentYear();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        // Creates a new client
        MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                    .WithClientId(_currentYear.ClientName)
                                    .WithTcpServer(_currentYear.BrokerIP, _currentYear.BrokerPort);

        // Create client options objects
        ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                                .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                                .WithClientOptions(builder.Build())
                                .Build();

        // client object
        IManagedMqttClient _mqttClient = new MqttFactory().CreateManagedMqttClient();


        // Set up handlers
        _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
        _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
        _mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);

        _mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(a => {
            Log.Logger.Information("Message recieved: {payload}", a.ApplicationMessage);
        });

        // Starts a connection with the Broker
        _mqttClient.StartAsync(options).GetAwaiter().GetResult();




        _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
            .WithTopic(_currentYear.GlobalTopic)
            .Build()).GetAwaiter().GetResult();

        _mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            try
            {
                string topic = e.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.Write($"{topic}: ");
                Console.WriteLine(payload);
                /*

                if (string.IsNullOrWhiteSpace(topic) == false)
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    string type = topic.Substring(topic.LastIndexOf('/') + 1);

                    if (topic.Contains("person"))
                    {
                        var id = payload.Split(' ')[0];
                        var dateTime = payload.Split(' ')[1];

                        var person = new Person
                        {
                            PersonId = id,
                            At = DateTime.Parse(dateTime)
                        };

                        Console.WriteLine(JsonSerializer.Serialize(person));
                        PostApi(person);
                    }
                    else if (topic.Contains("alarm"))
                    {
                        var value = payload.Split(' ')[0];

                        var valueBoolean = false;
                        if (value.Equals("true"))
                        {
                            valueBoolean = true;
                        }
                        else
                        {
                            valueBoolean = false;
                        }

                        var alarm = new Alarm
                        {
                            Enabled = valueBoolean
                        };

                        Console.WriteLine(JsonSerializer.Serialize(alarm));
                        PostApi(alarm);
                    }
                    else
                    {
                        var value = payload.Split(' ')[0];
                        var dateTime = payload.Split(' ')[1];


                        var dataPoint = new DataPoint
                        {
                            Value = Convert.ToSingle(value.Replace('.', ',')),
                            At = DateTime.Parse(dateTime),
                        };

                        Console.WriteLine(JsonSerializer.Serialize(dataPoint));
                        PostApi(dataPoint, type);
                    }
                    Console.WriteLine($"Topic: {topic}. Message Received: {payload}");
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        });

        while (true)
        {
            Task.Delay(1000).GetAwaiter().GetResult();
        }
    }

    // HANDLERS
    public static void OnConnected(MqttClientConnectedEventArgs obj)
    {
        Log.Logger.Information("Successfully connected.");
    }

    public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
    {
        Log.Logger.Warning("Couldn't connect to broker.");
    }

    public static void OnDisconnected(MqttClientDisconnectedEventArgs obj)
    {
        Log.Logger.Information("Successfully disconnected.");
    }
}