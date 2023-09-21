using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    seatId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    startDateTime = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    endDateTime = table.Column<string>(type: "nvarchar(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
