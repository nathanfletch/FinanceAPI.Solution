using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FinanceAPI.Models
{
  public class FinanceAPIContextFactory : IDesignTimeDbContextFactory<FinanceAPIContext>
  {

    FinanceAPIContext IDesignTimeDbContextFactory<FinanceAPIContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<FinanceAPIContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new FinanceAPIContext(builder.Options);
    }
  }
}