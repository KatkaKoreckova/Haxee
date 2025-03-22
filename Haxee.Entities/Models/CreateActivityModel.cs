using Haxee.Entities.Validations;

namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model, do ktorého sa mapujú dáta pri vytváraní aktivity.
    /// </summary>
    public class CreateActivityModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Názov")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "IP MQTT brokera")]
        [IpAddress]
        public string? BrokerIp { get; set; }

        [Display(Name = "Port MQTT brokera")]
        [Range(1, 49151)]
        public int? BrokerPort { get; set; } = 1883;

        [Display(Name = "MQTT globálny topic")]
        [MqttTopic]
        public string? GlobalTopic { get; set; }
    }
}
