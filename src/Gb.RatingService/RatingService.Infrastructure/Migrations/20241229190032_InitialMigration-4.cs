using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectId",
                schema: "ratings",
                table: "participants_ratings_updates",
                newName: "subject_id");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                schema: "ratings",
                table: "events_ratings_updates",
                newName: "subject_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "subject_id",
                schema: "ratings",
                table: "participants_ratings_updates",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "subject_id",
                schema: "ratings",
                table: "events_ratings_updates",
                newName: "SubjectId");
        }
    }
}
