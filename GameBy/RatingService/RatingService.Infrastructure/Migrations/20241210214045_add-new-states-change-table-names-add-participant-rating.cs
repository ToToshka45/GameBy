using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewstateschangetablenamesaddparticipantrating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_participants_user_ratings_Id",
                schema: "RatingServices",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_user_ratings_users_info_Id",
                schema: "RatingServices",
                table: "user_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_users_rating_updates_user_ratings_Id",
                schema: "RatingServices",
                table: "users_rating_updates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_ratings",
                schema: "RatingServices",
                table: "user_ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_event_ratings",
                schema: "RatingServices",
                table: "event_ratings");

            migrationBuilder.RenameTable(
                name: "user_ratings",
                schema: "RatingServices",
                newName: "users_ratings",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "event_ratings",
                schema: "RatingServices",
                newName: "events_ratings",
                newSchema: "RatingServices");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipationState",
                schema: "RatingServices",
                table: "participants",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "RatingServices",
                table: "events_info",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "RatingServices",
                table: "users_ratings",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_users_ratings",
                schema: "RatingServices",
                table: "users_ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_events_ratings",
                schema: "RatingServices",
                table: "events_ratings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "participants_ratings",
                schema: "RatingServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants_ratings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_events_info_events_ratings_Id",
                schema: "RatingServices",
                table: "events_info",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "events_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_events_rating_updates_events_ratings_Id",
                schema: "RatingServices",
                table: "events_rating_updates",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "events_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_participants_participants_ratings_Id",
                schema: "RatingServices",
                table: "participants",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "participants_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_rating_updates_users_ratings_Id",
                schema: "RatingServices",
                table: "users_rating_updates",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "users_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_ratings_users_info_Id",
                schema: "RatingServices",
                table: "users_ratings",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "users_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_info_events_ratings_Id",
                schema: "RatingServices",
                table: "events_info");

            migrationBuilder.DropForeignKey(
                name: "FK_events_rating_updates_events_ratings_Id",
                schema: "RatingServices",
                table: "events_rating_updates");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_participants_ratings_Id",
                schema: "RatingServices",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_users_rating_updates_users_ratings_Id",
                schema: "RatingServices",
                table: "users_rating_updates");

            migrationBuilder.DropForeignKey(
                name: "FK_users_ratings_users_info_Id",
                schema: "RatingServices",
                table: "users_ratings");

            migrationBuilder.DropTable(
                name: "participants_ratings",
                schema: "RatingServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users_ratings",
                schema: "RatingServices",
                table: "users_ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_events_ratings",
                schema: "RatingServices",
                table: "events_ratings");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "RatingServices",
                table: "events_info");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "RatingServices",
                table: "users_ratings");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "RatingServices",
                table: "events_ratings");

            migrationBuilder.RenameTable(
                name: "users_ratings",
                schema: "RatingServices",
                newName: "user_ratings",
                newSchema: "RatingServices");

            migrationBuilder.RenameTable(
                name: "events_ratings",
                schema: "RatingServices",
                newName: "event_ratings",
                newSchema: "RatingServices");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipationState",
                schema: "RatingServices",
                table: "participants",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_ratings",
                schema: "RatingServices",
                table: "user_ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_event_ratings",
                schema: "RatingServices",
                table: "event_ratings",
                column: "Id");

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
                name: "FK_participants_user_ratings_Id",
                schema: "RatingServices",
                table: "participants",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "user_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_ratings_users_info_Id",
                schema: "RatingServices",
                table: "user_ratings",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "users_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_rating_updates_user_ratings_Id",
                schema: "RatingServices",
                table: "users_rating_updates",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "user_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
