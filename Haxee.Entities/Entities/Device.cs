namespace Haxee.Entities.Entities
{
    public class Device : AbstractEntity
    {
        /// <summary>
        /// Čítateľný názov zariadenia.
        /// </summary>
        public required string Name { get; set; }
        
        /// <summary>
        /// Identifikátor zariadenia (hardvér).
        /// </summary>
        public required string Identifier { get; set; }

        /// <summary>
        /// ID priradeného stanoviska.
        /// </summary>
        public int? StandId { get; set; }

        /// <summary>
        /// Priradené stanovisko.
        /// </summary>
        public virtual Stand? Stand { get; set; }
    }
}
