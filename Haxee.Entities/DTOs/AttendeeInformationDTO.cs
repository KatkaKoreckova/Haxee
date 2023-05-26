namespace Haxee.Entities.DTOs
{
    /// <summary>
    /// Objekt pre pohyb dát. MQTT Consumer využíva tento objekt na rozparsovanie správy od MQTT Brokera a jej následné odoslanie na API.
    /// </summary>
    public class AttendeeInformationDTO
    {
        /// <summary>
        /// Idnetifikačné číslo karty.
        /// </summary>
        public required string CardId { get; set; }
        /// <summary>
        /// čas, kedy bola priložená.
        /// </summary>
        public required DateTime DateTime { get; set; }

        /// <summary>
        /// Stanovisko, ktoré správu odoslalo.
        /// </summary>
        public required string Trigger { get; set; }
    }
}
