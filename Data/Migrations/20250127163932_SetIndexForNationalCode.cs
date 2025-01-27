using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonAssets.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetIndexForNationalCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "ass",
                table: "Person",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Person_NationalCode",
                schema: "ass",
                table: "Person",
                column: "NationalCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_NationalCode",
                schema: "ass",
                table: "Person");

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "ass",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
