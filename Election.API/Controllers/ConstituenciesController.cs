#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Election.API.Data;
using Election.API.Data.Entities;

namespace Election.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstituenciesController : ControllerBase
    {
        private readonly DataContext _context;

        public ConstituenciesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Constituencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Constituency>>> GetConstituencies()
        {
            return await _context.Constituencies.ToListAsync();
        }

        // GET: api/Constituencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Constituency>> GetConstituency(int id)
        {
            var constituency = await _context.Constituencies.FindAsync(id);

            if (constituency == null)
            {
                return NotFound();
            }

            return constituency;
        }

        // PUT: api/Constituencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConstituency(int id, Constituency constituency)
        {
            if (id != constituency.Id)
            {
                return BadRequest();
            }

            _context.Entry(constituency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConstituencyExists(id))
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

        // POST: api/Constituencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Constituency>> PostConstituency(Constituency constituency)
        {
            _context.Constituencies.Add(constituency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConstituency", new { id = constituency.Id }, constituency);
        }

        // DELETE: api/Constituencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConstituency(int id)
        {
            var constituency = await _context.Constituencies.FindAsync(id);
            if (constituency == null)
            {
                return NotFound();
            }

            _context.Constituencies.Remove(constituency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConstituencyExists(int id)
        {
            return _context.Constituencies.Any(e => e.Id == id);
        }
    }
}
