using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "teamservice");

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "teamservice",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TeamName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "FORMING"),
                    OverallProgress = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    CurrentMembers = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    TeamLeaderId = table.Column<Guid>(type: "uuid", nullable: true),
                    FormedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "checkpoints",
                schema: "teamservice",
                columns: table => new
                {
                    CheckpointId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckpointName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PENDING"),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Score = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    EvaluatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    EvaluatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkpoints", x => x.CheckpointId);
                    table.ForeignKey(
                        name: "FK_checkpoints_teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "teamservice",
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_members",
                schema: "teamservice",
                columns: table => new
                {
                    TeamMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "MEMBER"),
                    ContributionPercentage = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    LeftAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "ACTIVE"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_members", x => x.TeamMemberId);
                    table.ForeignKey(
                        name: "FK_team_members_teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "teamservice",
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_milestones",
                schema: "teamservice",
                columns: table => new
                {
                    TeamMilestoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectMilestoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "NOT_STARTED"),
                    Progress = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Score = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_milestones", x => x.TeamMilestoneId);
                    table.ForeignKey(
                        name: "FK_team_milestones_teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "teamservice",
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_progress_logs",
                schema: "teamservice",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressPercentage = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    LoggedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_progress_logs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_team_progress_logs_teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "teamservice",
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkpoint_submissions",
                schema: "teamservice",
                columns: table => new
                {
                    SubmissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckpointId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubmissionFiles = table.Column<string>(type: "text", nullable: true),
                    SubmissionNotes = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    SubmittedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsLatest = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkpoint_submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_checkpoint_submissions_checkpoints_CheckpointId",
                        column: x => x.CheckpointId,
                        principalSchema: "teamservice",
                        principalTable: "checkpoints",
                        principalColumn: "CheckpointId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkpoint_assignments",
                schema: "teamservice",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckpointId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    AssignedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "ASSIGNED"),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkpoint_assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_checkpoint_assignments_checkpoints_CheckpointId",
                        column: x => x.CheckpointId,
                        principalSchema: "teamservice",
                        principalTable: "checkpoints",
                        principalColumn: "CheckpointId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkpoint_assignments_team_members_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalSchema: "teamservice",
                        principalTable: "team_members",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "milestone_questions",
                schema: "teamservice",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamMilestoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    QuestionType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TEXT"),
                    Options = table.Column<string>(type: "text", nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    MaxScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 10m),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_milestone_questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_milestone_questions_team_milestones_TeamMilestoneId",
                        column: x => x.TeamMilestoneId,
                        principalSchema: "teamservice",
                        principalTable: "team_milestones",
                        principalColumn: "TeamMilestoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "milestone_answers",
                schema: "teamservice",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: true),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    Score = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EvaluatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    EvaluatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_milestone_answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_milestone_answers_milestone_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "teamservice",
                        principalTable: "milestone_questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_milestone_answers_team_members_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalSchema: "teamservice",
                        principalTable: "team_members",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_CheckpointId",
                schema: "teamservice",
                table: "checkpoint_assignments",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_CheckpointId_TeamMemberId",
                schema: "teamservice",
                table: "checkpoint_assignments",
                columns: new[] { "CheckpointId", "TeamMemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_Status",
                schema: "teamservice",
                table: "checkpoint_assignments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_TeamMemberId",
                schema: "teamservice",
                table: "checkpoint_assignments",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_CheckpointId",
                schema: "teamservice",
                table: "checkpoint_submissions",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_IsLatest",
                schema: "teamservice",
                table: "checkpoint_submissions",
                column: "IsLatest");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_SubmittedBy",
                schema: "teamservice",
                table: "checkpoint_submissions",
                column: "SubmittedBy");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_Version",
                schema: "teamservice",
                table: "checkpoint_submissions",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoints_DueDate",
                schema: "teamservice",
                table: "checkpoints",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoints_Status",
                schema: "teamservice",
                table: "checkpoints",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoints_TeamId",
                schema: "teamservice",
                table: "checkpoints",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answers_QuestionId",
                schema: "teamservice",
                table: "milestone_answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answers_QuestionId_TeamMemberId",
                schema: "teamservice",
                table: "milestone_answers",
                columns: new[] { "QuestionId", "TeamMemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answers_TeamMemberId",
                schema: "teamservice",
                table: "milestone_answers",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_questions_Order",
                schema: "teamservice",
                table: "milestone_questions",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_questions_TeamMilestoneId",
                schema: "teamservice",
                table: "milestone_questions",
                column: "TeamMilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_Status",
                schema: "teamservice",
                table: "team_members",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_StudentId",
                schema: "teamservice",
                table: "team_members",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_TeamId",
                schema: "teamservice",
                table: "team_members",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_TeamId_StudentId",
                schema: "teamservice",
                table: "team_members",
                columns: new[] { "TeamId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_ProjectMilestoneId",
                schema: "teamservice",
                table: "team_milestones",
                column: "ProjectMilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_Status",
                schema: "teamservice",
                table: "team_milestones",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_TeamId",
                schema: "teamservice",
                table: "team_milestones",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_TeamId_ProjectMilestoneId",
                schema: "teamservice",
                table: "team_milestones",
                columns: new[] { "TeamId", "ProjectMilestoneId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_progress_logs_LoggedAt",
                schema: "teamservice",
                table: "team_progress_logs",
                column: "LoggedAt");

            migrationBuilder.CreateIndex(
                name: "IX_team_progress_logs_TeamId",
                schema: "teamservice",
                table: "team_progress_logs",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_ClassId",
                schema: "teamservice",
                table: "teams",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_ProjectId",
                schema: "teamservice",
                table: "teams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_Status",
                schema: "teamservice",
                table: "teams",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_teams_TeamCode",
                schema: "teamservice",
                table: "teams",
                column: "TeamCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teams_TeamLeaderId",
                schema: "teamservice",
                table: "teams",
                column: "TeamLeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkpoint_assignments",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "checkpoint_submissions",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "milestone_answers",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "team_progress_logs",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "checkpoints",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "milestone_questions",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "team_members",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "team_milestones",
                schema: "teamservice");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "teamservice");
        }
    }
}
