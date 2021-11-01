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
  public class PitchersController : ControllerBase
  {
    private readonly FinanceAPIContext _db;

    public PitchersController(FinanceAPIContext db)
    {
      _db = db;
    }

        [HttpGet("load")]
    public async Task<ActionResult<IEnumerable<Country>>> LoadPitchers()
    {
      var pitchers = await _db.Pitchers.ToListAsync();
      if(pitchers.Count != 0)
      {
        return NoContent();
      }

      using (var streamReader = new StreamReader("./Models/SeedData/pitchers.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<PitcherMap>();
          // csvReader.Configuration.MissingFieldFound = null;
          var pitcherRecords = csvReader.GetRecords<Pitcher>().ToList();
          
          _db.Pitchers.AddRange(pitcherRecords);
          _db.SaveChanges();
        }
      }
      return NoContent();
    }
  }
}