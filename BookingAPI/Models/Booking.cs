using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingAPI.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        //L1A, L1B, L2A, L3A,
        [Column(TypeName = "nvarchar(3)")]
        public string seatId { get; set; } = "";

        //...
        [Column(TypeName = "datetime")]
        public DateTime startDateTime { get; set; }

        //...
        [Column(TypeName = "datetime")]
        public DateTime endDateTime { get; set; }

    }
}
