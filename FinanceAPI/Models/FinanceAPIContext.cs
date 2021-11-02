using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Models
{
  public class FinanceAPIContext : DbContext
  {
    public FinanceAPIContext(DbContextOptions<FinanceAPIContext> options)
        : base(options)
    {

       
    
    public DbSet<Pitcher> Pitchers {get;set;}
    public DbSet<Economy> Economy { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
     }
  }
}