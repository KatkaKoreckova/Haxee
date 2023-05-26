namespace Haxee.Entities.DTOs
{
    /// <summary>
    /// Databázový objekt používateľského konta aplikácie.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Meno používateľa.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Dátum narodenia používateľa.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Typ účtu.
        /// </summary>
        public required UserType UserType { get; set; }

        /// <summary>
        /// V prípade inštruktorského konta či má zvýšené práva.
        /// </summary>
        public bool SuperInstructor { get; set; } = false;

        /// <summary>
        /// ID osobnej karty.
        /// </summary>
        public string? CardId { get; set; }

        /// <summary>
        /// Stanovisko na ktorom je vedúci.
        /// </summary>
        public virtual Stand? SupervisorOfStand { get; set; }

        /// <summary>
        /// Zoznam ročníkov súťaže ktorých sa zúčastnil.
        /// </summary>
        public virtual List<Attendee> Attendances { get; set; } = new();
    }
}
