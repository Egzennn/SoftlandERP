using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Serilog;
using SoftlandERP.Core.Repositories;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Core.Repositories.Interfaces.Ogolne;
using SoftlandERP.Core.Repositories.Interfaces.Spedycja;
using SoftlandERP.Core.Repositories.Interfaces.Stanowisko;
using SoftlandERP.Core.Repositories.Ogolne;
using SoftlandERP.Core.Repositories.Spedycja;
using SoftlandERP.Core.Repositories.Stanowisko;
using SoftlandERP.Data.Configurations;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Forms.KL;
using SoftlandERP.Data.Entities.Forms.Spedycja;
using SoftlandERP.Data.Entities.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Dokumenty;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Handel;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Oprogramowanie;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions { NewestOnTop = true, ProgressBar = true, TimeOut = 3000, PositionClass = ToastPositions.TopRight });

builder.Services.Configure<ADConfiguration>(builder.Configuration.GetRequiredSection("ADConfiguration"));

builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));
builder.Services.AddDbContext<OptimaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OptimaConnection")));
builder.Services.AddDbContext<XLContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("XLConnection")));

builder.Services.AddTransient<IADRepository, ADRepository>();

// Repozytoria
builder.Services.AddTransient<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddTransient<ISekcjaTowarRepository, SekcjaTowarRepository>();
builder.Services.AddTransient<IStandardyPrzygotowaniaRepository, StandardyPrzygotowaniaRepository>();
builder.Services.AddTransient<ICzynnosciMagazynowoSpedycyjneLogRepository, CzynnosciMagazynowoSpedycyjneLogRepository>();
builder.Services.AddTransient<ICzynnosciMagazynowoSpedycyjneMagRepository, CzynnosciMagazynowoSpedycyjneMagRepository>();
builder.Services.AddTransient<IOprogramowanieRepository, OprogramowanieRepository>();
builder.Services.AddTransient<IRecepturyNormWykonawczychRepository, RecepturyNormWykonawczychRepository>();

// Formularze
builder.Services.AddTransient<IRepository<MPKForm>, Repository<MPKForm>>();
builder.Services.AddTransient<IRepository<CashflowForm>, Repository<CashflowForm>>();
builder.Services.AddTransient<IRepository<KLKartaForm>, Repository<KLKartaForm>>();
builder.Services.AddTransient<IRepository<KLKKKForm>, Repository<KLKKKForm>>();
builder.Services.AddTransient<IRepository<KLOsobaForm>, Repository<KLOsobaForm>>();
builder.Services.AddTransient<IRepository<KLRozlForm>, Repository<KLRozlForm>>();
builder.Services.AddTransient<IRepository<KLTerminKorForm>, Repository<KLTerminKorForm>>();
builder.Services.AddTransient<IRepository<KLXMLForm>, Repository<KLXMLForm>>();
builder.Services.AddTransient<IRepository<StanowiskoDOWForm>, Repository<StanowiskoDOWForm>>();
builder.Services.AddTransient<IRepository<StanowiskoListaWypForm>, Repository<StanowiskoListaWypForm>>();
builder.Services.AddTransient<IRepository<StanowiskoListaWypWSZForm>, Repository<StanowiskoListaWypWSZForm>>();
builder.Services.AddTransient<IRepository<StanowiskoObiektyITForm>, Repository<StanowiskoObiektyITForm>>();
builder.Services.AddTransient<IRepository<StanowiskoOdbiorPrzesylekForm>, Repository<StanowiskoOdbiorPrzesylekForm>>();
builder.Services.AddTransient<IRepository<StanowiskoOdpowiedzialnosciForm>, Repository<StanowiskoOdpowiedzialnosciForm>>();
builder.Services.AddTransient<IRepository<StanowiskoOprogramowanieForm>, Repository<StanowiskoOprogramowanieForm>>();
builder.Services.AddTransient<IRepository<StanowiskoSprawyForm>, Repository<StanowiskoSprawyForm>>();
builder.Services.AddTransient<IRepository<SpedycjaKonfekcjaForm>, Repository<SpedycjaKonfekcjaForm>>();
builder.Services.AddTransient<IRepository<PraceZleconeForm>, Repository<PraceZleconeForm>>();

