using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceAPI.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;

namespace FinanceAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ComplaintsController : ControllerBase
  {
    private readonly FinanceAPIContext _db;

    public ComplaintsController(FinanceAPIContext db)
    {
      _db = db;
    }
    [HttpGet("load")]
    public async Task<ActionResult<IEnumerable<Complaint>>> LoadComplaints()
    {
      var complaints = await _db.Complaints.ToListAsync();
      if (complaints.Count != 0)
      {
        return NoContent();
      }

      using (var streamReader = new StreamReader("./Models/SeedData/complaints.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<ComplaintMap>();
          var complaintRecords = csvReader.GetRecords<Complaint>().ToList();

          _db.Complaints.AddRange(complaintRecords);
          _db.SaveChanges();
        }
      }
      return NoContent();
    }

    // GET: api/Complaints
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
    {
      return await _db.Complaints.ToListAsync();
    }

  }
}