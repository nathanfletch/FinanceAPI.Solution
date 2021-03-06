using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;


namespace FinanceAPI.Models
{
  public class Country
  {
    public int CountryId { get; set; }
    public string Name { get; set; }
    public string Region {get; set;}
    public int GDP {get; set;}
    public int Population {get; set;}
    // public InfantMortality {get; set;}
    // public Literacy {get; set;}
    // public Phones {get; set;}
    // public Birthrate {get; set;}
    // public Deathrate {get; set;}
  }

  public sealed class CountryMap: ClassMap<Country>
  {
    public CountryMap()
    {
      AutoMap(CultureInfo.InvariantCulture);
      //don't try to get countryid from csv:
      Map(m => m.CountryId).Ignore();
      Map(m => m.GDP).Default(0);
      // Map(m => m.Population).Optional();

      //map column name to Prop name
      // Map(m => m.CountryId).Ignore();
    }
  }
}


