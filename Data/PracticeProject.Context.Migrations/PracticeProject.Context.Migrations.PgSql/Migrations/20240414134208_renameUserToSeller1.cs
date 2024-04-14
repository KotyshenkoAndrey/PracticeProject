using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class renameUserToSeller1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_car_user_SellerId",
                table: "car");

            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_user_SellerId",
                table: "viewingrequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "seller");

            migrationBuilder.RenameIndex(
                name: "IX_user_Uid",
                table: "seller",
                newName: "IX_seller_Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seller",
                table: "seller",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_car_seller_SellerId",
                table: "car",
                column: "SellerId",
                principalTable: "seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_seller_SellerId",
                table: "viewingrequest",
                column: "SellerId",
                principalTable: "seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_car_seller_SellerId",
                table: "car");

            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_seller_SellerId",
                table: "viewingrequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seller",
                table: "seller");

            migrationBuilder.RenameTable(
                name: "seller",
                newName: "user");

            migrationBuilder.RenameIndex(
                name: "IX_seller_Uid",
                table: "user",
                newName: "IX_user_Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_car_user_SellerId",
                table: "car",
                column: "SellerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_user_SellerId",
                table: "viewingrequest",
                column: "SellerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
