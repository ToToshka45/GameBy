using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduserratingstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_participants_UserRating_Id",
                schema: "RatingServices",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_users_info_Id",
                schema: "RatingServices",
                table: "UserRating");

            migrationBuilder.DropForeignKey(
                name: "FK_users_rating_updates_UserRating_Id",
                schema: "RatingServices",
                table: "users_rating_updates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRating",
                schema: "RatingServices",
                table: "UserRating");

            migrationBuilder.RenameTable(
                name: "UserRating",
                schema: "RatingServices",
                newName: "user_ratings",
                newSchema: "RatingServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_ratings",
                schema: "RatingServices",
                table: "user_ratings",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameTable(
                name: "user_ratings",
                schema: "RatingServices",
                newName: "UserRating",
                newSchema: "RatingServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRating",
                schema: "RatingServices",
                table: "UserRating",
                column: "Id");

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
                name: "FK_UserRating_users_info_Id",
                schema: "RatingServices",
                table: "UserRating",
                column: "Id",
                principalSchema: "RatingServices",
                principalTable: "users_info",
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
    }
}
