using CsvHelper.Configuration.Attributes;

namespace Haxee.Entities.DTOs;

public class AttendeeResultDTO
{
    [Name("Poradie")]
    public int ImportedStanding { get; set; }

    [Name("Krstné meno")]
    public string? Firstname { get; set; }

    [Name("Priezvisko")]
    public string? Surname { get; set; }

    [Name("Vek")]
    public int Age { get; set; }

    [Name("Štart")]
    public DateTime Start { get; set; }

    [Name("Cieľ")]
    public DateTime Finish { get; set; }

    [Name("Trvanie")]
    public TimeSpan Duration { get; set; }

    [Ignore]
    public Dictionary<int, int> WaitingPeriods { get; set; } = new();

    [Ignore]
    public Dictionary<int, int> PenaltyPeriods { get; set; } = new();

    [Ignore]
    private TimeSpan? _calculatedTotalTime;

    [Ignore]
    public int? FinalStanding { get; set; }

    [Ignore]
    public double? FinalPoints { get; set; }

    [Ignore]
    public double? FinalPercentile { get; set; }

    public TimeSpan GetCalculatedTotalTime()
    {
        if (_calculatedTotalTime == null)
        {
            _calculatedTotalTime = Duration;

            foreach (var waitingPeriod in WaitingPeriods)
            {
                _calculatedTotalTime -= TimeSpan.FromMinutes(waitingPeriod.Value);
            }

            foreach (var penaltyPeriod in PenaltyPeriods)
            {
                _calculatedTotalTime += TimeSpan.FromMinutes(penaltyPeriod.Value);
            }
        }

        return _calculatedTotalTime.Value;
    }
}
