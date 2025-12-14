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
            migrationBuilder.CreateTable(
                name: "teams",
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
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_members",
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
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_milestones",
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
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_progress_logs",
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
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkpoint_submissions",
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
                        principalTable: "checkpoints",
                        principalColumn: "CheckpointId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkpoint_assignments",
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
                        principalTable: "checkpoints",
                        principalColumn: "CheckpointId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkpoint_assignments_team_members_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "team_members",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "milestone_questions",
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
                        principalTable: "team_milestones",
                        principalColumn: "TeamMilestoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "milestone_answers",
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
                        principalTable: "milestone_questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_milestone_answers_team_members_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "team_members",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_CheckpointId",
                table: "checkpoint_assignments",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_CheckpointId_TeamMemberId",
                table: "checkpoint_assignments",
                columns: new[] { "CheckpointId", "TeamMemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_Status",
                table: "checkpoint_assignments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_assignments_TeamMemberId",
                table: "checkpoint_assignments",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_CheckpointId",
                table: "checkpoint_submissions",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_IsLatest",
                table: "checkpoint_submissions",
                column: "IsLatest");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_SubmittedBy",
                table: "checkpoint_submissions",
                column: "SubmittedBy");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_submissions_Version",
                table: "checkpoint_submissions",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoints_DueDate",
                table: "checkpoints",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoints_Status",
                table: "checkpoints",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoints_TeamId",
                table: "checkpoints",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answers_QuestionId",
                table: "milestone_answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answers_QuestionId_TeamMemberId",
                table: "milestone_answers",
                columns: new[] { "QuestionId", "TeamMemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answers_TeamMemberId",
                table: "milestone_answers",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_questions_Order",
                table: "milestone_questions",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_questions_TeamMilestoneId",
                table: "milestone_questions",
                column: "TeamMilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_Status",
                table: "team_members",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_StudentId",
                table: "team_members",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_TeamId",
                table: "team_members",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_TeamId_StudentId",
                table: "team_members",
                columns: new[] { "TeamId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_ProjectMilestoneId",
                table: "team_milestones",
                column: "ProjectMilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_Status",
                table: "team_milestones",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_TeamId",
                table: "team_milestones",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_team_milestones_TeamId_ProjectMilestoneId",
                table: "team_milestones",
                columns: new[] { "TeamId", "ProjectMilestoneId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_progress_logs_LoggedAt",
                table: "team_progress_logs",
                column: "LoggedAt");

            migrationBuilder.CreateIndex(
                name: "IX_team_progress_logs_TeamId",
                table: "team_progress_logs",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_ClassId",
                table: "teams",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_ProjectId",
                table: "teams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_Status",
                table: "teams",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_teams_TeamCode",
                table: "teams",
                column: "TeamCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teams_TeamLeaderId",
                table: "teams",
                column: "TeamLeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkpoint_assignments");

            migrationBuilder.DropTable(
                name: "checkpoint_submissions");

            migrationBuilder.DropTable(
                name: "milestone_answers");

            migrationBuilder.DropTable(
                name: "team_progress_logs");

            migrationBuilder.DropTable(
                name: "checkpoints");

            migrationBuilder.DropTable(
                name: "milestone_questions");

            migrationBuilder.DropTable(
                name: "team_members");

            migrationBuilder.DropTable(
                name: "team_milestones");

            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
