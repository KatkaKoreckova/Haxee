using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

namespace Haxee.Entities.Entities.Mqtt
{
    /// <summary>
    /// Objekt slúžiaci na uchovávanie konfiguračných dát ročníka súťaže pre aplikáciu MQTT Consumer. Tieto dáta slúžia na pripojenie na MQTT broker, prihlásenie na odoberanie tém a publikovanie tém.
    /// </summary>
    public class CurrentYear
    {
        /// <summary>
        /// IP adresa MQTT broker-a.
        /// </summary>
        public string BrokerIP { get; set; } = string.Empty;

        /// <summary>
        /// Číslo portu na ktorom je spustený.
        /// </summary>
        public int BrokerPort { get; set; }

        /// <summary>
        /// Meno, pod akým sa alikácia MQTT Consumer prezentuje MQTT broker-u.
        /// </summary>
        public string ClientName { get; set; } = "MQTTConsumer";

        /// <summary>
        /// Téma, pod ktorou u v danom roku posielané dáta.
        /// </summary>
        public string GlobalTopic { get; set; } = string.Empty;

        /// <summary>
        /// Rok, v ktorom sa súťaž odohráva.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Inštancia konfigurácie
        /// </summary>

        private static CurrentYear? _instance = null;


        /// <summary>
        /// Konštruktor na vytvorenie konfigurácie
        /// </summary>
        public CurrentYear() { }

        /// <summary>
        /// Funkcia, ktorá vráti aktuálnu konfiguráciu.
        /// </summary>
        /// <returns>Inštanciu triedy</returns>
        public static CurrentYear GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CurrentYear();
            }
            return _instance;
        }

        /// <summary>
        /// Funkcia, ktorá slúži na overenie toho, či v databáze exstuje konfigurácia pre daný ročník.
        /// </summary>
        /// <returns> True / false podľa toho či konfigurácia existuje alebo nie.</returns>
        public static async Task<bool> IsSetUp()
        {
            var instance = GetInstance();

            if (instance.Year != 0)
                return true;

            using var client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7044/api/setup");

            if (!response.IsSuccessStatusCode)
                return false;

            var currentYear = await response.Content.ReadFromJsonAsync<Year>();

            if (currentYear is null)
                return false;

            instance.BrokerIP = currentYear.BrokerIp ?? string.Empty;
            instance.BrokerPort = currentYear.BrokerPort ?? 0;
            instance.GlobalTopic = currentYear.GlobalTopic ?? string.Empty;
            instance.Year = currentYear.YearValue;

            return true;
        }

        /// <summary>
        /// Vymazanie aktuálnej konfigurácie.
        /// </summary>
        public static void Clear()
        {
            _instance = null;
        }
    }
}
