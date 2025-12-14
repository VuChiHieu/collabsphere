using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProjectName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Overview = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    ExpectedOutcomes = table.Column<string>(type: "text", nullable: true),
                    TechnicalStack = table.Column<string>(type: "text", nullable: true),
                    DifficultyLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "MEDIUM"),
                    EstimatedDuration = table.Column<int>(type: "integer", nullable: false),
                    MinTeamSize = table.Column<int>(type: "integer", nullable: false, defaultValue: 4),
                    MaxTeamSize = table.Column<int>(type: "integer", nullable: false, defaultValue: 6),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "DRAFT"),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    RejectionReason = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "class_projects",
                columns: table => new
                {
                    ClassProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    AssignedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_projects", x => x.ClassProjectId);
                    table.ForeignKey(
                        name: "FK_class_projects_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_ai_generations",
                columns: table => new
                {
                    GenerationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    PromptUsed = table.Column<string>(type: "text", nullable: false),
                    GeneratedContent = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "gemini-pro"),
                    TokensUsed = table.Column<int>(type: "integer", nullable: true),
                    GeneratedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_ai_generations", x => x.GenerationId);
                    table.ForeignKey(
                        name: "FK_project_ai_generations_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_milestones",
                columns: table => new
                {
                    MilestoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    MilestoneCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MilestoneName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Objectives = table.Column<string>(type: "text", nullable: true),
                    Deliverables = table.Column<string>(type: "text", nullable: true),
                    DurationWeeks = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_milestones", x => x.MilestoneId);
                    table.ForeignKey(
                        name: "FK_project_milestones_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_objectives",
                columns: table => new
                {
                    ObjectiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectiveCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BloomLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Weight = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_objectives", x => x.ObjectiveId);
                    table.ForeignKey(
                        name: "FK_project_objectives_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_class_projects_ClassId",
                table: "class_projects",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_class_projects_ClassId_ProjectId",
                table: "class_projects",
                columns: new[] { "ClassId", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_class_projects_ProjectId",
                table: "class_projects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_project_ai_generations_CreatedAt",
                table: "project_ai_generations",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_project_ai_generations_ProjectId",
                table: "project_ai_generations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_project_milestones_Order",
                table: "project_milestones",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_project_milestones_ProjectId",
                table: "project_milestones",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_project_milestones_ProjectId_MilestoneCode",
                table: "project_milestones",
                columns: new[] { "ProjectId", "MilestoneCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_objectives_ProjectId",
                table: "project_objectives",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_project_objectives_ProjectId_ObjectiveCode",
                table: "project_objectives",
                columns: new[] { "ProjectId", "ObjectiveCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_CreatedBy",
                table: "projects",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_projects_ProjectCode",
                table: "projects",
                column: "ProjectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_Status",
                table: "projects",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_projects_SubjectId",
                table: "projects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_SyllabusId",
                table: "projects",
                column: "SyllabusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "class_projects");

            migrationBuilder.DropTable(
                name: "project_ai_generations");

            migrationBuilder.DropTable(
                name: "project_milestones");

            migrationBuilder.DropTable(
                name: "project_objectives");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
