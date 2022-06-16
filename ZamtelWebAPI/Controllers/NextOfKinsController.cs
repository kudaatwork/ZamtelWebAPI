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
    public class NextOfKinsController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public NextOfKinsController(ZamtelContext context)
        {
            _context = context;
        }

        // GET: api/NextOfKins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NextOfKin>>> GetNextOfKins()
        {
            return await _context.NextOfKins.ToListAsync();
        }

        // GET: api/NextOfKins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NextOfKin>> GetNextOfKin(int id)
        {
            var nextOfKin = await _context.NextOfKins.FindAsync(id);

            if (nextOfKin == null)
            {
                return NotFound();
            }

            return nextOfKin;
        }

        // PUT: api/NextOfKins/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNextOfKin(int id, NextOfKin nextOfKin)
        {
            if (id != nextOfKin.Id)
            {
                return BadRequest();
            }

            _context.Entry(nextOfKin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NextOfKinExists(id))
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

        // POST: api/NextOfKins
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NextOfKin>> PostNextOfKin(NextOfKin nextOfKin)
        {
            _context.NextOfKins.Add(nextOfKin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNextOfKin", new { id = nextOfKin.Id }, nextOfKin);
        }

        // DELETE: api/NextOfKins/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NextOfKin>> DeleteNextOfKin(int id)
        {
            var nextOfKin = await _context.NextOfKins.FindAsync(id);
            if (nextOfKin == null)
            {
                return NotFound();
            }

            _context.NextOfKins.Remove(nextOfKin);
            await _context.SaveChangesAsync();

            return nextOfKin;
        }

        private bool NextOfKinExists(int id)
        {
            return _context.NextOfKins.Any(e => e.Id == id);
        }
    }
}
