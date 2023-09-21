using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class SeatLayout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        //L1,L2,L3
        [Column(TypeName = "nvarchar(2)")]
        public string Level { get; set; } = "";

        //A,B,C,D
        [Column(TypeName = "nvarchar(1)")]
        public string Seat { get; set; } = "";
    }
}
