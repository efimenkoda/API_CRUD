using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_CRUD.Models;

namespace API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sheet2Controller : ControllerBase
    {
        private readonly SheetContext _context;

        public Sheet2Controller(SheetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sheet2>>> GetSheet2s()
        {
            return await _context.Sheet2s.OrderBy(x => x.DateTime).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sheet2>> GetSheet2(int id)
        {
            var sheet2 = await _context.Sheet2s.FindAsync(id);

            if (sheet2 == null)
            {
                return NotFound();
            }

            return sheet2;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSheet2(int id, Sheet2 sheet2)
        {
            if (id != sheet2.Id)
            {
                return BadRequest();
            }

            _context.Entry(sheet2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Sheet2Exists(id))
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
        public async Task<ActionResult<Sheet2>> DeleteSheet2(int id)
        {
            var sheet2 = await _context.Sheet2s.FindAsync(id);
            if (sheet2 == null)
            {
                return NotFound();
            }

            _context.Sheet2s.Remove(sheet2);
            await _context.SaveChangesAsync();

            return sheet2;
        }

        private bool Sheet2Exists(int id)
        {
            return _context.Sheet2s.Any(e => e.Id == id);
        }
    }
}
