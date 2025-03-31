using CsvHelper.Configuration.Attributes;

namespace Haxee.Entities.DTOs;

public class AttendeeResultDTO
{
    [Name("Poradie")]
    public int Standing { get; set; }

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
}
