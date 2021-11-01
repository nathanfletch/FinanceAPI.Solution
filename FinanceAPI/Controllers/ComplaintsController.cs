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
        private readonly FinanceAPIContext _context;

        public ComplaintsController(FinanceAPIContext context)
        {
            _context = context;
        }

        // GET: api/Complaints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            return await _context.Complaints.ToListAsync();
        }

    }
}