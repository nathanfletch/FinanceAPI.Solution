using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;


namespace FinanceAPI.Models
{
  public class Pitcher
  {
    public int PitcherId { get; set; }
    public string Name { get; set; }
    public int Pitches {get;set;}
    public int Salary {get; set;}
  }

  public sealed class PitcherMap: ClassMap<Pitcher>
  {
    public PitcherMap()
    {
      AutoMap(CultureInfo.InvariantCulture);
      Map(m => m.PitcherId).Ignore();
    }
  }
}