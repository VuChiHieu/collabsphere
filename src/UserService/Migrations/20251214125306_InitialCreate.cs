using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "userservice");

            migrationBuilder.CreateTable(
                name: "departments",
                schema: "userservice",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DepartmentCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HeadOfDepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "userservice",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Module = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "userservice",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "userservice",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Avatar = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "userservice",
                columns: table => new
                {
                    RolePermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => x.RolePermissionId);
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "userservice",
                        principalTable: "permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "userservice",
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                schema: "userservice",
                columns: table => new
                {
                    TokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeviceInfo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "userservice",
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profiles",
                schema: "userservice",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LecturerCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StaffCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profiles", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_user_profiles_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "userservice",
                        principalTable: "departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_user_profiles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "userservice",
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "userservice",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    AssignedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_user_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "userservice",
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "userservice",
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_DepartmentCode",
                schema: "userservice",
                table: "departments",
                column: "DepartmentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_departments_DepartmentName",
                schema: "userservice",
                table: "departments",
                column: "DepartmentName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_departments_HeadOfDepartmentId",
                schema: "userservice",
                table: "departments",
                column: "HeadOfDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_Module",
                schema: "userservice",
                table: "permissions",
                column: "Module");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_PermissionName",
                schema: "userservice",
                table: "permissions",
                column: "PermissionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_ExpiresAt",
                schema: "userservice",
                table: "refresh_tokens",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_Token",
                schema: "userservice",
                table: "refresh_tokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_UserId",
                schema: "userservice",
                table: "refresh_tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_PermissionId",
                schema: "userservice",
                table: "role_permissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_RoleId_PermissionId",
                schema: "userservice",
                table: "role_permissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_RoleName",
                schema: "userservice",
                table: "roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_DepartmentId",
                schema: "userservice",
                table: "user_profiles",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_LecturerCode",
                schema: "userservice",
                table: "user_profiles",
                column: "LecturerCode");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_StaffCode",
                schema: "userservice",
                table: "user_profiles",
                column: "StaffCode");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_StudentCode",
                schema: "userservice",
                table: "user_profiles",
                column: "StudentCode");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_UserId",
                schema: "userservice",
                table: "user_profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RoleId",
                schema: "userservice",
                table: "user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_UserId_RoleId",
                schema: "userservice",
                table: "user_roles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                schema: "userservice",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_IsActive",
                schema: "userservice",
                table: "users",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refresh_tokens",
                schema: "userservice");

            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "userservice");

            migrationBuilder.DropTable(
                name: "user_profiles",
                schema: "userservice");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "userservice");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "userservice");

            migrationBuilder.DropTable(
                name: "departments",
                schema: "userservice");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "userservice");

            migrationBuilder.DropTable(
                name: "users",
                schema: "userservice");
        }
    }
}
