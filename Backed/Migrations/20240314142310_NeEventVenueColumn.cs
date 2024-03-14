using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunOlympic.Migrations
{
    /// <inheritdoc />
    public partial class NeEventVenueColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventVenue",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventVenue",
                table: "Events");
        }
    }
}
