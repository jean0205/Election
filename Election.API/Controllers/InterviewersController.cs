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
    public class InterviewersController : ControllerBase
    {
        private readonly DataContext _context;

        public InterviewersController(DataContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interviewer>>> GetInterviewers()
        {
            return await _context.Interviewers
                .Include(i => i.Interviews)
                .ThenInclude(i => i.Voter)
                .ThenInclude(v=>v.PollingDivision)
                .Include(c => c.Interviews)
                .ThenInclude(c => c.SupportedParty)
                .ToListAsync();            
        }
       
        
        [HttpGet("Actives")]
        public async Task<ActionResult<IEnumerable<Interviewer>>> GetInterviewersActives()
        {
            return await _context.Interviewers
                .Include(i => i.Interviews)
                .ThenInclude(i => i.Voter)
                .ThenInclude(v => v.PollingDivision)
                .Include(c => c.Interviews)
                .ThenInclude(c => c.SupportedParty).Where(c => c.Active)
                .ToListAsync();

        }

        // GET: api/Interviewers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interviewer>> GetInterviewer(int id)
        {
            var interviewer = await _context.Interviewers
                .Include(i => i.Interviews)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interviewer == null)
            {
                return NotFound();
            }

            return interviewer;
        }

        // PUT: api/Interviewers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterviewer(int id, Interviewer interviewer)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (id != interviewer.Id)
            {
                return BadRequest();
            }

            _context.Entry(interviewer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(userName);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewerExists(id))
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

        // POST: api/Interviewers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interviewer>> PostInterviewer(Interviewer interviewer)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            _context.Interviewers.Add(interviewer);
            await _context.SaveChangesAsync(userName);

            return CreatedAtAction("GetInterviewer", new { id = interviewer.Id }, interviewer);
        }

        // DELETE: api/Interviewers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewer(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var interviewer = await _context.Interviewers.FindAsync(id);
            if (interviewer == null)
            {
                return NotFound();
            }

            _context.Interviewers.Remove(interviewer);
            await _context.SaveChangesAsync(userName);

            return NoContent();
        }

        private bool InterviewerExists(int id)
        {
            return _context.Interviewers.Any(e => e.Id == id);
        }
    }
}
