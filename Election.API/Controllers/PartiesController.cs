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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Election.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PartiesController : ControllerBase
    {
        private readonly DataContext _context;

        public PartiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Parties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Party>>> GetParties()
        {
            return await _context.Parties.ToListAsync();
        }

        // GET: api/Parties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Party>> GetParty(int id)
        {
            var party = await _context.Parties.FindAsync(id);

            if (party == null)
            {
                return NotFound();
            }

            return party;
        }

        // PUT: api/Parties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParty(int id, Party party)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != party.Id)
            {
                return BadRequest();
            }

            _context.Entry(party).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(userName);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyExists(id))
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

        // POST: api/Parties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Party>> PostParty(Party party)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            _context.Parties.Add(party);
            await _context.SaveChangesAsync(userName);

            return CreatedAtAction("GetParty", new { id = party.Id }, party);
        }

        // DELETE: api/Parties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParty(int id)
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync(userName);

            return NoContent();
        }

        private bool PartyExists(int id)
        {
            return _context.Parties.Any(e => e.Id == id);
        }
    }
}
