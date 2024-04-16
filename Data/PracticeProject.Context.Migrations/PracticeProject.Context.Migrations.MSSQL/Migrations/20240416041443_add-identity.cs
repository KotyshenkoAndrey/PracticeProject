using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Context.Migrations.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class addidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authorizedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "seller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_authorizedUsers_claims_authorizedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "authorizedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_authorizedUsers_logins_authorizedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "authorizedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_tokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_authorizedUsers_tokens_authorizedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "authorizedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_role_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_authorizedUsers_role_claims_authorizedUsers_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "authorizedUsers_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_role_owners",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers_role_owners", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_authorizedUsers_role_owners_authorizedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "authorizedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_authorizedUsers_role_owners_authorizedUsers_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "authorizedUsers_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_car_seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "viewingrequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewingrequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_viewingrequest_car_CarId",
                        column: x => x.CarId,
                        principalTable: "car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_viewingrequest_seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "authorizedUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "authorizedUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_authorizedUsers_claims_UserId",
                table: "authorizedUsers_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_authorizedUsers_logins_UserId",
                table: "authorizedUsers_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_authorizedUsers_role_claims_RoleId",
                table: "authorizedUsers_role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_authorizedUsers_role_owners_RoleId",
                table: "authorizedUsers_role_owners",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "authorizedUsers_roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_car_SellerId",
                table: "car",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_car_Uid",
                table: "car",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_seller_Uid",
                table: "seller",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_viewingrequest_CarId",
                table: "viewingrequest",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_viewingrequest_SellerId",
                table: "viewingrequest",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_viewingrequest_Uid",
                table: "viewingrequest",
                column: "Uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorizedUsers_claims");

            migrationBuilder.DropTable(
                name: "authorizedUsers_logins");

            migrationBuilder.DropTable(
                name: "authorizedUsers_role_claims");

            migrationBuilder.DropTable(
                name: "authorizedUsers_role_owners");

            migrationBuilder.DropTable(
                name: "authorizedUsers_tokens");

            migrationBuilder.DropTable(
                name: "viewingrequest");

            migrationBuilder.DropTable(
                name: "authorizedUsers_roles");

            migrationBuilder.DropTable(
                name: "authorizedUsers");

            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "seller");
        }
    }
}
