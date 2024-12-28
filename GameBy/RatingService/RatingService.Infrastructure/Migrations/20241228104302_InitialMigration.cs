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
                name: "ratings");

            migrationBuilder.CreateTable(
                name: "ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    entity_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users_info",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events_info",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_info_ratings_Id",
                        column: x => x.Id,
                        principalSchema: "ratings",
                        principalTable: "ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ratings_updates",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false),
                    rating_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RatingId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings_updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ratings_updates_ratings_RatingId1",
                        column: x => x.RatingId1,
                        principalSchema: "ratings",
                        principalTable: "ratings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ratings_updates_ratings_rating_id",
                        column: x => x.rating_id,
                        principalSchema: "ratings",
                        principalTable: "ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    Receiver_EntityType = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventInfoId = table.Column<int>(type: "integer", nullable: true),
                    UserInfoId = table.Column<int>(type: "integer", nullable: true),
                    UserInfoId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_feedbacks_events_info_EventInfoId",
                        column: x => x.EventInfoId,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_feedbacks_events_info_event_id",
                        column: x => x.event_id,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_feedbacks_users_info_UserInfoId",
                        column: x => x.UserInfoId,
                        principalSchema: "ratings",
                        principalTable: "users_info",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_feedbacks_users_info_UserInfoId1",
                        column: x => x.UserInfoId1,
                        principalSchema: "ratings",
                        principalTable: "users_info",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "participants",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    participation_state = table.Column<string>(type: "text", nullable: false),
                    EventInfoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participants_events_info_EventInfoId",
                        column: x => x.EventInfoId,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_participants_events_info_event_id",
                        column: x => x.event_id,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participants_ratings_Id",
                        column: x => x.Id,
                        principalSchema: "ratings",
                        principalTable: "ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_event_id",
                schema: "ratings",
                table: "feedbacks",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_EventInfoId",
                schema: "ratings",
                table: "feedbacks",
                column: "EventInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_UserInfoId",
                schema: "ratings",
                table: "feedbacks",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_UserInfoId1",
                schema: "ratings",
                table: "feedbacks",
                column: "UserInfoId1");

            migrationBuilder.CreateIndex(
                name: "IX_participants_event_id",
                schema: "ratings",
                table: "participants",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_participants_EventInfoId",
                schema: "ratings",
                table: "participants",
                column: "EventInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_updates_rating_id",
                schema: "ratings",
                table: "ratings_updates",
                column: "rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_updates_RatingId1",
                schema: "ratings",
                table: "ratings_updates",
                column: "RatingId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedbacks",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "participants",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "ratings_updates",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "users_info",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "events_info",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "ratings",
                schema: "ratings");
        }
    }
}
