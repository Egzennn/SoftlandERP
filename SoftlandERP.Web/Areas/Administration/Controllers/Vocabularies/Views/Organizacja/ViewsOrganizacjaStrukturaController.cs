using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Areas.Administration.Models.Views.Organizacja;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Vocabularies.Views.Organizacja
{
    [Area("Administration")]
    public class ViewsOrganizacjaStrukturaController : BaseController
    {
        public static readonly string Module = "Widok - ";
        public static readonly string Name = "Struktura firmy";
        public static readonly string ModuleName = Module + Name;

        private readonly OptimaContext optimaContext;

        public ViewsOrganizacjaStrukturaController(IADRepository adRepository, IRepository<HelperText> helperTextVocabularyRepository, IToastNotification toastNotification, ILogger<BaseController> logger, OptimaContext optimaContext)
            : base(adRepository, helperTextVocabularyRepository, toastNotification, logger)
        {
            this.optimaContext = optimaContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                this.ViewBag.Title = ModuleName;

                var model = new StructuraViewModel { StructuraList = new List<Structura>() };

                var first = await this.optimaContext.Database.SqlQuery<string>($"SELECT Wartosc as KOD FROM UDBS_Slownik.dbo.OrganizacjaStrukturaVocabulary").ToListAsync().ConfigureAwait(true);
                var second = await this.optimaContext.Database.SqlQuery<string>($"SELECT Wartosc1 as KOD1 FROM UDBS_Slownik.dbo.OrganizacjaStrukturaVocabulary").ToListAsync().ConfigureAwait(true);
                var third = await this.optimaContext.Database.SqlQuery<string>($"SELECT Wartosc2 as KOD2 FROM UDBS_Slownik.dbo.OrganizacjaStrukturaVocabulary").ToListAsync().ConfigureAwait(true);

                for (int i = 0; i < first.Count; i++)
                {
                    model.StructuraList.Add(new Structura { First = first[i], Second = second[i], Third = third[i] });
                }

                return this.View(model);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.View(new List<string>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            try
            {
                var model = new StructuraViewModel { StructuraList = new List<Structura>() };

                var first = await this.optimaContext.Database.SqlQuery<string>($"SELECT Wartosc as KOD FROM UDBS_Slownik.dbo.OrganizacjaStrukturaVocabulary").ToListAsync().ConfigureAwait(true);
                var second = await this.optimaContext.Database.SqlQuery<string>($"SELECT Wartosc1 as KOD1 FROM UDBS_Slownik.dbo.OrganizacjaStrukturaVocabulary").ToListAsync().ConfigureAwait(true);
                var third = await this.optimaContext.Database.SqlQuery<string>($"SELECT Wartosc2 as KOD2 FROM UDBS_Slownik.dbo.OrganizacjaStrukturaVocabulary").ToListAsync().ConfigureAwait(true);

                for (int i = 0; i < first.Count; i++)
                {
                    model.StructuraList.Add(new Structura { First = first[i], Second = second[i], Third = third[i] });
                }

                return ExcelExporter.Export(model.StructuraList, ModuleName);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
            }
        }
    }
}