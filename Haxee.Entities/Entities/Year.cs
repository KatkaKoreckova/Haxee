namespace Haxee.Entities.Entities
{
    /// <summary>
    /// Databázový objektdát o ročníku súťaže.
    /// </summary>
    public class Year : AbstractEntity
    {
        /// <summary>
        /// Rok v ktorom sa súťaž odohráva.
        /// </summary>
        public required int YearValue { get; set; }

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
        /// Informácia o tom či ročník sa odohral, neodohral alebo sa odohráva.
        /// </summary>
        public YearStatus Status { get; set; } = YearStatus.Pending;
    }
}
