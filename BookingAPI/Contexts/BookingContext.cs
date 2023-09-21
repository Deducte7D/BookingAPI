using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;

namespace BookingAPI.Contexts
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings {  get; set; }
    }
}
