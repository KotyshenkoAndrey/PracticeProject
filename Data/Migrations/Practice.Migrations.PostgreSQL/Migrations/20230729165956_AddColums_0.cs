using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Practice.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddColums_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_AuthorDetail_DetailId",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Category_CategoriesId",
                table: "BookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Author_AuthorId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCategory",
                table: "BookCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorDetail",
                table: "AuthorDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_DetailId",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "BookCategory",
                newName: "books_categories");

            migrationBuilder.RenameTable(
                name: "AuthorDetail",
                newName: "author_details");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "authors");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Uid",
                table: "books",
                newName: "IX_books_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "books",
                newName: "IX_books_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Uid",
                table: "categories",
                newName: "IX_categories_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_CategoriesId",
                table: "books_categories",
                newName: "IX_books_categories_CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_Author_Uid",
                table: "authors",
                newName: "IX_authors_Uid");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "books",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "books",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "categories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "authors",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "authors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books_categories",
                table: "books_categories",
                columns: new[] { "BooksId", "CategoriesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_author_details",
                table: "author_details",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authors",
                table: "authors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_authors_Name",
                table: "authors",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_authors_author_details_Id",
                table: "authors",
                column: "Id",
                principalTable: "author_details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books",
                column: "AuthorId",
                principalTable: "authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_books_categories_books_BooksId",
                table: "books_categories",
                column: "BooksId",
                principalTable: "books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_categories_categories_CategoriesId",
                table: "books_categories",
                column: "CategoriesId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_authors_author_details_Id",
                table: "authors");

            migrationBuilder.DropForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_categories_books_BooksId",
                table: "books_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_books_categories_categories_CategoriesId",
                table: "books_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books_categories",
                table: "books_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_authors",
                table: "authors");

            migrationBuilder.DropIndex(
                name: "IX_authors_Name",
                table: "authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_author_details",
                table: "author_details");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "books_categories",
                newName: "BookCategory");

            migrationBuilder.RenameTable(
                name: "authors",
                newName: "Author");

            migrationBuilder.RenameTable(
                name: "author_details",
                newName: "AuthorDetail");

            migrationBuilder.RenameIndex(
                name: "IX_books_Uid",
                table: "Books",
                newName: "IX_Books_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_books_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_Uid",
                table: "Category",
                newName: "IX_Category_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_books_categories_CategoriesId",
                table: "BookCategory",
                newName: "IX_BookCategory_CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_authors_Uid",
                table: "Author",
                newName: "IX_Author_Uid");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Category",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Author",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Author",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "Author",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCategory",
                table: "BookCategory",
                columns: new[] { "BooksId", "CategoriesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorDetail",
                table: "AuthorDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Author_DetailId",
                table: "Author",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_AuthorDetail_DetailId",
                table: "Author",
                column: "DetailId",
                principalTable: "AuthorDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Category_CategoriesId",
                table: "BookCategory",
                column: "CategoriesId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Author_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id");
        }
    }
}
