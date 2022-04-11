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
    public class CanvasTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public CanvasTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CanvasTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CanvasType>>> GetCanvasTypes()
        {
            return await _context.CanvasTypes
                .Include(c=>c.Canvas)
                .ThenInclude(c => c.Interviews)
                .ToListAsync();
        }

        // GET: api/CanvasTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CanvasType>> GetCanvasType(int id)
        {
            var canvasType = await _context.CanvasTypes
                .Include(c => c.Canvas)
                .FirstOrDefaultAsync(c=>c.Id==id);

            if (canvasType == null)
            {
                return NotFound();
            }

            return canvasType;
        }

        // PUT: api/CanvasTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanvasType(int id, CanvasType canvasType)
        {
            if (id != canvasType.Id)
            {
                return BadRequest();
            }

            _context.Entry(canvasType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanvasTypeExists(id))
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

        // POST: api/CanvasTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CanvasType>> PostCanvasType(CanvasType canvasType)
        {
            _context.CanvasTypes.Add(canvasType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanvasType", new { id = canvasType.Id }, canvasType);
        }

        // DELETE: api/CanvasTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanvasType(int id)
        {
            var canvasType = await _context.CanvasTypes.FindAsync(id);
            if (canvasType == null)
            {
                return NotFound();
            }

            _context.CanvasTypes.Remove(canvasType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CanvasTypeExists(int id)
        {
            return _context.CanvasTypes.Any(e => e.Id == id);
        }
    }
}
