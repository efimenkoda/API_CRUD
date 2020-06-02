using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_CRUD.Models;
using System.Collections.Immutable;

namespace API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sheet1Controller : ControllerBase
    {
        private readonly SheetContext _context;

        public Sheet1Controller(SheetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sheet1>>> GetSheet1s()
        {
            return await _context.Sheet1s.OrderBy(x=>x.DateTime).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sheet1>> GetSheet1(int id)
        {
            var sheet1 = await _context.Sheet1s.FindAsync(id);

            if (sheet1 == null)
            {
                return NotFound();
            }

            return sheet1;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSheet1(int id, Sheet1 sheet1)
        {
            if (id != sheet1.Id)
            {
                return BadRequest();
            }
            _context.Entry(sheet1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Sheet1Exists(id))
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


        [HttpDelete("{id}")]
        public async Task<ActionResult<Sheet1>> DeleteSheet1(int id)
        {
            var sheet1 = await _context.Sheet1s.FindAsync(id);
            if (sheet1 == null)
            {
                return NotFound();
            }

            _context.Sheet1s.Remove(sheet1);
            await _context.SaveChangesAsync();

            return sheet1;
        }

        private bool Sheet1Exists(int id)
        {
            return _context.Sheet1s.Any(e => e.Id == id);
        }
    }
}
