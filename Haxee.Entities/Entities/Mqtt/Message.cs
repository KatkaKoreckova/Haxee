using System.Text.Json.Serialization;

namespace Haxee.Entities.Entities.Mqtt
{
    public class Message
    {
        [JsonPropertyName("channel")]
        public int Channel { get; set; }

        [JsonPropertyName("from")]
        public long From { get; set; }

        [JsonPropertyName("hop_start")]
        public int Hop_start { get; set; }

        [JsonPropertyName("hops_away")]
        public int Hops_away { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("payload")]
        public Payload? Payload { get; set; }

        [JsonPropertyName("rssi")]
        public int Rssi { get; set; }

        [JsonPropertyName("sender")]
        public string Sender { get; set; } = string.Empty;

        [JsonPropertyName("snr")]
        public double Snr { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("to")]
        public long To { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
