#nullable disable
using Election.API.Data;
using Election.API.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Election.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PollingDivisionsController : ControllerBase
    {
        private readonly DataContext _context;

        public PollingDivisionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PollingDivisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollingDivision>>> GetPollingDivisions()
        {
            return await _context.PollingDivisions.ToListAsync();
        }

        // GET: api/PollingDivisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollingDivision>> GetPollingDivision(int id)
        {
            var pollingDivision = await _context.PollingDivisions.FindAsync(id);

            if (pollingDivision == null)
            {
                return NotFound();
            }

            return pollingDivision;
        }

        // PUT: api/PollingDivisions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollingDivision(int id, PollingDivision pollingDivision)
        {
            if (id != pollingDivision.Id)
            {
                return BadRequest();
            }

            _context.Entry(pollingDivision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollingDivisionExists(id))
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

        // POST: api/PollingDivisions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PollingDivision>> PostPollingDivision(PollingDivision pollingDivision)
        {
            var consty= await _context.Constituencies.FirstOrDefaultAsync(c=>c.Id==pollingDivision.Constituency.Id);
            pollingDivision.Constituency = consty;
            _context.PollingDivisions.Add(pollingDivision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollingDivision", new { id = pollingDivision.Id }, pollingDivision);
        }

        // DELETE: api/PollingDivisions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePollingDivision(int id)
        {
            var pollingDivision = await _context.PollingDivisions.FindAsync(id);
            if (pollingDivision == null)
            {
                return NotFound();
            }

            _context.PollingDivisions.Remove(pollingDivision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PollingDivisionExists(int id)
        {
            return _context.PollingDivisions.Any(e => e.Id == id);
        }
    }
}
