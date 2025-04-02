namespace Haxee.Entities.Entities
{
    public class ScheduledVisit : AbstractEntity
    {
        /// <summary>
        /// ID stanoviska.
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
    }
}
