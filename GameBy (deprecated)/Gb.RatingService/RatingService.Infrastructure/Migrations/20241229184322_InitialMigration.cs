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
                name: "events_info",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    organizer_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users_info",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: false),
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
                        name: "FK_feedbacks_events_info_external_event_id",
                        column: x => x.external_event_id,
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
                name: "gamers_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    user_info_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gamers_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gamers_ratings_users_info_user_info_id",
                        column: x => x.user_info_id,
                        principalSchema: "ratings",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizers_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    user_info_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizers_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_organizers_ratings_users_info_user_info_id",
                        column: x => x.user_info_id,
                        principalSchema: "ratings",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participants",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    participation_state = table.Column<string>(type: "text", nullable: false),
                    user_info_id = table.Column<int>(type: "integer", nullable: false),
                    event_info_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participants_events_info_event_info_id",
                        column: x => x.event_info_id,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participants_users_info_user_info_id",
                        column: x => x.user_info_id,
                        principalSchema: "ratings",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    event_info_id = table.Column<int>(type: "integer", nullable: false),
                    organizer_rating_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_ratings_events_info_event_info_id",
                        column: x => x.event_info_id,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_ratings_organizers_ratings_organizer_rating_id",
                        column: x => x.organizer_rating_id,
                        principalSchema: "ratings",
                        principalTable: "organizers_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participants_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    participant_id = table.Column<int>(type: "integer", nullable: false),
                    gamer_rating_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participants_ratings_gamers_ratings_gamer_rating_id",
                        column: x => x.gamer_rating_id,
                        principalSchema: "ratings",
                        principalTable: "gamers_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participants_ratings_participants_participant_id",
                        column: x => x.participant_id,
                        principalSchema: "ratings",
                        principalTable: "participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events_ratings_updates",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating_id = table.Column<int>(type: "integer", nullable: false),
                    EventRatingId = table.Column<int>(type: "integer", nullable: true),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_ratings_updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_ratings_updates_events_ratings_EventRatingId",
                        column: x => x.EventRatingId,
                        principalSchema: "ratings",
                        principalTable: "events_ratings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_events_ratings_updates_events_ratings_rating_id",
                        column: x => x.rating_id,
                        principalSchema: "ratings",
                        principalTable: "events_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participants_ratings_updates",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating_id = table.Column<int>(type: "integer", nullable: false),
                    ParticipantRatingId = table.Column<int>(type: "integer", nullable: true),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants_ratings_updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participants_ratings_updates_participants_ratings_Participa~",
                        column: x => x.ParticipantRatingId,
                        principalSchema: "ratings",
                        principalTable: "participants_ratings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_participants_ratings_updates_participants_ratings_rating_id",
                        column: x => x.rating_id,
                        principalSchema: "ratings",
                        principalTable: "participants_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_events_ratings_event_info_id",
                schema: "ratings",
                table: "events_ratings",
                column: "event_info_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_ratings_organizer_rating_id",
                schema: "ratings",
                table: "events_ratings",
                column: "organizer_rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_ratings_updates_EventRatingId",
                schema: "ratings",
                table: "events_ratings_updates",
                column: "EventRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_events_ratings_updates_rating_id",
                schema: "ratings",
                table: "events_ratings_updates",
                column: "rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_EventInfoId",
                schema: "ratings",
                table: "feedbacks",
                column: "EventInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_external_event_id",
                schema: "ratings",
                table: "feedbacks",
                column: "external_event_id");

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
                name: "IX_gamers_ratings_user_info_id",
                schema: "ratings",
                table: "gamers_ratings",
                column: "user_info_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organizers_ratings_user_info_id",
                schema: "ratings",
                table: "organizers_ratings",
                column: "user_info_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_participants_event_info_id",
                schema: "ratings",
                table: "participants",
                column: "event_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_participants_user_info_id",
                schema: "ratings",
                table: "participants",
                column: "user_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_participants_ratings_gamer_rating_id",
                schema: "ratings",
                table: "participants_ratings",
                column: "gamer_rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_participants_ratings_participant_id",
                schema: "ratings",
                table: "participants_ratings",
                column: "participant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_participants_ratings_updates_ParticipantRatingId",
                schema: "ratings",
                table: "participants_ratings_updates",
                column: "ParticipantRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_participants_ratings_updates_rating_id",
                schema: "ratings",
                table: "participants_ratings_updates",
                column: "rating_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events_ratings_updates",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "feedbacks",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "participants_ratings_updates",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "events_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "participants_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "organizers_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "gamers_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "participants",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "events_info",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "users_info",
                schema: "ratings");
        }
    }
}
