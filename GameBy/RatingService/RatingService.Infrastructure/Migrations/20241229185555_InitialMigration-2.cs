using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "external_event_id",
                schema: "ratings",
                table: "participants_ratings_updates");

            migrationBuilder.DropColumn(
                name: "external_event_id",
                schema: "ratings",
                table: "events_ratings_updates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "external_event_id",
                schema: "ratings",
                table: "participants_ratings_updates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "external_event_id",
                schema: "ratings",
                table: "events_ratings_updates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
