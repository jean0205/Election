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
    public class CanvasController : ControllerBase
    {
        private readonly DataContext _context;

        public CanvasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Canvas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canvas>>> GetCanvas()
        {
            return await _context.Canvas
                .Include(c=>c.Type)
                .Include(c=>c.Interviews)
                .ThenInclude(i=>i.Voter)
                .ThenInclude(i => i.PollingDivision)                
                .Include(c => c.Interviews)
                .ThenInclude(i => i.Interviewer)                
                .Include(c => c.Interviews)
                .ThenInclude(c=>c.SupportedParty)
                .ToListAsync();
        }
        // GET: api/Canvas
        [HttpGet("Actives")]
        public async Task<ActionResult<IEnumerable<Canvas>>> GetCanvasActives()
        {
            return await _context.Canvas
                .Include(c => c.Type)
                .Include(c => c.Interviews)
                .ThenInclude(i => i.Voter)
                .ThenInclude(i => i.PollingDivision)
                .Include(c => c.Interviews)
                .ThenInclude(i => i.Interviewer)
                .Include(c => c.Interviews)
                .ThenInclude(c => c.SupportedParty)
                .Where(c=>c.Active)
                .ToListAsync();
        }
        [HttpGet("OpenAll")]
        public async Task<ActionResult<IEnumerable<Canvas>>> GetCanvasOpen()
        {
            return await _context.Canvas
                .Include(c => c.Type)
                .Include(c => c.Interviews)
                .ThenInclude(i => i.Voter)
                .ThenInclude(i => i.PollingDivision)
                .Include(c => c.Interviews)
                .ThenInclude(i => i.Interviewer)
                .Include(c => c.Interviews)
                .ThenInclude(c => c.SupportedParty)
                .Where(c => c.Open)
                .ToListAsync();
        }
        [HttpGet("OpenByUser")]
        public async Task<ActionResult<IEnumerable<Canvas>>> GetCanvasOpenByUser()
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return await _context.Canvas
                .Include(c => c.Type)
                .Include(c => c.Interviews.Where(i => i.RecorderBy.UserName == userName))
                .ThenInclude(i => i.Voter)
                .ThenInclude(i => i.PollingDivision)
                .Include(c => c.Interviews.Where(i => i.RecorderBy.UserName == userName))
                .ThenInclude(i => i.Interviewer)
               .Include(c => c.Interviews.Where(i => i.RecorderBy.UserName == userName))
                .ThenInclude(c => c.SupportedParty)
                .Include(c => c.Interviews.Where(i => i.RecorderBy.UserName == userName))
                .ThenInclude(c => c.RecorderBy)
                .Where(c => c.Open)
                .ToListAsync();
        }

        // GET: api/Canvas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canvas>> GetCanvas(int id)
        {
            var canvas = await _context.Canvas
                .Include(c => c.Interviews)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (canvas == null)
            {
                return NotFound();
            }

            return canvas;
        }

        // PUT: api/Canvas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanvas(int id, Canvas canvas)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != canvas.Id)
            {
                return BadRequest();
            }

            var canvasType = await _context.CanvasTypes.FirstOrDefaultAsync(c => c.Id == canvas.Type.Id);
            _context.Entry(canvas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(userName);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanvasExists(id))
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

        // POST: api/Canvas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Canvas>> PostCanvas(Canvas canvas)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;            
            var canvasType = await _context.CanvasTypes.FirstOrDefaultAsync(c => c.Id == canvas.Type.Id);
            canvas.Type = canvasType;
            _context.Canvas.Add(canvas);
            await _context.SaveChangesAsync(userName);

            return CreatedAtAction("GetCanvas", new { id = canvas.Id }, canvas);
        }

        // DELETE: api/Canvas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanvas(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var canvas = await _context.Canvas.FindAsync(id);
            if (canvas == null)
            {
                return NotFound();
            }

            _context.Canvas.Remove(canvas);
            await _context.SaveChangesAsync(userName);

            return NoContent();
        }

        private bool CanvasExists(int id)
        {
            return _context.Canvas.Any(e => e.Id == id);
        }
    }
}
