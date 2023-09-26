using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Contexts;
using BookingAPI.Models;
using BookingAPI.ClassContructRepos;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
          if (_context.Bookings == null)
          {
              return NotFound();
          }
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(Guid id)
        {
          if (_context.Bookings == null)
          {
              return NotFound();
          }
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(Guid id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST api/bookings
        [HttpPost]
        public async Task<IActionResult> PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the time range is logical
            if (booking.startDateTime >= booking.endDateTime)
            {
                return BadRequest("Booking time range is illogical. Please select a valid time range.");
            }

            // Generate a new GUID for the booking
            booking.Id = Guid.NewGuid();

            // Check if the seat is available for the specified time range
            bool isSeatAvailable = await IsSeatAvailable(booking.seatId, booking.startDateTime, booking.endDateTime);

            if (!isSeatAvailable)
            {
                return BadRequest("The selected seat is not available for the specified time range.");
            }

            // If the seat is available, save the booking to the database
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // Check if the seat is available for the specified time range
        private async Task<bool> IsSeatAvailable(string seatId, DateTime startDateTime, DateTime endDateTime)
        {
            // Check if there are any existing bookings that overlap with the specified time range
            bool isAvailable = await _context.Bookings
                .Where(b => b.seatId == seatId &&
                            b.startDateTime < endDateTime &&
                            b.endDateTime > startDateTime)
                .AnyAsync();

            return !isAvailable; // Return true if the seat is available (no overlapping bookings)
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            if (_context.Bookings == null)
            {
                return NotFound();
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool BookingExists(Guid id)
        {
            return (_context.Bookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
