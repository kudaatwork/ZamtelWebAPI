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
    public class ProvincesController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public ProvincesController(ZamtelContext context)
        {
            _context = context;
        }

        // GET: api/Provinces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces()
        {
            return await _context.Provinces.ToListAsync();
        }

        // GET: api/Provinces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
            var province = await _context.Provinces.FindAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }

        // PUT: api/Provinces/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvince(int id, Province province)
        {
            if (id != province.Id)
            {
                return BadRequest();
            }

            _context.Entry(province).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinceExists(id))
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

        // POST: api/Provinces
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Province>> PostProvince(Province province)
        {
            _context.Provinces.Add(province);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvince", new { id = province.Id }, province);
        }

        // DELETE: api/Provinces/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Province>> DeleteProvince(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();

            return province;
        }

        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.Id == id);
        }
    }
}
