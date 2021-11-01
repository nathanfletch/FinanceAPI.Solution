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
  public class YearsController : ControllerBase
  {
    private readonly FinanceAPIContext _db;

    public YearsController(FinanceAPIContext db)
    {
      _db = db;
    }

    [HttpGet("load")]
    public async Task<ActionResult<IEnumerable<Year>>> LoadYears()
    {
      var years = await _db.Years.ToListAsync();
      if(years.Count != 0)
      {
        return NoContent();
      }

      using (var streamReader = new StreamReader("./Models/SeedData/allFactors.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<YearMap>();
          // csvReader.Configuration.MissingFieldFound = null;
          var YearRecords = csvReader.GetRecords<Year>().ToList(); 
          
          _db.Years.AddRange(YearRecords);
          _db.SaveChanges();
        }
      }

      return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Year>>> GetYears(string year, int GDP, int unemployment, int inflation, string sortedBy)
    {
      var query = _db.Years.AsQueryable();

      if(year != null)
      {
        query = query.Where(Year => Year.Region == year);
      }
      if(minGDP != 0)
      {
        query = query.Where(Year => Year.GDP == GDP);
      }
      // if(maxGDP != 0)
      // {
      //   query = query.Where(Year => Year.GDP <= maxGDP);
      // }
      // if(sortedBy != null)
      {
        switch(sortedBy)
        {
          case "GDP":
            query = query.OrderByDescending(country => country.GDP);
            break;
          case "name":
            query = query.OrderByDescending(country => country.Name);
            break;
          case "year":
            query = query.OrderByDescending(country => country.Year);
            break;
          case "unemployment":
            query = query.OrderByDescending(country => country.UnemplRate);
            break;
          case "inflation":
          query = query.OrderByDescending(country => country.InflationRate);
          break;
          default: 
            break;
        }
      }
      return await query.ToListAsync();
    }
    
    // [HttpPost]
    // public async Task<ActionResult<Year>> Post([FromBody] Year Year)
    // {
    //   _db.Years.Add(Year);
      
    //   await _db.SaveChangesAsync();

    //   return CreatedAtAction("Post", new { id = Year.CountryId }, Year);
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<Year>> GetYear(int id)
    {
      var Year = await _db.Years.FindAsync(id);

      if (Year == null)
      {
        return NotFound();
      }

      return Year;
    }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteCountry(int id)
    // {
    //   var CountryToDelete = await _db.Years.FirstOrDefaultAsync(entry => entry.CountryId == id);
    //   if (CountryToDelete == null)
    //   {
    //     return NotFound();
    //   }

    //   _db.Years.Remove(CountryToDelete);
    //   await _db.SaveChangesAsync();

    //   return NoContent();
    // }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutCountry(int id, [FromBody]Year Year)
    // {
    //   if (id != Year.CountryId)
    //   {
    //     return BadRequest();
    //   }
    
    //   _db.Entry(Year).State = EntityState.Modified;
    //   await _db.SaveChangesAsync();

    //   return NoContent();
    // }
  }
}