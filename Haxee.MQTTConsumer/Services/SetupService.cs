using Haxee.Entities.DTOs;

namespace Haxee.MQTTConsumer.Services
{
    class SetupService
    {
        public static void SetupCurrentYear()
        {
            bool validSetup = false;
            string year = String.Empty;
            string ip = String.Empty;
            string port = String.Empty;
            string name = String.Empty;
            string topic = String.Empty;

            Console.Clear();
            Console.WriteLine($"Hi-Fi Ralley SETUP");

            while (!validSetup)
            {
                Console.Write("Year:\n   ");
                year = Console.ReadLine() ?? String.Empty;

                Console.Write("Broker IPv4 :\n   ");
                ip = Console.ReadLine() ?? String.Empty;

                Console.Write("Broker port:\n   ");
                port = Console.ReadLine() ?? String.Empty;

                Console.Write("Client name:\n   ");
                name = Console.ReadLine() ?? String.Empty;

                Console.Write("Topic (topicname/#):\n   ");
                topic = Console.ReadLine() ?? String.Empty;

                Console.Clear();
                Console.WriteLine($"Hi-Fi Ralley SETUP");

                if (HelperService.ValidateCurrentYearSetup(year, topic, port, ip))
                    validSetup = true;
            }

            Program.CurrentYear = new CurrentYear() {
                BrokerIP = ip,
                ClientName = name,
                GlobalTopic = topic,
                BrokerPort = Convert.ToInt32(port),
                Year = Convert.ToInt32(year),
                SetupDone = true,
            };

            Console.Clear();
            DrawService.ShowCurrentSettings(Program.CurrentYear);

            Console.WriteLine("\n[1] Save & quit");
            Console.WriteLine("\n[2] Discard & quit");

            int option = 0;
            List<int> validOptions = new List<int> { 1, 2 };

            while (!HelperService.ValidMenuOption(validOptions, option))
            {
                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                if (!HelperService.ValidMenuOption(validOptions, option))
                {
                    DrawService.DrawBonk();
                    DrawService.DrawErrorOption(validOptions);
                }

                DrawService.ShowCurrentSettings(Program.CurrentYear);

                Console.WriteLine("\n[1] Save & quit");
                Console.WriteLine("\n[2] Discard & quit");
            }

            if (option == 2)
                Program.CurrentYear.Clear();

        }

        public static void SetupAndRunMQTT()
        {
            if (!Program.CurrentYear.SetupDone)
            {
                MenuService.MQTTMissingInfoScreen();
                return;
            }

            Console.Clear();

            DrawService.DrawInfoMessage("Connecting to broker ...\n");

            // Creates a new client
            MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                        .WithClientId(Program.CurrentYear.ClientName)
                                        .WithTcpServer(Program.CurrentYear.BrokerIP, Program.CurrentYear.BrokerPort);

            // Create client options objects
            ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                                    .WithClientOptions(builder.Build())
                                    .Build();

            // client object
            IManagedMqttClient mqttClient = new MqttFactory().CreateManagedMqttClient();


            // Set up handlers
            mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
            mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
            mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);

            mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(a => {
                DrawService.DrawInfoMessage($"Message recieved: {a.ApplicationMessage}");
            });

            // Starts a connection with the Broker
            mqttClient.StartAsync(options).GetAwaiter().GetResult();

            mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(Program.CurrentYear.GlobalTopic)
                .Build()).GetAwaiter().GetResult();

            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                try
                {
                    string topic = e.ApplicationMessage.Topic;
                    string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    DrawService.DrawReceivedMessage(topic, message);
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
        public static void OnConnected(MqttClientConnectedEventArgs obj)
        {
            DrawService.DrawSuccessMessage("Successfully connected.");
        }

        public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
        {
            DrawService.DrawErrorMessage("Couldn't connect to broker.");
        }

        public static void OnDisconnected(MqttClientDisconnectedEventArgs obj)
        {
            DrawService.DrawInfoMessage("Successfully disconnected.");
        }
    }
}
