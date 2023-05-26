namespace Haxee.Entities.Entities
{
    /// <summary>
    /// Databázový objekt súťažiaceho.
    /// </summary>
    public class Attendee : AbstractEntity
    {
        /// <summary>
        /// Čas kedy súťaž odštartoval.
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Čas kedy súťaž ukončil.
        /// </summary>
        public DateTime? EndedAt { get; set; }

        /// <summary>
        /// ID užívateľa aplikácie ktorý je týmto súťažiacim.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// ID karty s ktorou súťaží.
        /// </summary>
        public string? CardId { get; set; }

        /// <summary>
        /// ID ročníka súťaže v ktorom súťaží.
        /// </summary>
        public required int YearId { get; set; }

        /// <summary>
        /// Informácie o roku v ktorom je súťaž.
        /// </summary>
        public virtual Year Year { get; set; } = null!;
        
        /// <summary>
        /// Informácie o súťažiacom
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Zoznam stanovísk ktoré navštívil
        /// </summary>
        public virtual List<StandVisit> StandVisits { get; set; } = new();

        /// <summary>
        /// Funkcia ktorá vracia čas od zaČiatku súťaženia po koniec.
        /// </summary>
        public TimeSpan? GetTime()
            => StartedAt is null ? null : (EndedAt is null ? DateTime.Now - StartedAt : EndedAt - StartedAt);
    }
}
