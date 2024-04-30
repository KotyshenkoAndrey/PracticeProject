using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class fixDeleteCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest");

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest",
                column: "CarId",
                principalTable: "car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest");

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest",
                column: "CarId",
                principalTable: "car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
