using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "evaluationservice");

            migrationBuilder.CreateTable(
                name: "checkpoint_evaluations",
                schema: "evaluationservice",
                columns: table => new
                {
                    EvaluationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckpointId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    Criteria = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PENDING"),
                    EvaluatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    RevisionNotes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkpoint_evaluations", x => x.EvaluationId);
                });

            migrationBuilder.CreateTable(
                name: "evaluation_criteria",
                schema: "evaluationservice",
                columns: table => new
                {
                    CriterionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "GENERAL"),
                    CriterionName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MaxScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 10m),
                    Weight = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 1m),
                    Order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluation_criteria", x => x.CriterionId);
                });

            migrationBuilder.CreateTable(
                name: "evaluation_templates",
                schema: "evaluationservice",
                columns: table => new
                {
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EvaluationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "GENERAL"),
                    Criteria = table.Column<string>(type: "text", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluation_templates", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "member_evaluations",
                schema: "evaluationservice",
                columns: table => new
                {
                    EvaluationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    Criteria = table.Column<string>(type: "text", nullable: true),
                    ContributionScore = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    EvaluatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    EvaluationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "FINAL"),
                    MilestoneId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_evaluations", x => x.EvaluationId);
                });

            migrationBuilder.CreateTable(
                name: "milestone_answer_evaluations",
                schema: "evaluationservice",
                columns: table => new
                {
                    EvaluationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PENDING"),
                    EvaluatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    RevisionNotes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_milestone_answer_evaluations", x => x.EvaluationId);
                });

            migrationBuilder.CreateTable(
                name: "milestone_answer_peer_reviews",
                schema: "evaluationservice",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsHelpful = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_milestone_answer_peer_reviews", x => x.ReviewId);
                });

            migrationBuilder.CreateTable(
                name: "peer_reviews",
                schema: "evaluationservice",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    RevieweeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamworkScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    CommunicationScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    TechnicalSkillScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    ContributionScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    OverallScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Strengths = table.Column<string>(type: "text", nullable: true),
                    AreasForImprovement = table.Column<string>(type: "text", nullable: true),
                    AdditionalComments = table.Column<string>(type: "text", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    MilestoneId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peer_reviews", x => x.ReviewId);
                });

            migrationBuilder.CreateTable(
                name: "team_evaluations",
                schema: "evaluationservice",
                columns: table => new
                {
                    EvaluationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    OverallScore = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    Criteria = table.Column<string>(type: "text", nullable: true),
                    EvaluatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    EvaluationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "FINAL"),
                    MilestoneId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_evaluations", x => x.EvaluationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_evaluations_CheckpointId",
                schema: "evaluationservice",
                table: "checkpoint_evaluations",
                column: "CheckpointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_evaluations_EvaluatedBy",
                schema: "evaluationservice",
                table: "checkpoint_evaluations",
                column: "EvaluatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoint_evaluations_Status",
                schema: "evaluationservice",
                table: "checkpoint_evaluations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_criteria_EvaluationType",
                schema: "evaluationservice",
                table: "evaluation_criteria",
                column: "EvaluationType");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_criteria_IsActive",
                schema: "evaluationservice",
                table: "evaluation_criteria",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_criteria_Order",
                schema: "evaluationservice",
                table: "evaluation_criteria",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_templates_EvaluationType",
                schema: "evaluationservice",
                table: "evaluation_templates",
                column: "EvaluationType");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_templates_IsActive",
                schema: "evaluationservice",
                table: "evaluation_templates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_templates_IsPublic",
                schema: "evaluationservice",
                table: "evaluation_templates",
                column: "IsPublic");

            migrationBuilder.CreateIndex(
                name: "IX_member_evaluations_EvaluatedBy",
                schema: "evaluationservice",
                table: "member_evaluations",
                column: "EvaluatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_member_evaluations_EvaluationType",
                schema: "evaluationservice",
                table: "member_evaluations",
                column: "EvaluationType");

            migrationBuilder.CreateIndex(
                name: "IX_member_evaluations_StudentId",
                schema: "evaluationservice",
                table: "member_evaluations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_member_evaluations_TeamId",
                schema: "evaluationservice",
                table: "member_evaluations",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answer_evaluations_AnswerId",
                schema: "evaluationservice",
                table: "milestone_answer_evaluations",
                column: "AnswerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answer_evaluations_EvaluatedBy",
                schema: "evaluationservice",
                table: "milestone_answer_evaluations",
                column: "EvaluatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answer_evaluations_Status",
                schema: "evaluationservice",
                table: "milestone_answer_evaluations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answer_peer_reviews_AnswerId",
                schema: "evaluationservice",
                table: "milestone_answer_peer_reviews",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answer_peer_reviews_AnswerId_ReviewerId",
                schema: "evaluationservice",
                table: "milestone_answer_peer_reviews",
                columns: new[] { "AnswerId", "ReviewerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_milestone_answer_peer_reviews_ReviewerId",
                schema: "evaluationservice",
                table: "milestone_answer_peer_reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_peer_reviews_MilestoneId",
                schema: "evaluationservice",
                table: "peer_reviews",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_peer_reviews_RevieweeId",
                schema: "evaluationservice",
                table: "peer_reviews",
                column: "RevieweeId");

            migrationBuilder.CreateIndex(
                name: "IX_peer_reviews_ReviewerId",
                schema: "evaluationservice",
                table: "peer_reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_peer_reviews_TeamId",
                schema: "evaluationservice",
                table: "peer_reviews",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_peer_reviews_TeamId_ReviewerId_RevieweeId_MilestoneId",
                schema: "evaluationservice",
                table: "peer_reviews",
                columns: new[] { "TeamId", "ReviewerId", "RevieweeId", "MilestoneId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_evaluations_EvaluatedBy",
                schema: "evaluationservice",
                table: "team_evaluations",
                column: "EvaluatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_team_evaluations_EvaluationType",
                schema: "evaluationservice",
                table: "team_evaluations",
                column: "EvaluationType");

            migrationBuilder.CreateIndex(
                name: "IX_team_evaluations_MilestoneId",
                schema: "evaluationservice",
                table: "team_evaluations",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_team_evaluations_TeamId",
                schema: "evaluationservice",
                table: "team_evaluations",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkpoint_evaluations",
                schema: "evaluationservice");

            migrationBuilder.DropTable(
                name: "evaluation_criteria",
                schema: "evaluationservice");

            migrationBuilder.DropTable(
                name: "evaluation_templates",
                schema: "evaluationservice");

            migrationBuilder.DropTable(
                name: "member_evaluations",
                schema: "evaluationservice");

            migrationBuilder.DropTable(
                name: "milestone_answer_evaluations",
                schema: "evaluationservice");

            migrationBuilder.DropTable(
                name: "milestone_answer_peer_reviews",
                schema: "evaluationservice");

            migrationBuilder.DropTable(
                name: "peer_reviews",
                schema: "evaluationservice");

            migrationBuilder.DropTable(
                name: "team_evaluations",
                schema: "evaluationservice");
        }
    }
}
