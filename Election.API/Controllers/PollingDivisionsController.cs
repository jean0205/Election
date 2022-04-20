#nullable disable
using Election.API.Data;
using Election.API.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != pollingDivision.Id)
            {
                return BadRequest();
            }
            var consty = await _context.Constituencies.FirstOrDefaultAsync(c => c.Id == pollingDivision.Constituency.Id);
            pollingDivision.Constituency = consty;
            _context.Entry(pollingDivision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(userName);
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
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var consty= await _context.Constituencies.FirstOrDefaultAsync(c=>c.Id==pollingDivision.Constituency.Id);
            pollingDivision.Constituency = consty;
            _context.PollingDivisions.Add(pollingDivision);
            await _context.SaveChangesAsync(userName);

            return CreatedAtAction("GetPollingDivision", new { id = pollingDivision.Id }, pollingDivision);
        }

        // DELETE: api/PollingDivisions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePollingDivision(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var pollingDivision = await _context.PollingDivisions.FindAsync(id);
            if (pollingDivision == null)
            {
                return NotFound();
            }

            _context.PollingDivisions.Remove(pollingDivision);
            await _context.SaveChangesAsync(userName);

            return NoContent();
        }

        private bool PollingDivisionExists(int id)
        {
            return _context.PollingDivisions.Any(e => e.Id == id);
        }
    }
}
