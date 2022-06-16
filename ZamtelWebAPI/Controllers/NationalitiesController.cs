using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalitiesController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public NationalitiesController(ZamtelContext context)
        {
            _context = context;
        }

        // GET: api/Nationalities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nationality>>> GetNationalities()
        {
            return await _context.Nationalities.ToListAsync();
        }

        // GET: api/Nationalities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nationality>> GetNationality(int id)
        {
            var nationality = await _context.Nationalities.FindAsync(id);

            if (nationality == null)
            {
                return NotFound();
            }

            return nationality;
        }

        // PUT: api/Nationalities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNationality(int id, Nationality nationality)
        {
            if (id != nationality.Id)
            {
                return BadRequest();
            }

            _context.Entry(nationality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalityExists(id))
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

        // POST: api/Nationalities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Nationality>> PostNationality(Nationality nationality)
        {
            _context.Nationalities.Add(nationality);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNationality", new { id = nationality.Id }, nationality);
        }

        // DELETE: api/Nationalities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Nationality>> DeleteNationality(int id)
        {
            var nationality = await _context.Nationalities.FindAsync(id);
            if (nationality == null)
            {
                return NotFound();
            }

            _context.Nationalities.Remove(nationality);
            await _context.SaveChangesAsync();

            return nationality;
        }

        private bool NationalityExists(int id)
        {
            return _context.Nationalities.Any(e => e.Id == id);
        }
    }
}
