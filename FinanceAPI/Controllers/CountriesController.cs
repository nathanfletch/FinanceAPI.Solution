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
  public class CountriesController : ControllerBase
  {
    private readonly FinanceAPIContext _db;

    public CountriesController(FinanceAPIContext db)
    {
      _db = db;
    }

    [HttpGet("load")]
    public async Task<ActionResult<IEnumerable<Country>>> LoadCountries()
    {
      using (var streamReader = new StreamReader("./Models/SeedData/file_name.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<CountryMap>();
          var CountryRecords = csvReader.GetRecords<Country>().ToList();
          
          _db.Countries.AddRange(CountryRecords);
          _db.SaveChanges();
        }
      }

      return await _db.Countries.ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries(string type, int minScore, int maxScore, bool sorted = false)
    {
      var query = _db.Countries.AsQueryable();

      if(type != null)
      {
        query = query.Where(Country => Country.Type == type);
      }
      if(minScore != 0)
      {
        query = query.Where(Country => Country.Score >= minScore);
      }
      if(maxScore != 0)
      {
        query = query.Where(Country => Country.Score <= maxScore);
      }
      if(sorted)
      {
        query = query.OrderByDescending(Country => Country.Score);
      }
      return await query.ToListAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult<Country>> Post([FromBody] Country Country)
    {
      _db.Countries.Add(Country);
      
      await _db.SaveChangesAsync();

      return CreatedAtAction("Post", new { id = Country.CountryId }, Country);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
      var Country = await _db.Countries.FindAsync(id);

      if (Country == null)
      {
        return NotFound();
      }

      return Country;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
      var CountryToDelete = await _db.Countries.FirstOrDefaultAsync(entry => entry.CountryId == id);
      if (CountryToDelete == null)
      {
        return NotFound();
      }

      _db.Countries.Remove(CountryToDelete);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, [FromBody]Country Country)
    {
      if (id != Country.CountryId)
      {
        return BadRequest();
      }
    
      _db.Entry(Country).State = EntityState.Modified;
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}