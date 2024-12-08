using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eventratings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_info_ratings_Id",
                schema: "RatingServices",
                table: "events_info");

            migrationBuilder.DropForeignKey(
                name: "FK_events_rating_updates_ratings_Id",
                schema: "RatingServices",
                table: "events_rating_updates");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_ratings_Id",
                schema: "RatingServices",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_users_rating_updates_ratings_Id",
                schema: "RatingServices",
                table: "users_rating_updates");

            migrationBuilder.DropTable(
                name: "ratings",
                schema: "RatingServices");

            migrationBuilder.CreateTable(
                name: "event_ratings",
                schema: "RatingServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRating",
                schema: "RatingServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRating_users_info_Id",
                        column: x => x.Id,
                        principalSchema: "RatingServices",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_events_info_event_ratings_Id",
                schema: "RatingServices",
                table: "events_info",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "event_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_events_rating_updates_event_ratings_Id",
                schema: "RatingServices",
                table: "events_rating_updates",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "event_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_participants_UserRating_Id",
                schema: "RatingServices",
                table: "participants",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "UserRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_rating_updates_UserRating_Id",
                schema: "RatingServices",
                table: "users_rating_updates",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "UserRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_info_event_ratings_Id",
                schema: "RatingServices",
                table: "events_info");

            migrationBuilder.DropForeignKey(
                name: "FK_events_rating_updates_event_ratings_Id",
                schema: "RatingServices",
                table: "events_rating_updates");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_UserRating_Id",
                schema: "RatingServices",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_users_rating_updates_UserRating_Id",
                schema: "RatingServices",
                table: "users_rating_updates");

            migrationBuilder.DropTable(
                name: "event_ratings",
                schema: "RatingServices");

            migrationBuilder.DropTable(
                name: "UserRating",
                schema: "RatingServices");

            migrationBuilder.CreateTable(
                name: "ratings",
                schema: "RatingServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ratings_users_info_Id",
                        column: x => x.Id,
                        principalSchema: "RatingServices",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_events_info_ratings_Id",
                schema: "RatingServices",
                table: "events_info",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_events_rating_updates_ratings_Id",
                schema: "RatingServices",
                table: "events_rating_updates",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_participants_ratings_Id",
                schema: "RatingServices",
                table: "participants",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_rating_updates_ratings_Id",
                schema: "RatingServices",
                table: "users_rating_updates",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
