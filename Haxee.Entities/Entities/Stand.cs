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
        /// ID roku v ktorom je stanovisko.
        /// </summary>
        public required int YearId { get; set; }
      
        /// <summary>
        /// Udáva či je stanovisko kvízom.
        /// </summary>
        public bool IsQuiz { get; set; }
       
        /// <summary>
        /// Otázky a odpovede v prípade kvízového stanoviska.
        /// </summary>
        public List<string> QuestionsAndAnswers { get; set; } = new();

        public required int Capacity { get; set; }

        /// <summary>
        /// Informácie o roku v ktorom je súťaž.
        /// </summary>
        public virtual Year Year { get; set; } = null!;
      
        /// <summary>
        /// Zoznam návštev stanoviska.
        /// </summary>
        public virtual List<StandVisit> StandVisits { get; set; } = new();
    }
}
