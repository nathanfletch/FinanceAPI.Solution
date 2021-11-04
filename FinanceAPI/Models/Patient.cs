using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;


namespace FinanceAPI.Models
{
  public class Patient
  {
    public int PatientId { get; set; }
    public int Age { get; set; }
    public string Sex {get; set;}
    public double Bmi {get; set;}
    public int Children {get; set;}
    public string Smoker {get; set;}
    public double Charges {get; set;}
  }

  public sealed class PatientMap: ClassMap<Patient>
  {
    public PatientMap()
    {
      AutoMap(CultureInfo.InvariantCulture);
      Map(m => m.PatientId).Ignore();
    }
  }
}