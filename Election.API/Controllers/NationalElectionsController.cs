#nullable disable
using Election.API.Data;
using Election.API.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Election.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class NationalElectionsController : ControllerBase
    {
        private readonly DataContext _context;

        public NationalElectionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/NationalElections
        [HttpGet("Open")]
        public async Task<ActionResult<IEnumerable<NationalElection>>> GetNationalElectionsOpens()
        {
            return await _context.NationalElections
                .Where(e=>e.Open)
                .ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalElection>>> GetNationalElections()
        {
            return await _context.NationalElections
                .Include(e => e.Parties)
                .ToListAsync();
        }
        [HttpGet("Votes")]
        public async Task<ActionResult<IEnumerable<NationalElection>>> GetNationalElectionsVotes()
        {
            return await _context.NationalElections
                .Include(e => e.ElectionVotes)
                .ThenInclude(e => e.Voter)
                .Include(e => e.Parties)
                .ToListAsync();
        }
        [HttpGet("Votes-Open")]
        public async Task<ActionResult<IEnumerable<NationalElection>>> GetNationalElectionsVotesOpen()
        {
            return await _context.NationalElections
                .Include(e => e.ElectionVotes)
                .ThenInclude(e => e.Voter)
                .Include(e => e.Parties)
                .Where(e => e.Open)
                .ToListAsync();
        }
        [HttpGet("Votes-OpenByUser")]
        public async Task<ActionResult<IEnumerable<NationalElection>>> GetNationalElectionsVotesOpenByUser()
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return await _context.NationalElections
                .Include(e => e.ElectionVotes.Where(i => i.RecorderBy.UserName == userName))
                .ThenInclude(e => e.Voter)
                .Include(e => e.Parties)
                .Where(e => e.Open)
                .ToListAsync();
        }
       
        // GET: api/NationalElections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NationalElection>> GetNationalElection(int id)
        {
            var nationalElection = await _context.NationalElections
                .Include(e => e.Parties)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (nationalElection == null)
            {
                return NotFound();
            }
            return nationalElection;
        }
        [HttpGet("IncludingVotes/{id}")]
        public async Task<ActionResult<NationalElection>> GetNationalElectionWithVotes(int id)
        {
            var nationalElection = await _context.NationalElections
                .Include(e => e.ElectionVotes)
                .FirstOrDefaultAsync(e => e.ElectionVotes.Any() && e.Id == id);
            if (nationalElection == null)
            {
                return NotFound();
            }
            return nationalElection;
        }
        [HttpGet("FindByParty/{id}")]
        public async Task<ActionResult<NationalElection>> GetNationalElectionByParty(int id)
        {
            var nationalElection = await _context.NationalElections
                .Include(e => e.Parties)
                .FirstOrDefaultAsync(e => e.Parties.Any(p => p.Id == id));
            if (nationalElection == null)
            {
                return NotFound();
            }
            return nationalElection;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutNationalElection(int id, NationalElection nationalElection)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != nationalElection.Id)
            {
                return BadRequest();
            }
            var electionDB = await _context.NationalElections.Include(e => e.Parties).FirstOrDefaultAsync(e => e.Id == id);
            if (electionDB == null)
            {
                return NotFound();
            }
            electionDB.Parties = null;
            electionDB.Open = nationalElection.Open;
            electionDB.ElectionDate = nationalElection.ElectionDate;
            electionDB.Description = nationalElection.Description;
            _context.Entry(electionDB).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(userName);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalElectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (nationalElection.Parties != null && nationalElection.Parties.Any())
            {
                electionDB.Parties = new List<Party>();
                foreach (var party in nationalElection.Parties)
                {
                    var partyDB = _context.Parties.FirstOrDefault(x => x.Id == party.Id);
                    electionDB.Parties.Add(partyDB);
                    _context.Entry(electionDB).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync(userName);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!NationalElectionExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }


            return NoContent();
        }

        // POST: api/NationalElections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NationalElection>> PostNationalElection(NationalElection nationalElection)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (nationalElection.Parties != null && nationalElection.Parties.Any())
            {
                var parties = nationalElection.Parties.ToList();
                string json = JsonConvert.SerializeObject(parties);
                var partiesCopy = JsonConvert.DeserializeObject<List<Party>>(json);

                nationalElection.Parties = null;
                _context.NationalElections.Add(nationalElection);
                await _context.SaveChangesAsync(userName);

                var SavedElection = _context.NationalElections.FirstOrDefault(x => x.Id == nationalElection.Id);
                SavedElection.Parties = new List<Party>();
                foreach (var party in partiesCopy)
                {
                    var partyDB = _context.Parties.FirstOrDefault(x => x.Id == party.Id);
                    SavedElection.Parties.Add(partyDB);
                    _context.Entry(SavedElection).State = EntityState.Modified;
                    await _context.SaveChangesAsync(userName);
                }
            }

            return CreatedAtAction("GetNationalElection", new { id = nationalElection.Id }, nationalElection);
        }

        // DELETE: api/NationalElections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNationalElection(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nationalElection = await _context.NationalElections.FindAsync(id);
            if (nationalElection == null)
            {
                return NotFound();
            }

            _context.NationalElections.Remove(nationalElection);
            await _context.SaveChangesAsync(userName);

            return NoContent();
        }

        private bool NationalElectionExists(int id)
        {
            return _context.NationalElections.Any(e => e.Id == id);
        }
    }
}
