using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonAssets.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationOfCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonCar_Car_CarId",
                table: "PersonCar");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCar_Person_PersonId",
                table: "PersonCar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonCar",
                table: "PersonCar");

            migrationBuilder.RenameTable(
                name: "PersonCar",
                newName: "PersonCars");

            migrationBuilder.RenameIndex(
                name: "IX_PersonCar_CarId",
                table: "PersonCars",
                newName: "IX_PersonCars_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonCars",
                table: "PersonCars",
                columns: new[] { "PersonId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCars_Car_CarId",
                table: "PersonCars",
                column: "CarId",
                principalSchema: "ass",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCars_Person_PersonId",
                table: "PersonCars",
                column: "PersonId",
                principalSchema: "ass",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonCars_Car_CarId",
                table: "PersonCars");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCars_Person_PersonId",
                table: "PersonCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonCars",
                table: "PersonCars");

            migrationBuilder.RenameTable(
                name: "PersonCars",
                newName: "PersonCar");

            migrationBuilder.RenameIndex(
                name: "IX_PersonCars_CarId",
                table: "PersonCar",
                newName: "IX_PersonCar_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonCar",
                table: "PersonCar",
                columns: new[] { "PersonId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCar_Car_CarId",
                table: "PersonCar",
                column: "CarId",
                principalSchema: "ass",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCar_Person_PersonId",
                table: "PersonCar",
                column: "PersonId",
                principalSchema: "ass",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
