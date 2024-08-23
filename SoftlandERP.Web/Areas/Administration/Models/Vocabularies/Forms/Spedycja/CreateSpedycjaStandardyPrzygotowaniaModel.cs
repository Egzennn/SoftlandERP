using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;

namespace SoftlandERP.Web.Areas.Administration.Models.Vocabularies.Forms.Spedycja
{
    public class CreateSpedycjaStandardyPrzygotowaniaModel
    {
        public SpedycjaStandardyPrzygotowania SpedycjaStandardyPrzygotowania { get; set; }

        public SpedycjaCzynnosciMagazynowoSpedycyjneLog SpedycjaCzynnosciMagazynowoSpedycyjneLog { get; set; }

        public SpedycjaCzynnosciMagazynowoSpedycyjneMag SpedycjaCzynnosciMagazynowoSpedycyjneMag { get; set; }
    }
}
