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
  public class PatientsController : ControllerBase
  {
    private readonly FinanceAPIContext _db;

    public PatientsController(FinanceAPIContext db)
    {
      _db = db;
    }

    [HttpGet("load")]
    public async Task<ActionResult<IEnumerable<Patient>>> LoadPatients()
    {
      var patients = await _db.Patients.ToListAsync();
      if(patients.Count != 0)
      {
        return NoContent();
      }

      using (var streamReader = new StreamReader("./Models/SeedData/insurance.csv"))
      {
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
          csvReader.Context.RegisterClassMap<PatientMap>();
          // csvReader.Configuration.MissingFieldFound = null;
          var PatientRecords = csvReader.GetRecords<Patient>().ToList();
          
          _db.Patients.AddRange(PatientRecords);
          _db.SaveChanges();
        }
      }

      return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(int age, string sex, double bmi, int children, string smoker, double charges)
    {
      var query = _db.Patients.AsQueryable();
      return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(int id)
    {
      var Patient = await _db.Patients.FindAsync(id);

      if (Patient == null)
      {
        return NotFound();
      }

      return Patient;
    }
  }
}