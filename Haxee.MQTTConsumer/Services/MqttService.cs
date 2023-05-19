using Haxee.Entities.Entities.Mqtt;

namespace Haxee.MQTTConsumer.Services
{
    public class MqttService
    {
        public static void SetupAndRunMQTT()
        {

            if (!CurrentYear.SettedUp())
            {
                MenuService.MQTTMissingInfoScreen();
                return;
            }

            CurrentYear currentYear = CurrentYear.GetInstance();

            Console.Clear();

            DrawService.DrawInfoMessage("Connecting to broker ...\n");

            // Creates a new client
            MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                        .WithClientId(currentYear.ClientName)
                                        .WithTcpServer(currentYear.BrokerIP, currentYear.BrokerPort);

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
                .WithTopic(currentYear.GlobalTopic)
                .Build()).GetAwaiter().GetResult();

            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                try
                {
                    string topic = e.ApplicationMessage.Topic;
                    string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    DrawService.DrawReceivedMessage(topic, message);
                    AttendeeInformationDTO? attendeeInformation = HelperService.ParseMessage(message, topic);

                    if (attendeeInformation is not null)
                    {
                        Console.Write(JsonSerializer.Serialize(attendeeInformation));
                        if (attendeeInformation.CardId.Equals("D3E0611A"))
                        {
                            Console.WriteLine("ok");
                            mqttClient.PublishAsync("start/checkResult", "1");
                        } else
                        {
                            Console.WriteLine("fail");
                            mqttClient.PublishAsync("start/checkResult", "0");
                        }
                    }
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
