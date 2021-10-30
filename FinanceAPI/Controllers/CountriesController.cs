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
      using (var streamReader = new StreamReader("./Models/SeedData/countries.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<CountryMap>();
          // csvReader.Configuration.MissingFieldFound = null;
          var CountryRecords = csvReader.GetRecords<Country>().ToList();
          
          _db.Countries.AddRange(CountryRecords);
          _db.SaveChanges();
        }
      }

      return await _db.Countries.ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries(string region, int minGDP, int maxGDP, string sortedBy)
    {
      var query = _db.Countries.AsQueryable();

      if(region != null)
      {
        query = query.Where(Country => Country.Region == region);
      }
      if(minGDP != 0)
      {
        query = query.Where(Country => Country.GDP >= minGDP);
      }
      if(maxGDP != 0)
      {
        query = query.Where(Country => Country.GDP <= maxGDP);
      }
      if(sortedBy != null)
      {
        switch(sortedBy)
        {
          case "GDP":
            query = query.OrderByDescending(country => country.GDP);
            break;
          case "name":
            query = query.OrderByDescending(country => country.Name);
            break;
          case "region":
            query = query.OrderByDescending(country => country.Region);
            break;
          case "population":
            query = query.OrderByDescending(country => country.Population);
            break;
          default: 
            break;
        }
      }
      return await query.ToListAsync();
    }
    
    // [HttpPost]
    // public async Task<ActionResult<Country>> Post([FromBody] Country Country)
    // {
    //   _db.Countries.Add(Country);
      
    //   await _db.SaveChangesAsync();

    //   return CreatedAtAction("Post", new { id = Country.CountryId }, Country);
    // }

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

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteCountry(int id)
    // {
    //   var CountryToDelete = await _db.Countries.FirstOrDefaultAsync(entry => entry.CountryId == id);
    //   if (CountryToDelete == null)
    //   {
    //     return NotFound();
    //   }

    //   _db.Countries.Remove(CountryToDelete);
    //   await _db.SaveChangesAsync();

    //   return NoContent();
    // }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutCountry(int id, [FromBody]Country Country)
    // {
    //   if (id != Country.CountryId)
    //   {
    //     return BadRequest();
    //   }
    
    //   _db.Entry(Country).State = EntityState.Modified;
    //   await _db.SaveChangesAsync();

    //   return NoContent();
    // }
  }
}