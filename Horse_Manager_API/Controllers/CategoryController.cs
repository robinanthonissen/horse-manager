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
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly DBcontext _context;

        public CategoryController(DBcontext context)
        {
            _context = context;

            if (_context.Categories.Count() == 0)
            {
                _context.Categories.Add(new Category { Name = "Test" });
                _context.SaveChanges();
            }
        }

        //Get Categories by stableID voor HorsesPage
        [HttpGet("{stableID}")]
        public IEnumerable<Category> GetCategories([FromRoute] int stableID)
        {
            var categories = _context.Categories.Where(x => x.StableID == stableID).ToList();

            return categories;
        }

        //Get Category by ID
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryID == id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // PUT update Category by ID 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryID }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
