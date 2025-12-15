using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fileservice");

            migrationBuilder.CreateTable(
                name: "resources",
                schema: "fileservice",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Scope = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "CLASS"),
                    ScopeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    DownloadCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ViewCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UploadedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resources", x => x.ResourceId);
                });

            migrationBuilder.CreateTable(
                name: "shared_documents",
                schema: "fileservice",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DocumentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TEXT"),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LockedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shared_documents", x => x.DocumentId);
                });

            migrationBuilder.CreateTable(
                name: "resource_accesses",
                schema: "fileservice",
                columns: table => new
                {
                    AccessId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "VIEW"),
                    AccessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IpAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_accesses", x => x.AccessId);
                    table.ForeignKey(
                        name: "FK_resource_accesses_resources_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "fileservice",
                        principalTable: "resources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "resource_versions",
                schema: "fileservice",
                columns: table => new
                {
                    VersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    ChangeDescription = table.Column<string>(type: "text", nullable: true),
                    UploadedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCurrent = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_versions", x => x.VersionId);
                    table.ForeignKey(
                        name: "FK_resource_versions_resources_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "fileservice",
                        principalTable: "resources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document_collaborators",
                schema: "fileservice",
                columns: table => new
                {
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Permission = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "VIEW"),
                    LastAccessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_collaborators", x => x.CollaboratorId);
                    table.ForeignKey(
                        name: "FK_document_collaborators_shared_documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "fileservice",
                        principalTable: "shared_documents",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_document_collaborators_DocumentId",
                schema: "fileservice",
                table: "document_collaborators",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_document_collaborators_DocumentId_UserId",
                schema: "fileservice",
                table: "document_collaborators",
                columns: new[] { "DocumentId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_document_collaborators_UserId",
                schema: "fileservice",
                table: "document_collaborators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_resource_accesses_AccessedAt",
                schema: "fileservice",
                table: "resource_accesses",
                column: "AccessedAt");

            migrationBuilder.CreateIndex(
                name: "IX_resource_accesses_AccessType",
                schema: "fileservice",
                table: "resource_accesses",
                column: "AccessType");

            migrationBuilder.CreateIndex(
                name: "IX_resource_accesses_ResourceId",
                schema: "fileservice",
                table: "resource_accesses",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_resource_accesses_UserId",
                schema: "fileservice",
                table: "resource_accesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_resource_versions_IsCurrent",
                schema: "fileservice",
                table: "resource_versions",
                column: "IsCurrent");

            migrationBuilder.CreateIndex(
                name: "IX_resource_versions_ResourceId",
                schema: "fileservice",
                table: "resource_versions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_resource_versions_ResourceId_VersionNumber",
                schema: "fileservice",
                table: "resource_versions",
                columns: new[] { "ResourceId", "VersionNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_resource_versions_VersionNumber",
                schema: "fileservice",
                table: "resource_versions",
                column: "VersionNumber");

            migrationBuilder.CreateIndex(
                name: "IX_resources_IsPublic",
                schema: "fileservice",
                table: "resources",
                column: "IsPublic");

            migrationBuilder.CreateIndex(
                name: "IX_resources_Scope",
                schema: "fileservice",
                table: "resources",
                column: "Scope");

            migrationBuilder.CreateIndex(
                name: "IX_resources_Scope_ScopeId",
                schema: "fileservice",
                table: "resources",
                columns: new[] { "Scope", "ScopeId" });

            migrationBuilder.CreateIndex(
                name: "IX_resources_ScopeId",
                schema: "fileservice",
                table: "resources",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_resources_UploadedBy",
                schema: "fileservice",
                table: "resources",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_shared_documents_IsLocked",
                schema: "fileservice",
                table: "shared_documents",
                column: "IsLocked");

            migrationBuilder.CreateIndex(
                name: "IX_shared_documents_LockedBy",
                schema: "fileservice",
                table: "shared_documents",
                column: "LockedBy");

            migrationBuilder.CreateIndex(
                name: "IX_shared_documents_TeamId",
                schema: "fileservice",
                table: "shared_documents",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document_collaborators",
                schema: "fileservice");

            migrationBuilder.DropTable(
                name: "resource_accesses",
                schema: "fileservice");

            migrationBuilder.DropTable(
                name: "resource_versions",
                schema: "fileservice");

            migrationBuilder.DropTable(
                name: "shared_documents",
                schema: "fileservice");

            migrationBuilder.DropTable(
                name: "resources",
                schema: "fileservice");
        }
    }
}
