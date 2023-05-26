namespace Haxee.Entities.DTOs
{
    /// <summary>
    /// Objekt používaný na prenos dát. MQTT Consumer využíva tento objekt na rozparsovanie správy od MQTT Brokera a jej následné odoslanie na API.
    /// </summary>
    public class AttendeeInformationDTO
    {
        /// <summary>
        /// Identifikačné číslo karty.
        /// </summary>
        public required string CardId { get; set; }
        /// <summary>
        /// Čas, kedy bola priložená.
        /// </summary>
        public required DateTime DateTime { get; set; }

        /// <summary>
        /// Stanovisko, ktoré správu odoslalo.
        /// </summary>
        public required string Trigger { get; set; }
    }
}
