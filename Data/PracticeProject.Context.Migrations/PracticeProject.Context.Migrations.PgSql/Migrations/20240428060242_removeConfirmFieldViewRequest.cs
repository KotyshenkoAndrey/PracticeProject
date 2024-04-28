using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class removeConfirmFieldViewRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_seller_SellerId",
                table: "viewingrequest");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "viewingrequest");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "viewingrequest",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_viewingrequest_SellerId",
                table: "viewingrequest",
                newName: "IX_viewingrequest_SenderId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifedDate",
                table: "viewingrequest",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_seller_SenderId",
                table: "viewingrequest",
                column: "SenderId",
                principalTable: "seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_seller_SenderId",
                table: "viewingrequest");

            migrationBuilder.DropColumn(
                name: "LastModifedDate",
                table: "viewingrequest");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "viewingrequest",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_viewingrequest_SenderId",
                table: "viewingrequest",
                newName: "IX_viewingrequest_SellerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "viewingrequest",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_seller_SellerId",
                table: "viewingrequest",
                column: "SellerId",
                principalTable: "seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
