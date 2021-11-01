using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;


namespace FinanceAPI.Models
{
  public class Economy
  {
    public int YearId { get; set; }
    public string Year { get; set; }
    public string InterestRate { get; set; }
    public int GDP {get; set;}
    public int UnemplRate {get; set;}
    public int InflationRate {get; set;}
    // public InfantMortality {get; set;}
    // public Literacy {get; set;}
    // public Phones {get; set;}
    // public Birthrate {get; set;}
    // public Deathrate {get; set;}
  }

  public sealed class EconomyMap: ClassMap<Economy>
  {
    public EconomyMap()
    {
      AutoMap(CultureInfo.InvariantCulture);
      //don't try to get Economyid from csv:
      Map(m => m.YearId).Ignore();
      Map(m => m.GDP).Default(0);
      // Map(m => m.Population).Optional();

      //map column name to Prop name
      // Map(m => m.EconomyId).Ignore();
    }
  }
}


