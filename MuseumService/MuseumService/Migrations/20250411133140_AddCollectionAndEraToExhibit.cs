using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuseumService.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionAndEraToExhibit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Collection",
                table: "Exhibits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Era",
                table: "Exhibits",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collection",
                table: "Exhibits");

            migrationBuilder.DropColumn(
                name: "Era",
                table: "Exhibits");
        }
    }
}
