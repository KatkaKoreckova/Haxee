using System.Text.Json.Serialization;

namespace Haxee.Entities.Entities.Mqtt
{
    public class Payload
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}
