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
  public class NumberOfComplaintsController : ControllerBase
  {
    private readonly FinanceAPIContext _db;

    public NumberOfComplaintsController(FinanceAPIContext db)
    {
      _db = db;
    }
    [HttpGet("load")]
    public async Task<ActionResult<IEnumerable<NumberOfComplaint>>> LoadNumberOfComplaints()
    {
      var NumberOfComplaints = await _db.NumberOfComplaints.ToListAsync();
      if (NumberOfComplaints.Count != 0)
      {
        return NoContent();
      }

      using (var streamReader = new StreamReader("./Models/SeedData/NumberOfComplaints.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<NumberOfComplaintsMap>();
          var NumberOfComplaintRecords = csvReader.GetRecords<NumberOfComplaint>().ToList();

          _db.NumberOfComplaints.AddRange(NumberOfComplaintRecords);
          _db.SaveChanges();
        }
      }
      return NoContent();
    }

    // GET: api/NumberOfComplaints
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NumberOfComplaint>>> GetNumberOfComplaints()
    {
      return await _db.NumberOfComplaints.ToListAsync();
    }

  }
}