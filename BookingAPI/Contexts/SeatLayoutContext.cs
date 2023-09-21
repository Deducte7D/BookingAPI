using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;

namespace BookingAPI.Contexts
{
    public class SeatLayoutContext : DbContext
    {
        public SeatLayoutContext(DbContextOptions<SeatLayoutContext> options) : base(options)
        {
        }

        public DbSet<SeatLayout> SeatLayouts { get; set; }
    }
}
