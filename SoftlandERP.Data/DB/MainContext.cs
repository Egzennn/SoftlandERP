using Microsoft.EntityFrameworkCore;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Forms.KL;
using SoftlandERP.Data.Entities.Forms.Spedycja;
using SoftlandERP.Data.Entities.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Views;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Dokumenty;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Handel;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Oprogramowanie;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Towar;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Data.DB
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationPermissionsRule> ApplicationPermissionRules { get; set; }

        // Formularze
        public DbSet<CashflowForm> CashflowForms { get; set; }

        public DbSet<MPKForm> MPKForms { get; set; }

        public DbSet<PraceZleconeForm> PraceZleconeForms { get; set; }

        // KL
        public DbSet<KLKartaForm> KLKartaForms { get; set; }

        public DbSet<KLKKKForm> KLKKKForms { get; set; }

        public DbSet<KLOsobaForm> KLOsobaForms { get; set; }

        public DbSet<KLRozlForm> KLRozlForms { get; set; }

        public DbSet<KLTerminKorForm> KLTerminKorForms { get; set; }

        public DbSet<KLXMLForm> KLXMLForms { get; set; }

        // Stanowisko
        public DbSet<StanowiskoDOWForm> StanowiskoDOWForms { get; set; }

        public DbSet<StanowiskoListaWypForm> StanowiskoListaWypForms { get; set; }

        public DbSet<StanowiskoListaWypWSZForm> StanowiskoListaWypWSZForms { get; set; }

        public DbSet<StanowiskoObiektyITForm> StanowiskoObiektyITForms { get; set; }

        public DbSet<StanowiskoOdbiorPrzesylekForm> StanowiskoOdbiorPrzesylekForms { get; set; }

        public DbSet<StanowiskoOdpowiedzialnosciForm> StanowiskoOdpowiedzialnosciForms { get; set; }

        public DbSet<StanowiskoOprogramowanieForm> StanowiskoOprogramowanieForms { get; set; }

        public DbSet<StanowiskoSprawyForm> StanowiskoSprawyForms { get; set; }

        // Spedycja
        public DbSet<SpedycjaKonfekcjaForm> SpedycjaKonfekcjaForms { get; set; }

        // Słowniki
        public DbSet<UserAddress> UserAddressVocabulary { get; set; }

        public DbSet<DownloadableForm> DownloadFormVocabulary { get; set; }

        public DbSet<HelperText> HelperTextVocabulary { get; set; }

        // Dokumenty
        public DbSet<DokumentyKosztObslugi> DokumentyKosztObslugiVocabulary { get; set; }

        public DbSet<DokumentyRodzajKosztu> DokumentyRodzajKosztuVocabulary { get; set; }

        public DbSet<DokumentyRodzajPrzesylki> DokumentyRodzajPrzesylkiVocabulary { get; set; }

        public DbSet<DokumentySymbolDokumentu> DokumentySymbolDokumentuVocabulary { get; set; }

        public DbSet<DokumentyTemat> DokumentyTematVocabulary { get; set; }

        public DbSet<DokumentyWnioski> DokumentyWnioskiVocabulary { get; set; }

        // Handel
        public DbSet<HandelGrupa> HandelGrupaVocabulary { get; set; }

        public DbSet<HandelRynek> HandelRynekVocabulary { get; set; }

        // Ogolne
        public DbSet<OgolneCelKontaktu> OgolneCelKontaktuVocabulary { get; set; }

        public DbSet<OgolneKategoriaPracy> OgolneKategoriaPracyVocabulary { get; set; }

        public DbSet<OgolneObszar> OgolneObszarVocabulary { get; set; }

        public DbSet<OgolnePriorytet> OgolnePriorytetVocabulary { get; set; }

        public DbSet<OgolneRecepturyNormWykonawczych> OgolneRecepturyNormWykonawczychVocabulary { get; set; }

        public DbSet<OgolneRodzajPracy> OgolneRodzajPracyVocabulary { get; set; }

        public DbSet<OgolneRola> OgolneRolaVocabulary { get; set; }

        public DbSet<OgolneStan> OgolneStanVocabulary { get; set; }

        public DbSet<OgolneStatus> OgolneStatusVocabulary { get; set; }

        // Oprogramowanie
        public DbSet<OprogramowanieGrupaUslug> OprogramowanieGrupaUslugVocabulary { get; set; }

        public DbSet<OprogramowanieInstalacja> OprogramowanieInstalacjaVocabulary { get; set; }

        public DbSet<OprogramowanieObiektIT> OprogramowanieObiektITVocabulary { get; set; }

        public DbSet<OprogramowanieRola> OprogramowanieRolaVocabulary { get; set; }

        public DbSet<OprogramowanieTyp> OprogramowanieTypVocabulary { get; set; }

        // Spedycja
        public DbSet<SpedycjaAkronimCzynnosciPakowania> SpedycjaAkronimCzynnosciPakowaniaVocabulary { get; set; }

        public DbSet<SpedycjaAkronimDokumentu> SpedycjaAkronimDokumentuVocabulary { get; set; }

        public DbSet<SpedycjaCzynnosci> SpedycjaCzynnosciVocabulary { get; set; }

        public DbSet<SpedycjaCzynnosciMagazynowoSpedycyjneLog> SpedycjaCzynnosciMagazynowoSpedycyjneLogVocabulary { get; set; }

        public DbSet<SpedycjaCzynnosciMagazynowoSpedycyjneMag> SpedycjaCzynnosciMagazynowoSpedycyjneMagVocabulary { get; set; }

        public DbSet<SpedycjaPlatnik> SpedycjaPlatnikVocabulary { get; set; }

        public DbSet<SpedycjaPrzewoznik> SpedycjaPrzewoznikVocabulary { get; set; }

        public DbSet<SpedycjaRodzajDokumentu> SpedycjaRodzajDokumentuVocabulary { get; set; }

        public DbSet<SpedycjaStandardyPrzygotowania> SpedycjaStandardyPrzygotowaniaVocabulary { get; set; }

        public DbSet<SpedycjaSekcjeTowarow> SpedycjaSekcjeTowarowVocabulary { get; set; }

        public DbSet<SpedycjaStatus> SpedycjaStatusVocabulary { get; set; }

        // Stanowisko
        public DbSet<StanowiskoGrupaNazwaOprogramowania> StanowiskoGrupaNazwaOprogramowaniaVocabulary { get; set; }

        public DbSet<StanowiskoGrupaOprogramowania> StanowiskoGrupaOprogramowaniaVocabulary { get; set; }

        public DbSet<StanowiskoNazwaOdpowiedzialnosci> StanowiskoNazwaOdpowiedzialnosciVocabulary { get; set; }

        public DbSet<StanowiskoObiekt> StanowiskoObiektVocabulary { get; set; }

        public DbSet<StanowiskoRodzajePrzesylek> StanowiskoRodzajePrzesylekVocabulary { get; set; }

        public DbSet<StanowiskoRodzajeWyp> StanowiskoRodzajeWypVocabulary { get; set; }

        public DbSet<StanowiskoZdarzenie> StanowiskoZdarzenieVocabulary { get; set; }

        // Towar
        public DbSet<TowarSekcje> TowarSekcjeVocabulary { get; set; }

        public DbSet<TowarSekcjeParametry> TowarSekcjeParametryVocabulary { get; set; }

        public DbSet<TowarSekcjeParametryWartosc> TowarSekcjeParametryWartoscVocabulary { get; set; }

        // Widoki
        public DbSet<SpedycjaKonfekcjaSekcje> SpedycjaKonfekcjaSekcje { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Seed();
            modelBuilder.Entity<MPKForm>().Property(e => e.Kwota).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<CashflowForm>().Property(e => e.Kwota).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<CashflowForm>().Property(e => e.Pozostalo).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<KLTerminKorForm>().Property(e => e.Kwota).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<OgolneRecepturyNormWykonawczych>().Property(e => e.Kwota).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<PraceZleconeForm>().Property(e => e.WartoscPracyZleconej).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
        }
    }
}