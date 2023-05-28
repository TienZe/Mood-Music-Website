using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBL3.Migrations
{
    public partial class UpdateSongTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "Singer",
                table: "Songs",
                newName: "Artist");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Artist",
                table: "Songs",
                newName: "Singer");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
