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
    public class VotersController : ControllerBase
    {
        private readonly DataContext _context;

        public VotersController(DataContext context)
        {
            _context = context;
        }
        // GET: api/Voters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voter>>> GetVoters()
        {
            return await _context.Voters
                .Include(v => v.PollingDivision)
                .ThenInclude(v => v.Constituency)
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Voter>> GetVoter(int id)
        {
            var voter = await _context.Voters.
              Include(v => v.PollingDivision)
              .ThenInclude(v => v.Constituency)
              .Include(c => c.Interviews)
              .ThenInclude(i => i.Interviewer)
              .Include(c => c.Interviews)
              .ThenInclude(i => i.Canvas)
              .ThenInclude(i => i.Type)
              .Include(c => c.Interviews)
              .ThenInclude(c => c.SupportedParty)
              .Include(v => v.House)
              .Include(v => v.ElectionVotes)
              .ThenInclude(v => v.Election)
              .Include(v => v.ElectionVotes)
              .ThenInclude(v => v.SupportedParty)
               .FirstOrDefaultAsync(v => v.Id == id);
            if (voter == null)
            {
                return NotFound();
            }

            return voter;
        }

        [HttpGet("FindRegistration/{reg}")]
        public async Task<ActionResult<Voter>> GetVoterByRegistration(string reg)
        {
            var voter = await _context.Voters.
                Include(v => v.PollingDivision)
                .ThenInclude(v => v.Constituency)
                .Include(v => v.Interviews)
                .Include(v => v.House)
                .Include(v => v.ElectionVotes)
                .FirstOrDefaultAsync(v => v.Reg == reg);
            if (voter == null)
            {
                return NotFound();
            }

            return voter;
        }

        // PUT: api/Voters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoter(int id, Voter voter)
        {
            if (id != voter.Id)
            {
                return BadRequest();
            }

            _context.Entry(voter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoterExists(id))
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

        // POST: api/Voters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voter>> PostVoter(Voter voter)
        {
            if (voter.PollingDivision != null)
            {
                PollingDivision pollingDivision = await _context.PollingDivisions.FirstOrDefaultAsync(p => p.Name == voter.PollingDivision.Name);
                voter.PollingDivision = pollingDivision;
            }

            //TODO VERIFICAR CUAL SERA EL ID DE LA CASA, PROBABLEMENTE EL NUMERO Y BUSCARLA ENTONCES POR EL NUMERO NO POR EL ID
            if (voter.House != null)
            {
                House house = await _context.Houses.FirstOrDefaultAsync(h => h.Id == voter.House.Id);
                voter.House = house;
            }

            _context.Voters.Add(voter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoter", new { id = voter.Id }, voter);
        }
        
        [HttpPost("Range")]
        public async Task<ActionResult<Voter>> PostVoterList(List<Voter> voters)
        {
            foreach (var voter in voters)
            {
                if (voter.PollingDivision != null)
                {
                    PollingDivision pollingDivision = await _context.PollingDivisions.FirstOrDefaultAsync(p => p.Name == voter.PollingDivision.Name);
                    voter.PollingDivision = pollingDivision;
                }
                //TODO VERIFICAR CUAL SERA EL ID DE LA CASA, PROBABLEMENTE EL NUMERO Y BUSCARLA ENTONCES POR EL NUMERO NO POR EL ID
                if (voter.House != null)
                {
                    House house = await _context.Houses.FirstOrDefaultAsync(h => h.Id == voter.House.Id);
                    voter.House = house;
                }
            }
            await _context.Voters.AddRangeAsync(voters);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: api/Voters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoter(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null)
            {
                return NotFound();
            }

            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoterExists(int id)
        {
            return _context.Voters.Any(e => e.Id == id);
        }
    }
}
