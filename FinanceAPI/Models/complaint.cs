using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;

namespace FinanceAPI.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }
        public string  Product { get; set; }
        public string SubProduct { get; set; }
        public string Issue { get; set; }
        public string SubIssue { get; set; }
        public string ConsumerComplaint { get; set; } 
        public string CompanyResponce { get; set; }
        public string Company { get; set; }
        public string State { get; set; }
        public string ZIP  { get; set; } 
        public string Tags { get; set; }
        public string ConsumerConsentProvided { get; set; }
        public string SubmittedVia { get; set; }
        public string DateSentToCompany { get; set; }
        public string CompanyResponse { get; set; }
        public string TimelyResponse { get; set; }
        public string ConsumerDisputed { get; set; }
        public string ComplaintId { get; set; }

    }
public sealed class ComplaintMap : ClassMap<Complaint>
    {
        public ComplaintMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.DateReceived).Name("Date Received");
            Map(m => m.Product).Name("Product");
            Map(m => m.SubProduct).Name("Sub-product");
            Map(m => m.Issue).Name("Issue");
            Map(m => m.SubIssue).Name("Sub-issue");
            Map(m => m.ConsumerComplaint).Name("Consumer complaint narrative");
            Map(m => m.CompanyResponce).Name("Company response to consumer");
            Map(m => m.Company).Name("Company");
            Map(m => m.State).Name("State");
            Map(m => m.ZIP).Name("ZIP code");
            Map(m => m.Tags).Name("Tags");
            Map(m => m.ConsumerConsentProvided).Name("Consumer consent provided");
            Map(m => m.SubmittedVia).Name("Submitted via");
            Map(m => m.DateSentToCompany).Name("Date sent to company");
            Map(m => m.CompanyResponse).Name("Company response to consumer");
            Map(m => m.TimelyResponse).Name("Timely response");
            Map(m => m.ConsumerDisputed).Name("Consumer disputed?");
            Map(m => m.ComplaintId).Name("Complaint ID");
        }
    }
}


