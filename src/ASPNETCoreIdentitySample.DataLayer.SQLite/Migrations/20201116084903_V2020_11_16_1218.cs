using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNETCoreIdentitySample.DataLayer.SQLite.Migrations
{
    public partial class V2020_11_16_1218 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AppDataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FriendlyName = table.Column<string>(type: "TEXT", nullable: true),
                    XmlData = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    LogLevel = table.Column<string>(type: "TEXT", nullable: true),
                    Logger = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    StateJson = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSqlCache",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 449, nullable: false),
                    Value = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ExpiresAtTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(type: "INTEGER", nullable: true),
                    AbsoluteExpiration = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSqlCache", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    PhotoFileName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastVisitDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsEmailPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserUsedPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HashedPassword = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserUsedPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserUsedPasswords_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDataProtectionKeys_FriendlyName",
                table: "AppDataProtectionKeys",
                column: "FriendlyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_RoleId",
                table: "AppRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_ExpiresAtTime",
                schema: "dbo",
                table: "AppSqlCache",
                column: "ExpiresAtTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_UserId",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserUsedPasswords_UserId",
                table: "AppUserUsedPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDataProtectionKeys");

            migrationBuilder.DropTable(
                name: "AppLogItems");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppSqlCache",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "AppUserUsedPasswords");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
