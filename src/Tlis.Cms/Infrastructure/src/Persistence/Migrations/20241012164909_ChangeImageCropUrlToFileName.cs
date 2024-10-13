using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImageCropUrlToFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "url",
                schema: "cms",
                table: "crop",
                newName: "file_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_name",
                schema: "cms",
                table: "crop",
                newName: "url");
        }
    }
}
