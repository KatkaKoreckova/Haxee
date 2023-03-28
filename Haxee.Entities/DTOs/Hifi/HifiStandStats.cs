namespace Haxee.Entities.DTOs.Hifi
{
    public class HifiStandStats : AbstractEntity
    {
        public TimeSpan WaitTime { get; set; }
        public TimeSpan WorkTime { get; set; }
        public int TaskSkipedCount { get; set; }
        public int TaskNotFinishedCount { get; set; }
        public int TaskFinishedCount { get; set; }

    }
}
