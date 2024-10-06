using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tlis.Cms.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cms");

            migrationBuilder.CreateTable(
                name: "image",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    width = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_image", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "membership",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membership", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "crop",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    width = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    image_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crop", x => x.id);
                    table.ForeignKey(
                        name: "fk_crop_image_image_id",
                        column: x => x.image_id,
                        principalSchema: "cms",
                        principalTable: "image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "show",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateOnly>(type: "date", nullable: false),
                    profile_image_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_show", x => x.id);
                    table.ForeignKey(
                        name: "fk_show_image_profile_image_id",
                        column: x => x.profile_image_id,
                        principalSchema: "cms",
                        principalTable: "image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cms_admin_access = table.Column<bool>(type: "boolean", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    abouth = table.Column<string>(type: "text", nullable: false),
                    profile_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    prefer_nickname_over_name = table.Column<bool>(type: "boolean", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_image_profile_image_id",
                        column: x => x.profile_image_id,
                        principalSchema: "cms",
                        principalTable: "image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "broadcast",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    external_url = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    show_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_broadcast", x => x.id);
                    table.ForeignKey(
                        name: "fk_broadcast_image_image_id",
                        column: x => x.image_id,
                        principalSchema: "cms",
                        principalTable: "image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_broadcast_show_show_id",
                        column: x => x.show_id,
                        principalSchema: "cms",
                        principalTable: "show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shows_users",
                schema: "cms",
                columns: table => new
                {
                    show_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shows_users", x => new { x.show_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_shows_users_show_show_id",
                        column: x => x.show_id,
                        principalSchema: "cms",
                        principalTable: "show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shows_users_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "cms",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_membership_history",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    membership_id = table.Column<Guid>(type: "uuid", nullable: false),
                    change_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_membership_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_membership_history_membership_membership_id",
                        column: x => x.membership_id,
                        principalSchema: "cms",
                        principalTable: "membership",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_membership_history_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "cms",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role_history",
                schema: "cms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    function_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    function_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_history_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "cms",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_history_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "cms",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "cms",
                table: "membership",
                columns: new[] { "id", "status" },
                values: new object[,]
                {
                    { new Guid("80126b05-9dab-4709-aa6a-39baa5bafe79"), "Active" },
                    { new Guid("a7c0bea2-2812-40b6-9836-d4b5accae95a"), "Archive" },
                    { new Guid("cfaeecff-d26b-44f2-bfa1-c80ab79983a9"), "Postponed" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_broadcast_image_id",
                schema: "cms",
                table: "broadcast",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_broadcast_show_id",
                schema: "cms",
                table: "broadcast",
                column: "show_id");

            migrationBuilder.CreateIndex(
                name: "ix_crop_image_id",
                schema: "cms",
                table: "crop",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_membership_id",
                schema: "cms",
                table: "membership",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_id",
                schema: "cms",
                table: "roles",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_show_name",
                schema: "cms",
                table: "show",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_show_profile_image_id",
                schema: "cms",
                table: "show",
                column: "profile_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_shows_users_show_id",
                schema: "cms",
                table: "shows_users",
                column: "show_id");

            migrationBuilder.CreateIndex(
                name: "ix_shows_users_user_id",
                schema: "cms",
                table: "shows_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_firstname_lastname_nickname",
                schema: "cms",
                table: "user",
                columns: new[] { "firstname", "lastname", "nickname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_id",
                schema: "cms",
                table: "user",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_profile_image_id",
                schema: "cms",
                table: "user",
                column: "profile_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_id",
                schema: "cms",
                table: "user_membership_history",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_membership_id",
                schema: "cms",
                table: "user_membership_history",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_user_id",
                schema: "cms",
                table: "user_membership_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_id",
                schema: "cms",
                table: "user_role_history",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_role_id",
                schema: "cms",
                table: "user_role_history",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_user_id",
                schema: "cms",
                table: "user_role_history",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "broadcast",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "crop",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "shows_users",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "user_membership_history",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "user_role_history",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "show",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "membership",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "user",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "image",
                schema: "cms");
        }
    }
}
