using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonAssets.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonCarEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Person_PersonId",
                schema: "ass",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_PersonId",
                schema: "ass",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "PersonId",
                schema: "ass",
                table: "Car");

            migrationBuilder.CreateTable(
                name: "PersonCar",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCar", x => new { x.PersonId, x.CarId });
                    table.ForeignKey(
                        name: "FK_PersonCar_Car_CarId",
                        column: x => x.CarId,
                        principalSchema: "ass",
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCar_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ass",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonCar_CarId",
                table: "PersonCar",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonCar");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                schema: "ass",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Car_PersonId",
                schema: "ass",
                table: "Car",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Person_PersonId",
                schema: "ass",
                table: "Car",
                column: "PersonId",
                principalSchema: "ass",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
