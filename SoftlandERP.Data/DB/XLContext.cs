using Microsoft.EntityFrameworkCore;
using SoftlandERP.Data.Entities.Views;

namespace SoftlandERP.Data.DB
{
    public class XLContext : DbContext
    {
        public XLContext(DbContextOptions<XLContext> options)
            : base(options)
        {
        }

        public DbSet<PraceZleconeDokumenty> PraceZleconeDokumenty { get; set; }

        public DbSet<Cashflow> Cashflow { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Seed();
            modelBuilder.Entity<Cashflow>().Property(e => e.Kwota).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Cashflow>().Property(e => e.Pozostalo).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<PraceZleconeDokumenty>().Property(e => e.Ilosc).HasPrecision(18, 2).HasColumnType("decimal(18, 2)");
        }
    }
}