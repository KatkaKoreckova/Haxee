using Haxee.Entities.Entities.Mqtt;
using System.Text;

namespace Haxee.MQTTConsumer.Services
{
    /// <summary>
    /// Trieda na komunikáciu s MQTT broker-om
    /// </summary>
    public class MqttService
    {
        /// <summary>
        /// Funkcia na pripojenie sa na MQTT broker a následné prijímanie a odoberanie správ.
        /// </summary>
        public static async Task SetupAndRunMQTT()
        {

            if (!(await CurrentYear.IsSetUp()))
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

            mqttClient.UseApplicationMessageReceivedHandler(async (e) =>
            {
                try
                {
                    string topic = e.ApplicationMessage.Topic;
                    string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    DrawService.DrawReceivedMessage(topic, message);
                    AttendeeInformationDTO? attendeeInformation = HelperService.ParseMessage(message, topic);

                    if (attendeeInformation is not null)
                    {
                        using var client = new HttpClient();
                        using StringContent jsonContent = new(JsonSerializer.Serialize(attendeeInformation), Encoding.UTF8, "application/json");

                        var response = await client.PostAsync("https://localhost:7044/api/mqtt", jsonContent);

                        Console.WriteLine(JsonSerializer.Serialize(response));

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("ok");
                            await mqttClient.PublishAsync($"{topic}/checkResult", "1");
                        } else
                        {
                            Console.WriteLine("fail");
                            await mqttClient.PublishAsync($"{topic}/checkResult", "0");
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

        /// <summary>
        /// Funkcia, ktorá hovorí čo sa má vykonať po napojení sa na MQTT broker.
        /// </summary>
        public static void OnConnected(MqttClientConnectedEventArgs obj)
        {
            DrawService.DrawSuccessMessage("Successfully connected.");
        }

        /// <summary>
        /// Funkcia, ktorá hovorí čo sa má vykonať pri zlyhaní napojenia sa na MQTT broker.
        /// </summary>
        public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
        {
            DrawService.DrawErrorMessage("Couldn't connect to broker.");
        }

        /// <summary>
        /// Funkcia, ktorá hovorí čo sa má vykonať pri odpojení od MQTT broker-a.
        /// </summary>
        public static void OnDisconnected(MqttClientDisconnectedEventArgs obj)
        {
            DrawService.DrawInfoMessage("Successfully disconnected.");
        }
    }
}
