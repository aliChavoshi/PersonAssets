using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonAssets.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSchemaToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ass");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Person",
                newSchema: "ass");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Car",
                newSchema: "ass");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Person",
                schema: "ass",
                newName: "Person");

            migrationBuilder.RenameTable(
                name: "Car",
                schema: "ass",
                newName: "Car");
        }
    }
}
