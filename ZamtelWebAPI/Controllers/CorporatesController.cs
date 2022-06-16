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
    public class CorporatesController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public CorporatesController(ZamtelContext context)
        {
            _context = context;
        }

        // GET: api/Corporates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corporate>>> GetCorporates()
        {
            return await _context.Corporates.ToListAsync();
        }

        // GET: api/Corporates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Corporate>> GetCorporate(int id)
        {
            var corporate = await _context.Corporates.FindAsync(id);

            if (corporate == null)
            {
                return NotFound();
            }

            return corporate;
        }

        // PUT: api/Corporates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorporate(int id, Corporate corporate)
        {
            if (id != corporate.Id)
            {
                return BadRequest();
            }

            _context.Entry(corporate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorporateExists(id))
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

        // POST: api/Corporates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Corporate>> PostCorporate(Corporate corporate)
        {
            _context.Corporates.Add(corporate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCorporate", new { id = corporate.Id }, corporate);
        }

        // DELETE: api/Corporates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Corporate>> DeleteCorporate(int id)
        {
            var corporate = await _context.Corporates.FindAsync(id);
            if (corporate == null)
            {
                return NotFound();
            }

            _context.Corporates.Remove(corporate);
            await _context.SaveChangesAsync();

            return corporate;
        }

        private bool CorporateExists(int id)
        {
            return _context.Corporates.Any(e => e.Id == id);
        }
    }
}
