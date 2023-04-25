namespace Haxee.Entities.DTOs
{
    public abstract class AbstractEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
