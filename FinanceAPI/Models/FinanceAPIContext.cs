using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Models
{
    public class FinanceAPIContext : DbContext
    {
        public FinanceAPIContext(DbContextOptions<FinanceAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Year> Years { get; set; }
    }
}