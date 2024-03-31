using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class fixNoneNullValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_car_user_SellerId",
                table: "car");

            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest");

            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_user_UserId",
                table: "viewingrequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_viewingrequest",
                table: "viewingrequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_car",
                table: "car");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "viewingrequest",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_viewingrequest_UserId",
                table: "viewingrequest",
                newName: "IX_viewingrequest_SellerId");

            migrationBuilder.AlterColumn<int>(
                name: "ViewingRequestId",
                table: "viewingrequest",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "viewingrequest",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "Uid",
                table: "viewingrequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "user",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "car",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "car",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "car",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "car",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "car",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "Uid",
                table: "car",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_viewingrequest",
                table: "viewingrequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_car",
                table: "car",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_viewingrequest_Uid",
                table: "viewingrequest",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_car_Uid",
                table: "car",
                column: "Uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_car_user_SellerId",
                table: "car",
                column: "SellerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest",
                column: "CarId",
                principalTable: "car",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_car_user_SellerId",
                table: "car");

            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest");

            migrationBuilder.DropForeignKey(
                name: "FK_viewingrequest_user_SellerId",
                table: "viewingrequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_viewingrequest",
                table: "viewingrequest");

            migrationBuilder.DropIndex(
                name: "IX_viewingrequest_Uid",
                table: "viewingrequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_car",
                table: "car");

            migrationBuilder.DropIndex(
                name: "IX_car_Uid",
                table: "car");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "viewingrequest");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "viewingrequest");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "car");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "car");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "viewingrequest",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_viewingrequest_SellerId",
                table: "viewingrequest",
                newName: "IX_viewingrequest_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ViewingRequestId",
                table: "viewingrequest",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "user",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "car",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "car",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "car",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "car",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_viewingrequest",
                table: "viewingrequest",
                column: "ViewingRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_car",
                table: "car",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_car_user_SellerId",
                table: "car",
                column: "SellerId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_car_CarId",
                table: "viewingrequest",
                column: "CarId",
                principalTable: "car",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_viewingrequest_user_UserId",
                table: "viewingrequest",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
