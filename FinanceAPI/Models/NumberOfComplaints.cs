using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;

namespace FinanceAPI.Models
{
  public class NumberOfComplaint
  {
    public int Id { get; set; }
    public string Company { get; set; }
    public int NumberOfComplaints { get; set; }
  }

  public sealed class NumberOfComplaintsMap : ClassMap<NumberOfComplaint>
  {
    public NumberOfComplaintsMap()
    {
      AutoMap(CultureInfo.InvariantCulture);
      Map(m => m.Id).Ignore();
      // Map(m => m.ComplaintId).Name("Complaint ID");
      // Map(m => m.DateReceived).Name("Date Received");
      // Map(m => m.Product).Name("Product");
      // Map(m => m.SubProduct).Name("Sub-product");
      // Map(m => m.Issue).Name("Issue");
      // Map(m => m.SubIssue).Name("Sub-issue");
      // Map(m => m.ConsumerComplaint).Name("ConsumerComplaint");
      // // Map(m => m.CompanyResponce).Name("Company response to consumer");
      // Map(m => m.Company).Name("Company");
      // Map(m => m.State).Name("State");
      // Map(m => m.ZIP).Name("ZIP code");
      // Map(m => m.Tags).Name("Tags");
      // Map(m => m.ConsumerConsentProvided).Name("Consumer consent provided");
      // Map(m => m.SubmittedVia).Name("Submitted via");
      // Map(m => m.DateSentToCompany).Name("Date sent to company");
      // Map(m => m.CompanyResponse).Name("Company response to consumer");
      // Map(m => m.TimelyResponse).Name("Timely response");
      // Map(m => m.ConsumerDisputed).Name("Consumer disputed?");
    }
  }
}
