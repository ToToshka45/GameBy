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
                name: "RatingBase",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events_info",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    OrganizerRatingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_ratings_RatingBase_Id",
                        column: x => x.Id,
                        principalSchema: "ratings",
                        principalTable: "RatingBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_ratings_events_info_external_event_id",
                        column: x => x.external_event_id,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events_ratings_updates",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events_ratings_updates", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "gamers_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gamers_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gamers_ratings_RatingBase_Id",
                        column: x => x.Id,
                        principalSchema: "ratings",
                        principalTable: "RatingBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizers_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizers_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_organizers_ratings_RatingBase_Id",
                        column: x => x.Id,
                        principalSchema: "ratings",
                        principalTable: "RatingBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_info",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_user_id = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    OrganizerRatingId = table.Column<int>(type: "integer", nullable: false),
                    GamerRatingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_info_gamers_ratings_GamerRatingId",
                        column: x => x.GamerRatingId,
                        principalSchema: "ratings",
                        principalTable: "gamers_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_info_organizers_ratings_OrganizerRatingId",
                        column: x => x.OrganizerRatingId,
                        principalSchema: "ratings",
                        principalTable: "organizers_ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participants",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    external_participant_id = table.Column<int>(type: "integer", nullable: false),
                    external_user_id = table.Column<int>(type: "integer", nullable: false),
                    participation_state = table.Column<string>(type: "text", nullable: false),
                    EventInfoId = table.Column<int>(type: "integer", nullable: true),
                    UserInfoId = table.Column<int>(type: "integer", nullable: true)
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
                        name: "FK_participants_events_info_external_event_id",
                        column: x => x.external_event_id,
                        principalSchema: "ratings",
                        principalTable: "events_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participants_users_info_UserInfoId",
                        column: x => x.UserInfoId,
                        principalSchema: "ratings",
                        principalTable: "users_info",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_participants_users_info_external_user_id",
                        column: x => x.external_user_id,
                        principalSchema: "ratings",
                        principalTable: "users_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participants_ratings",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    external_participant_id = table.Column<int>(type: "integer", nullable: false),
                    GamerRatingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participants_ratings_RatingBase_Id",
                        column: x => x.Id,
                        principalSchema: "ratings",
                        principalTable: "RatingBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participants_ratings_gamers_ratings_GamerRatingId",
                        column: x => x.GamerRatingId,
                        principalSchema: "ratings",
                        principalTable: "gamers_ratings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_participants_ratings_participants_external_participant_id",
                        column: x => x.external_participant_id,
                        principalSchema: "ratings",
                        principalTable: "participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingUpdate",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<float>(type: "numeric(3,2)", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    external_event_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EventRatingId = table.Column<int>(type: "integer", nullable: true),
                    ParticipantRatingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingUpdate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingUpdate_events_ratings_EventRatingId",
                        column: x => x.EventRatingId,
                        principalSchema: "ratings",
                        principalTable: "events_ratings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RatingUpdate_participants_ratings_ParticipantRatingId",
                        column: x => x.ParticipantRatingId,
                        principalSchema: "ratings",
                        principalTable: "participants_ratings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "participants_ratings_updates",
                schema: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants_ratings_updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participants_ratings_updates_RatingUpdate_Id",
                        column: x => x.Id,
                        principalSchema: "ratings",
                        principalTable: "RatingUpdate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_events_ratings_external_event_id",
                schema: "ratings",
                table: "events_ratings",
                column: "external_event_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_ratings_OrganizerRatingId",
                schema: "ratings",
                table: "events_ratings",
                column: "OrganizerRatingId");

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
                name: "IX_gamers_ratings_external_user_id",
                schema: "ratings",
                table: "gamers_ratings",
                column: "external_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_organizers_ratings_external_user_id",
                schema: "ratings",
                table: "organizers_ratings",
                column: "external_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_participants_EventInfoId",
                schema: "ratings",
                table: "participants",
                column: "EventInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_participants_external_event_id",
                schema: "ratings",
                table: "participants",
                column: "external_event_id");

            migrationBuilder.CreateIndex(
                name: "IX_participants_external_user_id",
                schema: "ratings",
                table: "participants",
                column: "external_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_participants_UserInfoId",
                schema: "ratings",
                table: "participants",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_participants_ratings_external_participant_id",
                schema: "ratings",
                table: "participants_ratings",
                column: "external_participant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_participants_ratings_GamerRatingId",
                schema: "ratings",
                table: "participants_ratings",
                column: "GamerRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingUpdate_EventRatingId",
                schema: "ratings",
                table: "RatingUpdate",
                column: "EventRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingUpdate_ParticipantRatingId",
                schema: "ratings",
                table: "RatingUpdate",
                column: "ParticipantRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_users_info_GamerRatingId",
                schema: "ratings",
                table: "users_info",
                column: "GamerRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_users_info_OrganizerRatingId",
                schema: "ratings",
                table: "users_info",
                column: "OrganizerRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_info_events_ratings_Id",
                schema: "ratings",
                table: "events_info",
                column: "Id",
                principalSchema: "ratings",
                principalTable: "events_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_events_ratings_organizers_ratings_OrganizerRatingId",
                schema: "ratings",
                table: "events_ratings",
                column: "OrganizerRatingId",
                principalSchema: "ratings",
                principalTable: "organizers_ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_events_ratings_updates_RatingUpdate_Id",
                schema: "ratings",
                table: "events_ratings_updates",
                column: "Id",
                principalSchema: "ratings",
                principalTable: "RatingUpdate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_users_info_UserInfoId",
                schema: "ratings",
                table: "feedbacks",
                column: "UserInfoId",
                principalSchema: "ratings",
                principalTable: "users_info",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_users_info_UserInfoId1",
                schema: "ratings",
                table: "feedbacks",
                column: "UserInfoId1",
                principalSchema: "ratings",
                principalTable: "users_info",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_gamers_ratings_users_info_external_user_id",
                schema: "ratings",
                table: "gamers_ratings",
                column: "external_user_id",
                principalSchema: "ratings",
                principalTable: "users_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_organizers_ratings_users_info_external_user_id",
                schema: "ratings",
                table: "organizers_ratings",
                column: "external_user_id",
                principalSchema: "ratings",
                principalTable: "users_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_participants_participants_ratings_Id",
                schema: "ratings",
                table: "participants",
                column: "Id",
                principalSchema: "ratings",
                principalTable: "participants_ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_info_events_ratings_Id",
                schema: "ratings",
                table: "events_info");

            migrationBuilder.DropForeignKey(
                name: "FK_gamers_ratings_RatingBase_Id",
                schema: "ratings",
                table: "gamers_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_organizers_ratings_RatingBase_Id",
                schema: "ratings",
                table: "organizers_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_ratings_RatingBase_Id",
                schema: "ratings",
                table: "participants_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_events_info_EventInfoId",
                schema: "ratings",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_events_info_external_event_id",
                schema: "ratings",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_users_info_organizers_ratings_OrganizerRatingId",
                schema: "ratings",
                table: "users_info");

            migrationBuilder.DropForeignKey(
                name: "FK_gamers_ratings_users_info_external_user_id",
                schema: "ratings",
                table: "gamers_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_users_info_UserInfoId",
                schema: "ratings",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_users_info_external_user_id",
                schema: "ratings",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_participants_ratings_Id",
                schema: "ratings",
                table: "participants");

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
                name: "RatingUpdate",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "events_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "RatingBase",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "events_info",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "organizers_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "users_info",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "participants_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "gamers_ratings",
                schema: "ratings");

            migrationBuilder.DropTable(
                name: "participants",
                schema: "ratings");
        }
    }
}