// Słowniki
builder.Services.AddTransient<IRepository<ApplicationPermissionsRule>, Repository<ApplicationPermissionsRule>>();
builder.Services.AddTransient<IRepository<DownloadableForm>, Repository<DownloadableForm>>();
builder.Services.AddTransient<IRepository<ApplicationPermissionsRule>, Repository<ApplicationPermissionsRule>>();
builder.Services.AddTransient<IRepository<HelperText>, Repository<HelperText>>();
builder.Services.AddTransient<IRepository<DokumentyKosztObslugi>, Repository<DokumentyKosztObslugi>>();
builder.Services.AddTransient<IRepository<DokumentyRodzajKosztu>, Repository<DokumentyRodzajKosztu>>();
builder.Services.AddTransient<IRepository<HandelGrupa>, Repository<HandelGrupa>>();
builder.Services.AddTransient<IRepository<HandelRynek>, Repository<HandelRynek>>();
builder.Services.AddTransient<IRepository<OgolneCelKontaktu>, Repository<OgolneCelKontaktu>>();
builder.Services.AddTransient<IRepository<OgolneKategoriaPracy>, Repository<OgolneKategoriaPracy>>();
builder.Services.AddTransient<IRepository<OgolneObszar>, Repository<OgolneObszar>>();
builder.Services.AddTransient<IRepository<OgolnePriorytet>, Repository<OgolnePriorytet>>();
builder.Services.AddTransient<IRepository<OgolneRecepturyNormWykonawczych>, Repository<OgolneRecepturyNormWykonawczych>>();
builder.Services.AddTransient<IRepository<OgolneRodzajPracy>, Repository<OgolneRodzajPracy>>();
builder.Services.AddTransient<IRepository<OgolneRola>, Repository<OgolneRola>>();
builder.Services.AddTransient<IRepository<OgolneStan>, Repository<OgolneStan>>();
builder.Services.AddTransient<IRepository<OgolneStatus>, Repository<OgolneStatus>>();
builder.Services.AddTransient<IRepository<OprogramowanieGrupaUslug>, Repository<OprogramowanieGrupaUslug>>();
builder.Services.AddTransient<IRepository<OprogramowanieRola>, Repository<OprogramowanieRola>>();
builder.Services.AddTransient<IRepository<OprogramowanieObiektIT>, Repository<OprogramowanieObiektIT>>();
builder.Services.AddTransient<IRepository<OprogramowanieInstalacja>, Repository<OprogramowanieInstalacja>>();
builder.Services.AddTransient<IRepository<SpedycjaCzynnosci>, Repository<SpedycjaCzynnosci>>();
builder.Services.AddTransient<IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneLog>, Repository<SpedycjaCzynnosciMagazynowoSpedycyjneLog>>();
builder.Services.AddTransient<IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneMag>, Repository<SpedycjaCzynnosciMagazynowoSpedycyjneMag>>();
builder.Services.AddTransient<IRepository<SpedycjaSekcjeTowarow>, Repository<SpedycjaSekcjeTowarow>>();
builder.Services.AddTransient<IRepository<SpedycjaStandardyPrzygotowania>, Repository<SpedycjaStandardyPrzygotowania>>();
builder.Services.AddTransient<IRepository<StanowiskoGrupaNazwaOprogramowania>, Repository<StanowiskoGrupaNazwaOprogramowania>>();
builder.Services.AddTransient<IRepository<StanowiskoGrupaOprogramowania>, Repository<StanowiskoGrupaOprogramowania>>();
builder.Services.AddTransient<IRepository<StanowiskoNazwaOdpowiedzialnosci>, Repository<StanowiskoNazwaOdpowiedzialnosci>>();
builder.Services.AddTransient<IRepository<StanowiskoObiekt>, Repository<StanowiskoObiekt>>();
builder.Services.AddTransient<IRepository<StanowiskoRodzajePrzesylek>, Repository<StanowiskoRodzajePrzesylek>>();
builder.Services.AddTransient<IRepository<StanowiskoRodzajeWyp>, Repository<StanowiskoRodzajeWyp>>();
builder.Services.AddTransient<IRepository<StanowiskoZdarzenie>, Repository<StanowiskoZdarzenie>>();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionHandler("/Error/Exception");
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseStaticFiles();

app.UseRouting();

app.UseNToastNotify();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();