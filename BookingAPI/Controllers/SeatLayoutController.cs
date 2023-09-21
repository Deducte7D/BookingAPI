using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Contexts;
using BookingAPI.Models;

public class SeatLayoutInfo
{
    public Guid Id { get; set; }
    public string Seat { get; set; }
}

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatLayoutController : ControllerBase
    {
        private readonly SeatLayoutContext _context;

        public SeatLayoutController(SeatLayoutContext context)
        {
            _context = context;
        }

        // GET: api/SeatLayout
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatLayout>>> GetSeatLayouts()
        {
          if (_context.SeatLayouts == null)
          {
              return NotFound();
          }
            return await _context.SeatLayouts.ToListAsync();
        }

        // GET: api/SeatLayout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeatLayout>> GetSeatLayout(Guid id)
        {
          if (_context.SeatLayouts == null)
          {
              return NotFound();
          }
            var seatLayout = await _context.SeatLayouts.FindAsync(id);

            if (seatLayout == null)
            {
                return NotFound();
            }

            return seatLayout;
        }

        // GET: api/SeatLayout/ByLevel/1
        [HttpGet("ByLevel/{level}")]
        public async Task<ActionResult<IEnumerable<SeatLayoutInfo>>> GetSeatLayoutsByLevel(string level)
        {
            if (_context.SeatLayouts == null)
            {
                return NotFound();
            }

            var seatLayouts = await _context.SeatLayouts
                .Where(sl => sl.Level == level)
                .Select(sl => new SeatLayoutInfo
                {
                    Id = sl.Id,
                    Seat = sl.Seat
                })
                .ToListAsync();

            return seatLayouts;
        }

        // PUT: api/SeatLayout/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeatLayout(Guid id, SeatLayout seatLayout)
        {
            if (id != seatLayout.Id)
            {
                return BadRequest();
            }

            _context.Entry(seatLayout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatLayoutExists(id))
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

        // POST: api/SeatLayout
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SeatLayout>> PostSeatLayout(SeatLayout seatLayout)
        {
          if (_context.SeatLayouts == null)
          {
              return Problem("Entity set 'SeatLayoutContext.SeatLayouts'  is null.");
          }

            // Generate a new GUID for the booking
            seatLayout.Id = Guid.NewGuid();

            _context.SeatLayouts.Add(seatLayout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeatLayout", new { id = seatLayout.Id }, seatLayout);
        }

        // DELETE: api/SeatLayout/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeatLayout(Guid id)
        {
            if (_context.SeatLayouts == null)
            {
                return NotFound();
            }
            var seatLayout = await _context.SeatLayouts.FindAsync(id);
            if (seatLayout == null)
            {
                return NotFound();
            }

            _context.SeatLayouts.Remove(seatLayout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatLayoutExists(Guid id)
        {
            return (_context.SeatLayouts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

