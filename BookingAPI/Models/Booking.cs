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
        [Column(TypeName = "nvarchar(100)")]
        public string startDateTime { get; set; } = "";

        //...
        [Column(TypeName = "nvarchar(100)")]
        public string endDateTime { get; set; } = "";

    }
}
