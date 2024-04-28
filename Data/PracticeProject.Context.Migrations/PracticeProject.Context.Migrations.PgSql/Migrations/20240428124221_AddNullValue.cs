using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddNullValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "viewingrequest",
                newName: "StateConfirmed");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifedDate",
                table: "viewingrequest",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StateConfirmed",
                table: "viewingrequest",
                newName: "IsConfirmed");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifedDate",
                table: "viewingrequest",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
