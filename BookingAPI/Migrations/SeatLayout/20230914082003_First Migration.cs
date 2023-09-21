using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingAPI.Migrations.SeatLayout
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeatLayouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Seat = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatLayouts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatLayouts");
        }
    }
}
