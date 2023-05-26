namespace Haxee.Entities.Entities
{
    /// <summary>
    /// Databázový objekt návštevy stanoviska.
    /// </summary>
    public class StandVisit : AbstractEntity
    {

        /// <summary>
        /// ID stanmoviska.
        /// </summary>
        public required int StandId { get; set; }

        /// <summary>
        /// Informácie o stanovisku.
        /// </summary>
        public virtual Stand Stand { get; set; } = null!;

        /// <summary>
        /// ID návštevníka stanoviska (súťažiaci).
        /// </summary>
        public required int AttendeeId { get; set; }

        /// <summary>
        /// Informácie o návštevníkovi stanoviska.
        /// </summary>
        public virtual Attendee Attendee { get; set; } = null!;

        /// <summary>
        /// Štádium návštevy stanoviska.
        /// </summary>
        public required StandVisitStatus Status { get; set; }

        /// <summary>
        /// Počet penalizačných minút za stanovisko.
        /// </summary>
        public TimeSpan Penalty { get; set; }

        /// <summary>
        /// Či preskočil stanovisko.
        /// </summary>
        public bool Skipped { get; set; }

        /// <summary>
        /// Čas kedy na stanovisko prišiel.
        /// </summary>
        public required DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Čas kedy ukončil čakanie na stanovisku.
        /// </summary>
        public DateTime? EndWaitTime { get; set; }

        /// <summary>
        /// Čas kedy opustil stanovisko.
        /// </summary>
        public DateTime? LeaveTime { get; set; }
    }
}
