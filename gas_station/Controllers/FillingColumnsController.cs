using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gas_station.Models;

namespace gas_station.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FillingColumnsController : ControllerBase
    {
        private readonly DBContext _context;

        public FillingColumnsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/FillingColumns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FillingColumn>>> GetFillingColumns()
        {
            return await _context.FillingColumns.ToListAsync();
        }

        // GET: api/FillingColumns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FillingColumn>> GetFillingColumn(int id)
        {
            var fillingColumn = await _context.FillingColumns.FindAsync(id);

            if (fillingColumn == null)
            {
                return NotFound();
            }

            return fillingColumn;
        }

        // PUT: api/FillingColumns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFillingColumn(int id, FillingColumn fillingColumn)
        {
            if (id != fillingColumn.Id)
            {
                return BadRequest();
            }

            _context.Entry(fillingColumn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FillingColumnExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FillingColumns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FillingColumn>> PostFillingColumn(FillingColumn fillingColumn)
        {
            _context.FillingColumns.Add(fillingColumn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFillingColumn", new { id = fillingColumn.Id }, fillingColumn);
        }

        // DELETE: api/FillingColumns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFillingColumn(int id)
        {
            var fillingColumn = await _context.FillingColumns.FindAsync(id);
            if (fillingColumn == null)
            {
                return NotFound();
            }

            _context.FillingColumns.Remove(fillingColumn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FillingColumnExists(int id)
        {
            return _context.FillingColumns.Any(e => e.Id == id);
        }
    }
}
