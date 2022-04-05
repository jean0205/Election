﻿#nullable disable
using Election.API.Data;
using Election.API.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalElection>>> GetNationalElections()
        {
            return await _context.NationalElections
                .Include(e => e.Parties)
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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutNationalElection(int id, NationalElection nationalElection)
        {
            if (id != nationalElection.Id)
            {
                return BadRequest();
            }
            var electionDB = await _context.NationalElections.Include(e=>e.Parties).FirstOrDefaultAsync(e => e.Id == id);
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
                await _context.SaveChangesAsync();
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
                        await _context.SaveChangesAsync();
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
            if (nationalElection.Parties != null && nationalElection.Parties.Any())
            {
                var parties = nationalElection.Parties.ToList();
                string json = JsonConvert.SerializeObject(parties);
                var partiesCopy = JsonConvert.DeserializeObject<List<Party>>(json);

                nationalElection.Parties = null;
                _context.NationalElections.Add(nationalElection);
                await _context.SaveChangesAsync();

                var SavedElection = _context.NationalElections.FirstOrDefault(x => x.Id == nationalElection.Id);
                SavedElection.Parties = new List<Party>();
                foreach (var party in partiesCopy)
                {
                    var partyDB = _context.Parties.FirstOrDefault(x => x.Id == party.Id);
                    SavedElection.Parties.Add(partyDB);
                    _context.Entry(SavedElection).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }

            return CreatedAtAction("GetNationalElection", new { id = nationalElection.Id }, nationalElection);
        }

        // DELETE: api/NationalElections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNationalElection(int id)
        {
            var nationalElection = await _context.NationalElections.FindAsync(id);
            if (nationalElection == null)
            {
                return NotFound();
            }

            _context.NationalElections.Remove(nationalElection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NationalElectionExists(int id)
        {
            return _context.NationalElections.Any(e => e.Id == id);
        }
    }
}