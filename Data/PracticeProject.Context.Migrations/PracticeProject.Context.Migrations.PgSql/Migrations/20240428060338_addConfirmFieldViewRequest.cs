using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class addConfirmFieldViewRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsConfirmed",
                table: "viewingrequest",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "viewingrequest");
        }
    }
}
