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
    public int EconomyId { get; set; }
    public int Year { get; set; }
    public double InterestRate { get; set; }
    public double GDP {get; set;}
    public double UnemplRate {get; set;}
    public double InflationRate {get; set;}
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
      Map(m => m.EconomyId).Ignore();
      Map(m => m.Year);
      Map(m => m.InterestRate).Default(0);
      Map(m => m.GDP).Default(0);
      Map(m => m.UnemplRate).Default(0);
      Map(m => m.InflationRate).Default(0);
      //Map(m => m.Json).TypeConverter<JsonConverter<Json>>();
    }
  }
}


