using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rating_services");

            migrationBuilder.CreateTable(
                name: "events_ratings",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "participants_ratings",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants_ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users_info",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_user_id = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events_info",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_info_events_ratings_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "events_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events_rating_updates",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_rating_updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_rating_updates_events_ratings_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "events_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_ratings",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_ratings_users_info_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    Receiver_EntityType = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserInfoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_feedbacks_events_info_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_feedbacks_users_info_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_feedbacks_users_info_UserInfoId",
                        column: x => x.UserInfoId,
                        principalSchema: "rating_services",
                        principalTable: "users_info",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "participants",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_participant_id = table.Column<int>(type: "integer", nullable: false),
                    external_user_id = table.Column<int>(type: "integer", nullable: false),
                    participation_state = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participants_events_info_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participants_participants_ratings_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "participants_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_rating_updates",
                schema: "rating_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    rating_owner_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_rating_updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_rating_updates_users_ratings_Id",
                        column: x => x.Id,
                        principalSchema: "rating_services",
                        principalTable: "users_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_UserInfoId",
                schema: "rating_services",
                table: "feedbacks",
                column: "UserInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events_rating_updates",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "feedbacks",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "participants",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "users_rating_updates",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "events_info",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "participants_ratings",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "users_ratings",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "events_ratings",
                schema: "rating_services");

            migrationBuilder.DropTable(
                name: "users_info",
                schema: "rating_services");
        }
    }
}
