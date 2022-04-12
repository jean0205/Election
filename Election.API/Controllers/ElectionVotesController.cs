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
            return await _context.ElectionVotes
                .Include(e=>e.Election)
                .Include(e=>e.Voter)
                .ToListAsync();
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
                .Include(e=>e.Interviewer)
                .FirstOrDefaultAsync(e=>e.Id== id);

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
            if (id != electionVote.Id)
            {
                return BadRequest();
            }

            _context.Entry(electionVote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            electionVote.Election = await _context.NationalElections.FindAsync(electionVote.Election.Id);
            electionVote.Voter = await _context.Voters.FindAsync(electionVote.Voter.Id);
            electionVote.SupportedParty = await _context.Parties.FindAsync(electionVote.SupportedParty.Id);
            electionVote.Interviewer = await _context.Interviewers.FindAsync(electionVote.Interviewer.Id);
            if (electionVote.Comment != null)
            {
                electionVote.Comment = _context.Comments.FirstOrDefault(c => c.Id == electionVote.Comment.Id);
            }
            
            _context.ElectionVotes.Add(electionVote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElectionVote", new { id = electionVote.Id }, electionVote);
        }

        // DELETE: api/ElectionVotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectionVote(int id)
        {
            var electionVote = await _context.ElectionVotes.FindAsync(id);
            if (electionVote == null)
            {
                return NotFound();
            }

            _context.ElectionVotes.Remove(electionVote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectionVoteExists(int id)
        {
            return _context.ElectionVotes.Any(e => e.Id == id);
        }
    }
}
