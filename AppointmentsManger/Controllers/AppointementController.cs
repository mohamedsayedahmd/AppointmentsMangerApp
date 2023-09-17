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
    [Route("api/appointment")]
    [ApiController]
    public class AppointementController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AppointementController(AppDbContext context)
        {
            _dbContext = context;
        }




        // GET: api/appointement - default
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointements()
        {
            if (_dbContext.Appointements == null)
            {
                return NotFound("No Data Found");
            }
            // filter                                  e === row
            return await _dbContext.Appointements.Where(e => !e.Deleted && !e.Done).ToListAsync();
        }





        // GET: api/Appointement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointement(int id)
        {
            if (_dbContext.Appointements == null)
            {
                return NotFound();
            }
            var appointement = await _dbContext.Appointements.FindAsync(id);

            if (appointement == null)
            {
                return NotFound();
            }

            return appointement;
        }





        // PUT: api/Appointement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointement(int id, Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return BadRequest("You are trying to modify the wrong appointment");
            }
            // I) using libary automapper
            
            try
            {// II) checking manualy each col one by one

                Appointment entry_ = await _dbContext.Appointements.FirstAsync(e => e.ID == appointment.ID);

                if (entry_.Title != appointment.Title)
                {
                    entry_.Title = appointment.Title;
                }

                if (entry_.Description != appointment.Description)
                {
                    entry_.Description = appointment.Description;
                }

                if (entry_.Address != appointment.Address)
                {
                    entry_.Address = appointment.Address;
                }

                if (entry_.LevelOfImportance != appointment.LevelOfImportance)
                {
                    entry_.LevelOfImportance = appointment.LevelOfImportance;
                }

                if (entry_.Done != appointment.Done)
                {
                    entry_.Done = appointment.Done;
                }

                if (entry_.Deleted != appointment.Deleted)
                {
                    entry_.Deleted = appointment.Deleted;
                }

                if (entry_.Date != appointment.Date)
                {
                    entry_.Date = appointment.Date;
                }

                if (entry_.Time != appointment.Time)
                {
                    entry_.Time = appointment.Time;
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointementExists(id))
                {
                    return NotFound("The appointment with the Id" + " " + id + "does not esits!");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Appointment updated successfully!");
        }



         


        // POST: api/Appointement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointement(Appointment appointement)
        {
            if (_dbContext.Appointements == null)
            {
                return Problem("Entity set 'Appointements'  is null.");
            }
            try
            {
                _dbContext.Appointements.Add(appointement);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                return BadRequest("Could not create new Appointment: " + e.Message);
            }


            return CreatedAtAction("GetAppointement", new { id = appointement.ID }, appointement);
        }


        // DELETE: api/Appointement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointement(int id)
        {
            if (_dbContext.Appointements == null)
            {
                return NotFound("No Data Found!");
            }
            var appointement = await _dbContext.Appointements.FindAsync(id);
            if (appointement == null) // now row
            {
                return NotFound("No appointment with the ID " + id);
            }

            Appointment entry_ = await _dbContext.Appointements.FirstAsync(e => e.ID == appointement.ID);
            entry_.ModifiedDate = DateTime.Now;
            entry_.Deleted = true;
            await _dbContext.SaveChangesAsync();

            return Ok("Appointment deleted successfully.");
        }

        private bool AppointementExists(int id)
        {
            return (_dbContext.Appointements?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
