using System.Net.Http.Json;

namespace Haxee.Entities.Entities.Mqtt
{
    /// <summary>
    /// Objekt slúžiaci na prácu s konfiguračnými dátami aktivity pre aplikáciu MQTT Consumer. Tieto dáta slúžia na pripojenie na MQTT broker, prihlásenie na odoberanie tém a publikovanie tém.
    /// </summary>
    public class ActivityConfiguration
    {
        /// <summary>
        /// IP adresa MQTT broker-a.
        /// </summary>
        public string BrokerIp { get; set; } = string.Empty;

        /// <summary>
        /// Číslo portu, na ktorom je spustený.
        /// </summary>
        public int BrokerPort { get; set; }

        /// <summary>
        /// Meno, pod akým sa alikácia MQTT Consumer prezentuje MQTT broker-ovi.
        /// </summary>
        public string ClientName { get; set; } = "MQTTConsumer";

        /// <summary>
        /// Téma, pod ktorou sú v danom roku posielané dáta.
        /// </summary>
        public string GlobalTopic { get; set; } = string.Empty;

        /// <summary>
        /// ID, podľa ktorého napárovať konfiguráciu k aktivite.
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// Inštancia konfigurácie
        /// </summary>

        private static ActivityConfiguration? _instance = null;


        /// <summary>
        /// Konštruktor na vytvorenie konfigurácie
        /// </summary>
        public ActivityConfiguration() { }

        /// <summary>
        /// Funkcia, ktorá vráti aktuálnu konfiguráciu.
        /// </summary>
        /// <returns>Inštanciu triedy</returns>
        public static ActivityConfiguration GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ActivityConfiguration();
            }
            return _instance;
        }

        /// <summary>
        /// Funkcia, ktorá slúži na overenie toho, či v databáze existuje konfigurácia pre daný ročník.
        /// </summary>
        /// <returns> True / false podľa toho či konfigurácia existuje alebo nie.</returns>
        public static async Task<bool> IsSetUp()
        {
            var instance = GetInstance();

            if (instance.Id != -1)
                return true;

            using var client = new HttpClient();

            var response = await client.GetAsync($"{Constants.Mqtt.API_URL}api/setup/{instance.Id}");

            if (!response.IsSuccessStatusCode)
                return false;

            var currentActivity = await response.Content.ReadFromJsonAsync<Activity>();

            if (currentActivity is null)
                return false;

            instance.BrokerIp = currentActivity.BrokerIp ?? string.Empty;
            instance.BrokerPort = currentActivity.BrokerPort ?? 0;
            instance.GlobalTopic = currentActivity.GlobalTopic ?? string.Empty;

            return true;
        }

        /// <summary>
        /// Vymazanie aktuálnej konfigurácie.
        /// </summary>
        public static async Task ClearAsync()
        {
            var instance = GetInstance();

            if (instance.Id == -1)
                return;

            using var client = new HttpClient();
            var _ = await client.DeleteAsync($"{Constants.Mqtt.API_URL}api/setup/{instance.Id}");

            _instance = null;
        }
    }
}
