using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class renameCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idConfrirmEmail",
                table: "authorizedUsers",
                newName: "idConfirmEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idConfirmEmail",
                table: "authorizedUsers",
                newName: "idConfrirmEmail");
        }
    }
}
