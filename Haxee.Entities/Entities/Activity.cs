using System.Diagnostics;

namespace Haxee.Entities.Entities
{
    /// <summary>
    /// Databázový objekt dát o danej aktivite.
    /// </summary>
    public class Activity : AbstractEntity
    {
        /// <summary>
        /// Názov aktivity.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// IP adresa MQTT broker-a.
        /// </summary>
        public string? BrokerIp { get; set; }

        /// <summary>
        /// Číslo portu na ktorom je spustený.
        /// </summary>
        public int? BrokerPort { get; set; }

        /// <summary>
        /// Téma, pod ktorou u v danom roku posielané dáta.
        /// </summary>
        public string? GlobalTopic { get; set; }

        /// <summary>
        /// Zoznam stanovísk.
        /// </summary>
        public virtual List<Stand> Stands { get; set; } = new();

        /// <summary>
        /// Zoznam účastníkov.
        /// </summary>
        public virtual List<Attendee> Attendees { get; set; } = new();

        /// <summary>
        /// Informácia o tom či sa aktivita odohrala, neodohrala alebo sa práve odohráva.
        /// </summary>
        public ActivityStatus Status { get; set; } = ActivityStatus.Pending;

        public void AddDefaultStand()
        {
            Stands.Add(new()
            {
                Name = "Štart",
                Location = string.Empty,
                Number = 0,
                Penalty = TimeSpan.Zero,
                ActivityId = Id,
                Capacity = Constants.Limits.MAX_PEOPLE,
                IsStartingStand = true,
            });
        }
    }
}
