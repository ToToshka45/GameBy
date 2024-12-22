using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "RatingServices",
                table: "participants_ratings");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "RatingServices",
                table: "events_ratings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "RatingServices",
                table: "participants_ratings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "RatingServices",
                table: "events_ratings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
