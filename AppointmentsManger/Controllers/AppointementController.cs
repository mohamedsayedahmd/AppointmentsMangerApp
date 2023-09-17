using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentsManger.Data;
using AppointmentsManger.Data.Models;

namespace AppointmentsManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointementController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointement>>> GetAppointements()
        {
          if (_context.Appointements == null)
          {
              return NotFound();
          }
            return await _context.Appointements.ToListAsync();
        }

        // GET: api/Appointement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointement>> GetAppointement(int id)
        {
          if (_context.Appointements == null)
          {
              return NotFound();
          }
            var appointement = await _context.Appointements.FindAsync(id);

            if (appointement == null)
            {
                return NotFound();
            }

            return appointement;
        }

        // PUT: api/Appointement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointement(int id, Appointement appointement)
        {
            if (id != appointement.ID)
            {
                return BadRequest();
            }

            _context.Entry(appointement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointementExists(id))
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

        // POST: api/Appointement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointement>> PostAppointement(Appointement appointement)
        {
          if (_context.Appointements == null)
          {
              return Problem("Entity set 'AppDbContext.Appointements'  is null.");
          }
            _context.Appointements.Add(appointement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointement", new { id = appointement.ID }, appointement);
        }

        // DELETE: api/Appointement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointement(int id)
        {
            if (_context.Appointements == null)
            {
                return NotFound();
            }
            var appointement = await _context.Appointements.FindAsync(id);
            if (appointement == null)
            {
                return NotFound();
            }

            _context.Appointements.Remove(appointement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointementExists(int id)
        {
            return (_context.Appointements?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
