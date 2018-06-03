using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Horse_Manager_API.Models;
using Horse_Manager_API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Horse_Manager_API.Controllers
{
    [Route("api/Stable")]
    public class StableController : Controller
    {
        private readonly DBcontext _context;

        public StableController(DBcontext context)
        {
            _context = context;

            if (_context.Stables.Count() == 0)
            {
                _context.Stables.Add(new Stable { Name = "Test" });
                _context.SaveChanges();
            }
        }

        //Get Stable by ID
        [HttpGet("{id}", Name = "GetStable")]
        public async Task<IActionResult> GetStable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stable = await _context.Stables.Include("Categories").Include("Horses").Include("Workers").SingleOrDefaultAsync(m => m.StableID == id);

            foreach (var horse in stable.Horses)
            {
                horse.Category = _context.Categories.Find(horse.CategoryID);
            }
            foreach(var cat in stable.Categories)
            {
                cat.HorseCount = _context.Horses.Where(x => x.CategoryID == cat.CategoryID).Count();
            }

            /*var img = _context.Images.Where(x => x.ParentObjectType == "stable" && x.ParentObjectID == stable.StableID).ToList();
            stable.Images = img;*/

            if (stable == null)
            {
                return NotFound();
            }
            return Ok(stable);
        }

        // PUT update Stable by ID 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStable([FromRoute] int id, [FromBody] Stable stable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stable.StableID)
            {
                return BadRequest();
            }

            _context.Entry(stable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StableExists(id))
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

        // POST: api/Stable
        [HttpPost]
        public async Task<IActionResult> PostStable([FromBody] Stable stable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stables.Add(stable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStable", new { id = stable.StableID }, stable);
        }

        // DELETE: api/Stable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stable = await _context.Stables.SingleOrDefaultAsync(m => m.StableID == id);
            if (stable == null)
            {
                return NotFound();
            }

            _context.Stables.Remove(stable);
            await _context.SaveChangesAsync();

            return Ok(stable);
        }

        private bool StableExists(int id)
        {
            return _context.Stables.Any(e => e.StableID == id);
        }
    }
}
