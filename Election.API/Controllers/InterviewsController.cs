﻿#nullable disable
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
    public class InterviewsController : ControllerBase
    {
        private readonly DataContext _context;

        public InterviewsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Interviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interview>>> GetInterviews()
        {
            return await _context.Interviews.ToListAsync();
        }

        // GET: api/Interviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interview>> GetInterview(int id)
        {
            var interview = await _context.Interviews
                .Include(i=>i.Canvas)
                .Include(i => i.Interviewer)
                .Include(i => i.Comment)
                .Include(i => i.SupportedParty)
                .Include(i => i.Voter)
                .ThenInclude(v => v.PollingDivision)
                .ThenInclude(v => v.Constituency)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interview == null)
            {
                return NotFound();
            }

            return interview;
        }

        // PUT: api/Interviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterview(int id, Interview interview)
        {
            //TODO
            //modificar elupdate para qactualice las relacions, si cambia en interviewer o el party
            //revisar q al hacer null las relaciones, no se borren cuando se actualiza
            if (id != interview.Id)
            {
                return BadRequest();
            }
            var interviewDB = await  _context.Interviews
                .Include(i => i.Interviewer)
                .Include(i => i.SupportedParty)
                .Include(i => i.Voter)
                .Include(i=>i.Canvas)
                .Include(i => i.Comment)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (interviewDB == null)
            {
                return NotFound();
            }
            interviewDB.Voter = _context.Voters.FirstOrDefault(v => v.Id == interview.Voter.Id);
            interviewDB.Interviewer = _context.Interviewers.FirstOrDefault(i => i.Id == interview.Interviewer.Id);
            interviewDB.SupportedParty = _context.Parties.FirstOrDefault(s => s.Id == interview.SupportedParty.Id);            
            interviewDB.Canvas = _context.Canvas.FirstOrDefault(c => c.Id == interview.Canvas.Id);
            if (interview.Comment==null)
            {
                interviewDB.Comment = null;
            }
            else
            {
                interviewDB.Comment = _context.Comments.FirstOrDefault(c => c.Id == interview.Comment.Id);
            }           

            _context.Entry(interviewDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewExists(id))
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

        // POST: api/Interviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interview>> PostInterview(Interview interview)
        {
            var interviewDB = _context.Interviews.FirstOrDefault(i => i.Id == interview.Id);
            interview.Interviewer = _context.Interviewers.FirstOrDefault(i => i.Id == interview.Interviewer.Id);
            if (interview.Comment != null)
            {
                interview.Comment = _context.Comments.FirstOrDefault(c => c.Id == interview.Comment.Id);
            }

            interview.SupportedParty = _context.Parties.FirstOrDefault(s => s.Id == interview.SupportedParty.Id);
            interview.Voter = _context.Voters.FirstOrDefault(v => v.Id == interview.Voter.Id);
            interview.Canvas = _context.Canvas.FirstOrDefault(c => c.Id == interview.Canvas.Id);
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterview", new { id = interview.Id }, interview);
        }

        // DELETE: api/Interviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            _context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewExists(int id)
        {
            return _context.Interviews.Any(e => e.Id == id);
        }
    }
}
