using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Vocabularies.Views.Zatrudnieni
{
    [Area("Administration")]
    public class ViewsZatrudnieniPlecController : BaseController
    {
        public static readonly string Module = "Widok - ";
        public static readonly string Name = "Płeć zatrudnionych";
        public static readonly string ModuleName = Module + Name;

        private readonly XLContext xlContext;

        public ViewsZatrudnieniPlecController(IADRepository adRepository, IRepository<HelperText> helperTextVocabularyRepository, IToastNotification toastNotification, ILogger<BaseController> logger, XLContext xlContext)
            : base(adRepository, helperTextVocabularyRepository, toastNotification, logger)
        {
            this.xlContext = xlContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                this.ViewBag.Title = ModuleName;
                var acronyms = await this.xlContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.ZatrudnieniPlecVocabulary").ToListAsync().ConfigureAwait(true);
                return this.View(acronyms);
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
                var acronyms = await this.xlContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.ZatrudnieniPlecVocabulary").ToListAsync().ConfigureAwait(true);
                return ExcelExporter.Export(acronyms, ModuleName);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
            }
        }
    }
}