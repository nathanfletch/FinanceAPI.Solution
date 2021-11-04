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
      var countries = await _db.Countries.ToListAsync();
      if(countries.Count != 0)
      {
        return NoContent();
      }

        using (var streamReader = new StreamReader("./Models/SeedData/countries.csv"))      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<CountryMap>();
          // csvReader.Configuration.MissingFieldFound = null;
          var CountryRecords = csvReader.GetRecords<Country>().ToList();
          CountryRecords.ForEach(country => country.Region = country.Region.Trim());
          _db.Countries.AddRange(CountryRecords);
          _db.SaveChanges();
        }
      }

      return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries(string region, int minGDP, int maxGDP, string sortedBy)
    {
      var query = _db.Countries.AsQueryable();

      if(region != null)
      {
        query = query.Where(country => country.Region == region);
      }
      if(minGDP != 0)
      {
        query = query.Where(country => country.GDP >= minGDP);
      }
      if(maxGDP != 0)
      {
        query = query.Where(country => country.GDP <= maxGDP);
      }
      if(sortedBy != null)
      {
        switch(sortedBy)
        {
          case "GDP":
            query = query.OrderByDescending(country => country.GDP);
            break;
          // case "region":
          //   query = query.OrderByDescending(country => country.Region);
          //   break;
          case "POPULATION":
            query = query.OrderByDescending(country => country.Population);
            break;
          default: 
            break;
        }
      }
      else
      {
        query = query.OrderBy(country => country.Name);
      }

      return await query.ToListAsync();
    }
    
    
  }
}