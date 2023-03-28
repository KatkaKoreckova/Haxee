namespace Haxee.Entities.DTOs.Hifi
{
    public class HifiYear : AbstractEntity
    {
        public required int Year { get; set; }
        public List<Stand> Stands { get; set; } = new();
        public Status Status { get; set; } = Status.None;
        public int Waiting { get; set; }
        public int Working { get; set; }
        public int Done { get; set; }
    }
}
