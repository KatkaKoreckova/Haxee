namespace Haxee.Entities.DTOs
{
    /// <summary>
    /// Trieda reprezentujúca abstraktnú entitu v databáze.
    /// </summary>
    public abstract class AbstractEntity
    {
        /// <summary>
        /// Primárny kľúč.
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
