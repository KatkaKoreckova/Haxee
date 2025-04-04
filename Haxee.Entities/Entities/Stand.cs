namespace Haxee.Entities.DTOs
{
    /// <summary>
    /// Databázový objekt stanoviska.
    /// </summary>
    public class Stand : AbstractEntity
    {
        /// <summary>
        /// ID vedúceho stanoviska.
        /// </summary>
        public string? SupervisorId { get; set; }

        /// <summary>
        /// Informácie o vedúcom stanoviska.
        /// </summary>
        public virtual User? Supervisor { get; set; }

        /// <summary>
        /// Názov stanoviska.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Poloha stanoviska.
        /// </summary>
        public required string Location { get; set; }

        /// <summary>
        /// Číslo stanoviska.
        /// </summary>
        public required int Number { get; set; }

        /// <summary>
        /// Počet penalizačných minút pri preskočení stanoviska.
        /// </summary>
        public required TimeSpan Penalty { get; set; }

        /// <summary>
        /// ID aktivity, v ktorej je stanovisko.
        /// </summary>
        public required int ActivityId { get; set; }

        /// <summary>
        /// Udáva či je stanovisko kvízom.
        /// </summary>
        public bool IsQuiz { get; set; }

        /// <summary>
        /// Otázky a odpovede v prípade kvízového stanoviska.
        /// </summary>
        public List<string> QuestionsAndAnswers { get; set; } = new();

        /// <summary>
        /// Maximálna kapacita daného stanoviska.
        /// </summary>
        public required int Capacity { get; set; }

        /// <summary>
        /// Maximálny čas na strávenie na stanovisku.
        /// </summary>
        public required int TimeLimitInMinutes { get; set; }

        /// <summary>
        /// Určuje, či dané stanovisko je štartovné
        /// </summary>
        public bool IsStartingStand { get; set; }

        /// <summary>
        /// Informácie o aktivite.
        /// </summary>
        public virtual Activity Activity { get; set; } = null!;

        /// <summary>
        /// Zoznam návštev stanoviska.
        /// </summary>
        public virtual List<StandVisit> StandVisits { get; set; } = new();

        /// <summary>
        /// Zoznam naplánovaných návštev stanoviska.
        /// </summary>
        public virtual List<ScheduledVisit> ScheduledVisits { get; set; } = new();

        /// <summary>
        /// ID priradeného zariadenia.
        /// </summary>
        public int? DeviceId { get; set; }

        /// <summary>
        /// Priradené zariadenie.
        /// </summary>
        public virtual Device? Device { get; set; }

        public int RemainingCapacity
        {
            get => Capacity - StandVisits.Count - ScheduledVisits.Count;
        }

        public int AlmostEndingVisits
        {
            get =>
                StandVisits
                    .Where(x => x.LeaveTime == null)
                    .Count(y =>
                        (y.StartTime.AddMinutes(TimeLimitInMinutes) - DateTime.Now).TotalSeconds
                        < 60
                    );
        }
    }
}
