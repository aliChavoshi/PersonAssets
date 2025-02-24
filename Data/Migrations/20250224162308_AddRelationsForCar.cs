using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonAssets.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsForCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                schema: "ass",
                table: "Car",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "ass",
                table: "Car",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_CreatedBy",
                schema: "ass",
                table: "Car",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ModifiedBy",
                schema: "ass",
                table: "Car",
                column: "ModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_AspNetUsers_CreatedBy",
                schema: "ass",
                table: "Car",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_AspNetUsers_ModifiedBy",
                schema: "ass",
                table: "Car",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_AspNetUsers_CreatedBy",
                schema: "ass",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_AspNetUsers_ModifiedBy",
                schema: "ass",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_CreatedBy",
                schema: "ass",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_ModifiedBy",
                schema: "ass",
                table: "Car");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                schema: "ass",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "ass",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
