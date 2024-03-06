using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunOlympic.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsCanceledColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "EventSubscribers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "EventSubscribers");
        }
    }
}
