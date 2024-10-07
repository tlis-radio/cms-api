using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImageUrlToFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "url",
                schema: "cms",
                table: "image",
                newName: "file_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_name",
                schema: "cms",
                table: "image",
                newName: "url");
        }
    }
}
