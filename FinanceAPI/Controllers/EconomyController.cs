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
  public class EconomyController : ControllerBase
  {
    private readonly FinanceAPIContext _db;

    public EconomyController(FinanceAPIContext db)
    {
      _db = db;
    }

    [HttpGet("load")]
    public async Task<ActionResult<IEnumerable<Economy>>> LoadEconomy()
    {
      var economy = await _db.Economy.ToListAsync();
      if(economy.Count != 0)
      {
        return NoContent();
      }

      using (var streamReader = new StreamReader("./Models/SeedData/allFactors.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<EconomyMap>();
          // csvReader.Configuration.MissingFieldFound = null;
          var EconomyRecords = csvReader.GetRecords<Economy>().OrderBy(item => item.Year).ToList(); 
          
          _db.Economy.AddRange(EconomyRecords);
          _db.SaveChanges();
        }
      }

      return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Economy>>> GetEconomy(string year, double interest, int GDP, int unemployment, int inflation, string sortedBy)
    {
      var query = _db.Economy.AsQueryable();

      if(year != null)
      {
        query = query.Where(Economy => Economy.Year == Convert.ToInt32(year));
      }
      //sort just in case it wasn't working
      query = query.OrderBy(economy => economy.Year);

      return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Economy>> GetEconomy(int id)
    {
      var Economy = await _db.Economy.FindAsync(id);

      if (Economy == null)
      {
        return NotFound();
      }

      return Economy;
    }
  }
}