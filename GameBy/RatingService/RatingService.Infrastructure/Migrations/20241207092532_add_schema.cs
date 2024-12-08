using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RatingServices");

            migrationBuilder.RenameTable(
                name: "users_rating_updates",
                newName: "users_rating_updates",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "users_info",
                newName: "users_info",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "ratings",
                newName: "ratings",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "participants",
                newName: "participants",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "feedbacks",
                newName: "feedbacks",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "events_rating_updates",
                newName: "events_rating_updates",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "events_info",
                newName: "events_info",
                newSchema: "RatingServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "users_rating_updates",
                schema: "RatingServices",
                newName: "users_rating_updates");

            migrationBuilder.RenameTable(
                name: "users_info",
                schema: "RatingServices",
                newName: "users_info");

            migrationBuilder.RenameTable(
                name: "ratings",
                schema: "RatingServices",
                newName: "ratings");

            migrationBuilder.RenameTable(
                name: "participants",
                schema: "RatingServices",
                newName: "participants");

            migrationBuilder.RenameTable(
                name: "feedbacks",
                schema: "RatingServices",
                newName: "feedbacks");

            migrationBuilder.RenameTable(
                name: "events_rating_updates",
                schema: "RatingServices",
                newName: "events_rating_updates");

            migrationBuilder.RenameTable(
                name: "events_info",
                schema: "RatingServices",
                newName: "events_info");
        }
    }
}
