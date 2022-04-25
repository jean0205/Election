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
        [HttpGet("ByDivisions/{id}")]
        public async Task<ActionResult<IEnumerable<Voter>>> GetVotersByDivisions(int id)
        {
            return await _context.Voters
                .Include(v => v.PollingDivision)
                .ThenInclude(v => v.Constituency)
                .Where(v => v.PollingDivision.Id == id)
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
        //get voter by division
        [HttpGet("FindByDivision/{id}")]
        public async Task<ActionResult<Voter>> GetVoterByDivision(int id)
        {
            var voter = await _context.Voters.
                Include(v => v.PollingDivision)
                .FirstOrDefaultAsync(v => v.PollingDivision.Id == id);
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
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;            
            if (id != voter.Id)
            {
                return BadRequest();
            }
            var voterToUpdate = await _context.Voters.FindAsync(id);
            voterToUpdate.Reg = voter.Reg;
            voterToUpdate.Active = voter.Active;
            voterToUpdate.GivenNames = voter.GivenNames;
            voterToUpdate.Address = voter.Address;
            voterToUpdate.SurName = voter.SurName;
            voterToUpdate.Sex = voter.Sex;
            voterToUpdate.DOB = voter.DOB;
            voterToUpdate.Address = voter.Address;
            //assing every voter value to voterToUpdate
            voterToUpdate.Mobile1 = voter.Mobile1;
            voterToUpdate.Mobile2 = voter.Mobile2;
            voterToUpdate.HomePhone = voter.HomePhone;
            voterToUpdate.WorkPhone = voter.WorkPhone;
            voterToUpdate.Email = voter.Email;
            voterToUpdate.Mobile1 = voter.Mobile1;
            voterToUpdate.Occupation = voter.Occupation;
            var pollinDivision = await _context.PollingDivisions.FindAsync(voter.PollingDivision.Id);
            voterToUpdate.PollingDivision = pollinDivision;

            _context.Entry(voterToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(userName);
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
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
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
            await _context.SaveChangesAsync(userName);

            return CreatedAtAction("GetVoter", new { id = voter.Id }, voter);
        }
        
        [HttpPost("Range")]
        public async Task<ActionResult<Voter>> PostVoterList(List<Voter> voters)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
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
            await _context.SaveChangesAsync(userName);
            return NoContent();
        }
        // DELETE: api/Voters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoter(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null)
            {
                return NotFound();
            }

            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync(userName);

            return NoContent();
        }

        private bool VoterExists(int id)
        {
            return _context.Voters.Any(e => e.Id == id);
        }

        [HttpDelete("RemoveHouse/{id}")]
        public async Task<IActionResult> DeleteVoterHouse(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var voter = await _context.Voters
                .Include(v => v.House)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (voter == null)
            {
                return NotFound();
            }
            voter.House = null;
            _context.Entry(voter).State = EntityState.Modified;
            await _context.SaveChangesAsync(userName);
            return NoContent();
        }
    }
}
