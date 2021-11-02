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
  }

  public sealed class EconomyMap: ClassMap<Economy>
  {
    public EconomyMap()
    {
      AutoMap(CultureInfo.InvariantCulture);
      //don't try to get Economyid from csv:
      Map(m => m.EconomyId).Ignore();
      Map(m => m.Year).Index(0);
      Map(m => m.InterestRate).Index(1);
      Map(m => m.GDP).Index(2);
      Map(m => m.UnemplRate).Index(3);
      Map(m => m.InflationRate).Index(4);
      //Map(m => m.Json).TypeConverter<JsonConverter<Json>>();
    }
  }
}


