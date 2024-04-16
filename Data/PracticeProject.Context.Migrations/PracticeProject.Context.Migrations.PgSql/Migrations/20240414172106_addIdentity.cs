using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PracticeProject.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class addIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authorizedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "authorizedUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "authorizedUsers",
                column: "NormalizedUserName",
                unique: true);

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
                name: "authorizedUsers_roles");

            migrationBuilder.DropTable(
                name: "authorizedUsers");
        }
    }
}
