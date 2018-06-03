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
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly DBcontext _context;

        public UserController(DBcontext context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { FirstName = "Test" });
                _context.SaveChanges();
            }
        }

        //Get Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        //Get User by ID
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.Include("Own_Stables").SingleOrDefaultAsync(m => m.UserID == id);
            foreach (var stal in user.Own_Stables){
                stal.Categories = _context.Categories.Where(x => x.StableID == stal.StableID).ToList();
                foreach (var cat in stal.Categories)
                {
                    cat.HorseCount = _context.Horses.Where(x => x.CategoryID == cat.CategoryID).Count();
                }
                stal.Horses = _context.Horses.Where(x => x.StableID == stal.StableID).Include("Category").ToList();
            }

            var work = _context.Workers.Where(x => x.UserID == id).Include("Stable").ToList();
            foreach (var item in work)
            {
                item.User = null;
                item.Stable.Workers = null;
                item.Stable.Categories = _context.Categories.Where(x => x.StableID == item.StableID).ToList();
                foreach (var cat in item.Stable.Categories)
                {
                    cat.HorseCount = _context.Horses.Where(x => x.CategoryID == cat.CategoryID).Count();
                }
                item.Stable.Horses = _context.Horses.Where(x => x.StableID == item.Stable.StableID).Include("Category").ToList();
            }
            user.Work = work;

            /*foreach (var work in stableWorker)
            {
                user.Other_Stables = _context.Stables.Where(x => x.StableID == work.StableID).ToList();
            }*/

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT update User by ID 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            int a = (int)HttpContext.Session.GetInt32("userId");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.UserID = a;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
