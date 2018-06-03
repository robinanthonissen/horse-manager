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
    [Route("api/Horses")]
    public class HorsesController : Controller
    {
        private readonly DBcontext _context;

        public HorsesController (DBcontext context)
        {
            _context = context;

            if (_context.Horses.Count() == 0)
            {
                _context.Horses.Add(new Horse { Name = "Test" });
                _context.SaveChanges();
            }
        }

        //Get Horses by stableID voor HorsesPage
        /*[HttpGet("{stableID}")]
        public IEnumerable<Horse> GetHorses([FromRoute] int stableID)
        {
            var horses = _context.Horses.Where(x => x.StableID == stableID).Include("Category").ToList();
            foreach(var horse in horses){
                var img = _context.Images.Where(x => x.ParentObjectType == "horse" && x.ParentObjectID == horse.HorseID).ToList();
                horse.Images = img;
            }

            return horses;
        }*/

        //Get Horse by ID
        [HttpGet("{id}", Name = "GetHorse")]
        public async Task<IActionResult> GetHorse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var horses = await _context.Horses.Where(x => x.StableID == id).Include("Category").ToListAsync();
            /*foreach (var horse in horses)
            {
                var img = await _context.Images.Where(x => x.ParentObjectType == "horse" && x.ParentObjectID == horse.HorseID).ToListAsync();
                horse.Images = img;
            }*/

            if (horses == null)
            {
                return NotFound();
            }
            return Ok(horses);
        }

        /*[HttpGet("{id}", Name = "GetHorse")]
        public async Task<IActionResult> GetHorse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var horse = await _context.Horses.Include("Category").SingleOrDefaultAsync(m => m.HorseID == id);

            //var img = _context.Images.Where(x => x.ParentObjectType == "horse" && x.ParentObjectID == horse.HorseID).ToList();
            //horse.Images = img;

            if (horse == null)
            {
                return NotFound();
            }
            return Ok(horse);
        }*/

        // PUT update Horse by ID 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorse([FromRoute] int id, [FromBody] Horse horse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horse.HorseID)
            {
                return BadRequest();
            }

            _context.Entry(horse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseExists(id))
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

        // POST: api/Horse
        [HttpPost]
        public async Task<IActionResult> PostHorse([FromBody] Horse horse)
        {
            //int a = (int)HttpContext.Session.GetInt32("userId");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //horse.HorseID = a;

            _context.Horses.Add(horse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorse", new { id = horse.HorseID }, horse);
        }

        // DELETE: api/Horse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var horse = await _context.Horses.SingleOrDefaultAsync(m => m.HorseID == id);
            if (horse == null)
            {
                return NotFound();
            }

            _context.Horses.Remove(horse);
            await _context.SaveChangesAsync();

            return Ok(horse);
        }

        private bool HorseExists(int id)
        {
            return _context.Horses.Any(e => e.HorseID == id);
        }
    }
}
