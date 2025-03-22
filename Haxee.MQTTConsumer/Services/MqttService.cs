using Haxee.Entities;
using Haxee.Entities.Entities.Mqtt;
using System.Net.Http.Json;
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
            string? input = null;
            Activity? activity = null;
            using var client = new HttpClient();

            do
            {
                Console.Clear();
                Console.WriteLine("Activity ID:");
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    continue;

                if (!int.TryParse(input, out int activityId))
                {
                    DrawService.DrawErrorMessage("You must input a number.");
                    continue;
                }

                var response = await client.GetAsync($"{Constants.Mqtt.API_URL}api/setup/{activityId}");

                if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    DrawService.DrawErrorMessage($"There was an error with connecting to the web app ({response.StatusCode}).");
                    continue;
                }

                try
                {
                    activity = await response.Content.ReadFromJsonAsync<Activity>();
                }
                catch
                {
                    DrawService.DrawErrorMessage($"Activity with the ID {activityId} was not found.");
                    continue;
                }

                if (activity is null || string.IsNullOrEmpty(activity.BrokerIp) || !activity.BrokerPort.HasValue || string.IsNullOrEmpty(activity.GlobalTopic))
                {
                    activity = null;
                    DrawService.DrawErrorMessage($"Activity with the ID {activityId} is missing key data, please finish configuring it in the web app.");
                    continue;
                }
            } while (activity is null);

            Console.Clear();

            DrawService.DrawInfoMessage("Connecting to broker ...\n");

            // Creates a new client
            MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                        .WithClientId(activity.Name)
                                        .WithTcpServer(activity.BrokerIp, activity.BrokerPort);

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
                .WithTopic(activity.GlobalTopic)
                .Build()).GetAwaiter().GetResult();

            mqttClient.UseApplicationMessageReceivedHandler(async (e) =>
            {
                try
                {
                    string receivedMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                    var parsedMessage = JsonSerializer.Deserialize<Message>(receivedMessage);

                    if (parsedMessage is not null)
                    {
                        string message = parsedMessage.Payload.Text;
                        string topic = parsedMessage.From.ToString();
                        DrawService.DrawReceivedMessage(topic, message);
                        AttendeeInformationDTO? attendeeInformation = HelperService.ParseMessage(message, topic);

                        if (attendeeInformation is not null)
                        {
                            using var client = new HttpClient();
                            using StringContent jsonContent = new(JsonSerializer.Serialize(attendeeInformation), Encoding.UTF8, "application/json");

                            var response = await client.PostAsync($"{Constants.Mqtt.API_URL}api/mqtt", jsonContent);

                            Console.WriteLine(JsonSerializer.Serialize(response));

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("ok");
                                await mqttClient.PublishAsync($"{topic}/checkResult", "1");
                            }
                            else
                            {
                                Console.WriteLine("fail");
                                await mqttClient.PublishAsync($"{topic}/checkResult", "0");
                            }
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
