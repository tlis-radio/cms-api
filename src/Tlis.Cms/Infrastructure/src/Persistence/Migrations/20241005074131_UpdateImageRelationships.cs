using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_broadcast_image_image_id",
                schema: "cms",
                table: "broadcast");

            migrationBuilder.DropForeignKey(
                name: "fk_show_image_profile_image_id",
                schema: "cms",
                table: "show");

            migrationBuilder.DropForeignKey(
                name: "fk_user_image_profile_image_id",
                schema: "cms",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_user_profile_image_id",
                schema: "cms",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_show_profile_image_id",
                schema: "cms",
                table: "show");

            migrationBuilder.DropIndex(
                name: "ix_broadcast_image_id",
                schema: "cms",
                table: "broadcast");

            migrationBuilder.CreateIndex(
                name: "ix_user_profile_image_id",
                schema: "cms",
                table: "user",
                column: "profile_image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_show_profile_image_id",
                schema: "cms",
                table: "show",
                column: "profile_image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_broadcast_image_id",
                schema: "cms",
                table: "broadcast",
                column: "image_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_broadcast_image_image_id",
                schema: "cms",
                table: "broadcast",
                column: "image_id",
                principalSchema: "cms",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_show_image_profile_image_id",
                schema: "cms",
                table: "show",
                column: "profile_image_id",
                principalSchema: "cms",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_image_profile_image_id",
                schema: "cms",
                table: "user",
                column: "profile_image_id",
                principalSchema: "cms",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_broadcast_image_image_id",
                schema: "cms",
                table: "broadcast");

            migrationBuilder.DropForeignKey(
                name: "fk_show_image_profile_image_id",
                schema: "cms",
                table: "show");

            migrationBuilder.DropForeignKey(
                name: "fk_user_image_profile_image_id",
                schema: "cms",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_user_profile_image_id",
                schema: "cms",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_show_profile_image_id",
                schema: "cms",
                table: "show");

            migrationBuilder.DropIndex(
                name: "ix_broadcast_image_id",
                schema: "cms",
                table: "broadcast");

            migrationBuilder.CreateIndex(
                name: "ix_user_profile_image_id",
                schema: "cms",
                table: "user",
                column: "profile_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_show_profile_image_id",
                schema: "cms",
                table: "show",
                column: "profile_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_broadcast_image_id",
                schema: "cms",
                table: "broadcast",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "fk_broadcast_image_image_id",
                schema: "cms",
                table: "broadcast",
                column: "image_id",
                principalSchema: "cms",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_show_image_profile_image_id",
                schema: "cms",
                table: "show",
                column: "profile_image_id",
                principalSchema: "cms",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_user_image_profile_image_id",
                schema: "cms",
                table: "user",
                column: "profile_image_id",
                principalSchema: "cms",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
