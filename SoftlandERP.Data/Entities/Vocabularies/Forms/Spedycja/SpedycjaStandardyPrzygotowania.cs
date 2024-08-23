using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja
{
    public class SpedycjaStandardyPrzygotowania : BaseEntity
    {
        [DisplayName("Skrót standardu przygotowania dostawy")]
        public string? AkronimLog { get; set; }

        [DisplayName("Skrót standardu magazynowego")]
        public string? AkronimMag { get; set; }

        [DisplayName("Czas")]
        public TimeSpan Czas { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}
