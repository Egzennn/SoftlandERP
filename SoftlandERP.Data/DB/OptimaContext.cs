using Microsoft.EntityFrameworkCore;

namespace SoftlandERP.Data.DB
{
    public class OptimaContext : DbContext
    {
        public OptimaContext(DbContextOptions<OptimaContext> options)
            : base(options)
        {
        }
    }
}