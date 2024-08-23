﻿using Microsoft.AspNetCore.Mvc;
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
    public class ViewsZatrudnieniAkronimZatrudnionyController : BaseController
    {
        public static readonly string Module = "Widok - ";
        public static readonly string Name = "Akronimy zatrudnionych";
        public static readonly string ModuleName = Module + Name;

        private readonly OptimaContext optimaContext;

        public ViewsZatrudnieniAkronimZatrudnionyController(IADRepository adRepository, IRepository<HelperText> helperTextVocabularyRepository, IToastNotification toastNotification, ILogger<BaseController> logger, OptimaContext optimaContext)
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
                var acronyms = await this.optimaContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.ZatrudnieniAkronimVocabulary").ToListAsync().ConfigureAwait(true);
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
                var acronyms = await this.optimaContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.ZatrudnieniAkronimVocabulary").ToListAsync().ConfigureAwait(true);
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