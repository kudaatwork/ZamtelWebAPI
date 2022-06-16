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
    public class RepresentativesController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public RepresentativesController(ZamtelContext context)
        {
            _context = context;
        }

        // GET: api/Representatives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Representative>>> GetRepresentatives()
        {
            return await _context.Representatives.ToListAsync();
        }

        // GET: api/Representatives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Representative>> GetRepresentative(int id)
        {
            var representative = await _context.Representatives.FindAsync(id);

            if (representative == null)
            {
                return NotFound();
            }

            return representative;
        }

        // PUT: api/Representatives/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepresentative(int id, Representative representative)
        {
            if (id != representative.Id)
            {
                return BadRequest();
            }

            _context.Entry(representative).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepresentativeExists(id))
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

        // POST: api/Representatives
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Representative>> PostRepresentative(Representative representative)
        {
            _context.Representatives.Add(representative);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepresentative", new { id = representative.Id }, representative);
        }

        // DELETE: api/Representatives/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Representative>> DeleteRepresentative(int id)
        {
            var representative = await _context.Representatives.FindAsync(id);
            if (representative == null)
            {
                return NotFound();
            }

            _context.Representatives.Remove(representative);
            await _context.SaveChangesAsync();

            return representative;
        }

        private bool RepresentativeExists(int id)
        {
            return _context.Representatives.Any(e => e.Id == id);
        }
    }
}
