﻿#nullable disable
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
    public class ConstituenciesController : ControllerBase
    {
        private readonly DataContext _context;

        public ConstituenciesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Constituencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Constituency>>> GetConstituencies()
        {
            return await _context.Constituencies
                .Include(c=>c.PollingDivisions)
                .ToListAsync();
        }

        // GET: api/Constituencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Constituency>> GetConstituency(int id)
        {
            var constituency = await _context.Constituencies
                .Include(c => c.PollingDivisions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (constituency == null)
            {
                return NotFound();
            }

            return constituency;
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConstituency(int id, Constituency constituency)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != constituency.Id)
            {
                return BadRequest();
            }

            _context.Entry(constituency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(userName);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConstituencyExists(id))
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

        // POST: api/Constituencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Constituency>> PostConstituency(Constituency constituency)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            _context.Constituencies.Add(constituency);
            await _context.SaveChangesAsync(userName);

            return CreatedAtAction("GetConstituency", new { id = constituency.Id }, constituency);
        }

        // DELETE: api/Constituencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConstituency(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var constituency = await _context.Constituencies.FindAsync(id);
            if (constituency == null)
            {
                return NotFound();
            }

            _context.Constituencies.Remove(constituency);
            await _context.SaveChangesAsync(userName);

            return NoContent();
        }

        private bool ConstituencyExists(int id)
        {
            return _context.Constituencies.Any(e => e.Id == id);
        }
    }
}
