using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eventinfotitlefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Value",
                schema: "RatingServices",
                table: "ratings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                schema: "RatingServices",
                table: "events_info",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "RatingServices",
                table: "events_info",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                schema: "RatingServices",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "RatingServices",
                table: "events_info");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "RatingServices",
                table: "events_info");
        }
    }
}
