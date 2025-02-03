using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonAssets.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedPropToAllEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ass",
                table: "Person",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ass",
                table: "Car",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ass",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ass",
                table: "Car");
        }
    }
}
