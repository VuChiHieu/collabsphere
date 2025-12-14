using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollaborationService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workspaces",
                columns: table => new
                {
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ViewMode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "KANBAN"),
                    Settings = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspaces", x => x.WorkspaceId);
                });

            migrationBuilder.CreateTable(
                name: "activity_logs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Changes = table.Column<string>(type: "text", nullable: true),
                    PerformedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    PerformedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity_logs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_activity_logs_workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "workspaces",
                        principalColumn: "WorkspaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boards", x => x.BoardId);
                    table.ForeignKey(
                        name: "FK_boards_workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "workspaces",
                        principalColumn: "WorkspaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sprints",
                columns: table => new
                {
                    SprintId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Goals = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PLANNED"),
                    CompletionRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    TotalStoryPoints = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CompletedStoryPoints = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sprints", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_sprints_workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "workspaces",
                        principalColumn: "WorkspaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "columns",
                columns: table => new
                {
                    ColumnId = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    ColumnName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ColumnType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "CUSTOM"),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    WipLimit = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_columns", x => x.ColumnId);
                    table.ForeignKey(
                        name: "FK_columns_boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    ColumnId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintId = table.Column<Guid>(type: "uuid", nullable: true),
                    CardTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CardType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TASK"),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "MEDIUM"),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TODO"),
                    StoryPoints = table.Column<int>(type: "integer", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_cards_columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "columns",
                        principalColumn: "ColumnId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cards_sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "sprints",
                        principalColumn: "SprintId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "card_assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    AssignedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_card_assignments_cards_CardId",
                        column: x => x.CardId,
                        principalTable: "cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card_attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UploadedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_attachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_card_attachments_cards_CardId",
                        column: x => x.CardId,
                        principalTable: "cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card_comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentText = table.Column<string>(type: "text", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_card_comments_card_comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "card_comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_card_comments_cards_CardId",
                        column: x => x.CardId,
                        principalTable: "cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TODO"),
                    EstimatedHours = table.Column<int>(type: "integer", nullable: true),
                    ActualHours = table.Column<int>(type: "integer", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_tasks_cards_CardId",
                        column: x => x.CardId,
                        principalTable: "cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subtasks",
                columns: table => new
                {
                    SubtaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubtaskTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TODO"),
                    Order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subtasks", x => x.SubtaskId);
                    table.ForeignKey(
                        name: "FK_subtasks_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    AssignedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_task_assignments_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_logs_ActionType",
                table: "activity_logs",
                column: "ActionType");

            migrationBuilder.CreateIndex(
                name: "IX_activity_logs_EntityId",
                table: "activity_logs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_activity_logs_EntityType",
                table: "activity_logs",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_activity_logs_PerformedAt",
                table: "activity_logs",
                column: "PerformedAt");

            migrationBuilder.CreateIndex(
                name: "IX_activity_logs_PerformedBy",
                table: "activity_logs",
                column: "PerformedBy");

            migrationBuilder.CreateIndex(
                name: "IX_activity_logs_WorkspaceId",
                table: "activity_logs",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_boards_IsDefault",
                table: "boards",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "IX_boards_Order",
                table: "boards",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_boards_WorkspaceId",
                table: "boards",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_card_assignments_AssigneeId",
                table: "card_assignments",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_card_assignments_CardId",
                table: "card_assignments",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_card_assignments_CardId_AssigneeId",
                table: "card_assignments",
                columns: new[] { "CardId", "AssigneeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_card_attachments_CardId",
                table: "card_attachments",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_card_attachments_UploadedBy",
                table: "card_attachments",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_card_comments_CardId",
                table: "card_comments",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_card_comments_ParentCommentId",
                table: "card_comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_card_comments_UserId",
                table: "card_comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_cards_ColumnId",
                table: "cards",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_cards_DueDate",
                table: "cards",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_cards_IsArchived",
                table: "cards",
                column: "IsArchived");

            migrationBuilder.CreateIndex(
                name: "IX_cards_Priority",
                table: "cards",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_cards_SprintId",
                table: "cards",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_cards_Status",
                table: "cards",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_columns_BoardId",
                table: "columns",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_columns_ColumnType",
                table: "columns",
                column: "ColumnType");

            migrationBuilder.CreateIndex(
                name: "IX_columns_Order",
                table: "columns",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_sprints_EndDate",
                table: "sprints",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_sprints_StartDate",
                table: "sprints",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_sprints_Status",
                table: "sprints",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_sprints_WorkspaceId",
                table: "sprints",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_subtasks_IsCompleted",
                table: "subtasks",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_subtasks_Order",
                table: "subtasks",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_subtasks_TaskId",
                table: "subtasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_task_assignments_AssigneeId",
                table: "task_assignments",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_task_assignments_TaskId",
                table: "task_assignments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_task_assignments_TaskId_AssigneeId",
                table: "task_assignments",
                columns: new[] { "TaskId", "AssigneeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_CardId",
                table: "tasks",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_DueDate",
                table: "tasks",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_IsCompleted",
                table: "tasks",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_Status",
                table: "tasks",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_workspaces_IsActive",
                table: "workspaces",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_workspaces_TeamId",
                table: "workspaces",
                column: "TeamId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity_logs");

            migrationBuilder.DropTable(
                name: "card_assignments");

            migrationBuilder.DropTable(
                name: "card_attachments");

            migrationBuilder.DropTable(
                name: "card_comments");

            migrationBuilder.DropTable(
                name: "subtasks");

            migrationBuilder.DropTable(
                name: "task_assignments");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "columns");

            migrationBuilder.DropTable(
                name: "sprints");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.DropTable(
                name: "workspaces");
        }
    }
}
