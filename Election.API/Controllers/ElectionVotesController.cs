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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Election.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ElectionVotesController : ControllerBase
    {
        private readonly DataContext _context;

        public ElectionVotesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ElectionVotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectionVote>>> GetElectionVotes()
        {
            return await _context.ElectionVotes.AsNoTracking()
                .Include(e=>e.Election)
                .Include(e=>e.Voter)
                .ToListAsync();
        }
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<int>> GetElectionVotesByUser(string userId)
        {
            var votes= await _context.ElectionVotes.AsNoTracking()
                .Include(e => e.RecorderBy)
                .Where(e => e.RecorderBy.Id == userId)
                .ToListAsync();
            return votes.Count;
        }

        // GET: api/ElectionVotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectionVote>> GetElectionVote(int id)
        {
            var electionVote = await _context.ElectionVotes
                .Include(e=>e.Election)
                .Include(e => e.Voter)
                .ThenInclude(e=>e.PollingDivision)
                .ThenInclude(e=>e.Constituency)
                .Include(e => e.SupportedParty)
                .Include(e=>e.Comment)
                .Include(e=>e.RecorderBy)
                .FirstOrDefaultAsync(e=>e.Id== id);

            if (electionVote == null)
            {
                return NotFound();
            }

            return electionVote;
        }
        [HttpGet("FindByParty/{id}")]
        public async Task<ActionResult<ElectionVote>> GetElectionVoteByParty(int id)
        {
            var electionVote = await _context.ElectionVotes               
                .Include(e => e.SupportedParty)
                .FirstOrDefaultAsync(e => e.SupportedParty.Id == id);

            if (electionVote == null)
            {
                return NotFound();
            }

            return electionVote;
        }

        // PUT: api/ElectionVotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectionVote(int id, ElectionVote electionVote)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != electionVote.Id)
            {
                return BadRequest();
            }
            var electionVoteDB = await _context.ElectionVotes
                .Include(e => e.Election)                
                .Include(e => e.SupportedParty)
                .Include(e => e.Comment)
                .Include(e => e.RecorderBy)
                .FirstOrDefaultAsync(e => e.Id == id);
            electionVoteDB.Election = await _context.NationalElections.FirstOrDefaultAsync(e => e.Id == electionVote.Election.Id);            
            electionVoteDB.RecorderBy = await _context.Users.FirstOrDefaultAsync(e => e.Id == electionVote.RecorderBy.Id);
            if (electionVote.SupportedParty == null)
            {
                electionVoteDB.SupportedParty = null;
            }
            else
            {
                electionVoteDB.SupportedParty = await _context.Parties.FirstOrDefaultAsync(e => e.Id == electionVote.SupportedParty.Id);
            }
            if (electionVote.Comment == null)
            {
                electionVoteDB.Comment = null;
            }
            else
            {
                electionVoteDB.Comment = _context.Comments.FirstOrDefault(c => c.Id == electionVote.Comment.Id);
            }
            electionVoteDB.OtherComment = electionVote.OtherComment;
            electionVoteDB.VoteTime = electionVote.VoteTime;
            _context.Entry(electionVoteDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(userName);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectionVoteExists(id))
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

        // POST: api/ElectionVotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElectionVote>> PostElectionVote(ElectionVote electionVote)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            electionVote.Election = await _context.NationalElections.FindAsync(electionVote.Election.Id);
            var voter= await _context.Voters
                .Include(v=>v.ElectionVotes)
                .FirstOrDefaultAsync(v=>v.Id==electionVote.Voter.Id);

            electionVote.Voter = voter;

            if (voter.ElectionVotes.Count > 0 && voter.ElectionVotes.Any(e => e.Election.Id == electionVote.Election.Id))
            {
                ModelState.AddModelError("Error", "This voter already vote in this election.");
                return BadRequest(ModelState) ;                
               
            }

            if (electionVote.SupportedParty!=null)
            {
                electionVote.SupportedParty = await _context.Parties.FindAsync(electionVote.SupportedParty.Id);
            }
           
            electionVote.RecorderBy = await _context.Users.FindAsync(electionVote.RecorderBy.Id);
            if (electionVote.Comment != null)
            {
                electionVote.Comment = _context.Comments.FirstOrDefault(c => c.Id == electionVote.Comment.Id);
            }
            
            _context.ElectionVotes.Add(electionVote);
            await _context.SaveChangesAsync(userName);

            return CreatedAtAction("GetElectionVote", new { id = electionVote.Id }, electionVote);
        }

        // DELETE: api/ElectionVotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectionVote(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var electionVote = await _context.ElectionVotes.FindAsync(id);
            if (electionVote == null)
            {
                return NotFound();
            }
            _context.ElectionVotes.Remove(electionVote);
            await _context.SaveChangesAsync(userName);
            return NoContent();
        }
        private bool ElectionVoteExists(int id)
        {
            return _context.ElectionVotes.Any(e => e.Id == id);
        }
    }
}
