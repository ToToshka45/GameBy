using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_ratings_updates_events_ratings_EventRatingId",
                schema: "ratings",
                table: "events_ratings_updates");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_ratings_updates_participants_ratings_Participa~",
                schema: "ratings",
                table: "participants_ratings_updates");

            migrationBuilder.DropIndex(
                name: "IX_participants_ratings_updates_ParticipantRatingId",
                schema: "ratings",
                table: "participants_ratings_updates");

            migrationBuilder.DropIndex(
                name: "IX_events_ratings_updates_EventRatingId",
                schema: "ratings",
                table: "events_ratings_updates");

            migrationBuilder.DropColumn(
                name: "ParticipantRatingId",
                schema: "ratings",
                table: "participants_ratings_updates");

            migrationBuilder.DropColumn(
                name: "EventRatingId",
                schema: "ratings",
                table: "events_ratings_updates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParticipantRatingId",
                schema: "ratings",
                table: "participants_ratings_updates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventRatingId",
                schema: "ratings",
                table: "events_ratings_updates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_participants_ratings_updates_ParticipantRatingId",
                schema: "ratings",
                table: "participants_ratings_updates",
                column: "ParticipantRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_events_ratings_updates_EventRatingId",
                schema: "ratings",
                table: "events_ratings_updates",
                column: "EventRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_ratings_updates_events_ratings_EventRatingId",
                schema: "ratings",
                table: "events_ratings_updates",
                column: "EventRatingId",
                principalSchema: "ratings",
                principalTable: "events_ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_participants_ratings_updates_participants_ratings_Participa~",
                schema: "ratings",
                table: "participants_ratings_updates",
                column: "ParticipantRatingId",
                principalSchema: "ratings",
                principalTable: "participants_ratings",
                principalColumn: "Id");
        }
    }
}
