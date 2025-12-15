using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "notificationservice");

            migrationBuilder.CreateTable(
                name: "email_notifications",
                schema: "notificationservice",
                columns: table => new
                {
                    EmailId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "GENERAL"),
                    RecipientEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Subject = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PENDING"),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RetryCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    TriggeredBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_notifications", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "notification_preferences",
                schema: "notificationservice",
                columns: table => new
                {
                    PreferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    InAppEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    PushEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    NotificationTypes = table.Column<string>(type: "text", nullable: true),
                    QuietHoursStart = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    QuietHoursEnd = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    EnableQuietHours = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EnableDailyDigest = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EnableWeeklyDigest = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DigestTime = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    TimeZone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_preferences", x => x.PreferenceId);
                });

            migrationBuilder.CreateTable(
                name: "notification_queues",
                schema: "notificationservice",
                columns: table => new
                {
                    QueueId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "EMAIL"),
                    Payload = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PENDING"),
                    ScheduledFor = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProcessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RetryCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MaxRetries = table.Column<int>(type: "integer", nullable: false, defaultValue: 3),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "NORMAL"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_queues", x => x.QueueId);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                schema: "notificationservice",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "INFO"),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "NORMAL"),
                    ActionUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDismissed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DismissedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TriggeredBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "user_device_tokens",
                schema: "notificationservice",
                columns: table => new
                {
                    TokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DeviceType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "WEB"),
                    DeviceName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    LastUsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_device_tokens", x => x.TokenId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_email_notifications_EmailType",
                schema: "notificationservice",
                table: "email_notifications",
                column: "EmailType");

            migrationBuilder.CreateIndex(
                name: "IX_email_notifications_SentAt",
                schema: "notificationservice",
                table: "email_notifications",
                column: "SentAt");

            migrationBuilder.CreateIndex(
                name: "IX_email_notifications_Status",
                schema: "notificationservice",
                table: "email_notifications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_email_notifications_UserId",
                schema: "notificationservice",
                table: "email_notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_notification_preferences_UserId",
                schema: "notificationservice",
                table: "notification_preferences",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notification_queues_NotificationType",
                schema: "notificationservice",
                table: "notification_queues",
                column: "NotificationType");

            migrationBuilder.CreateIndex(
                name: "IX_notification_queues_Priority",
                schema: "notificationservice",
                table: "notification_queues",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_notification_queues_ScheduledFor",
                schema: "notificationservice",
                table: "notification_queues",
                column: "ScheduledFor");

            migrationBuilder.CreateIndex(
                name: "IX_notification_queues_Status",
                schema: "notificationservice",
                table: "notification_queues",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_CreatedAt",
                schema: "notificationservice",
                table: "notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_IsRead",
                schema: "notificationservice",
                table: "notifications",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_NotificationType",
                schema: "notificationservice",
                table: "notifications",
                column: "NotificationType");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_Priority",
                schema: "notificationservice",
                table: "notifications",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_UserId",
                schema: "notificationservice",
                table: "notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_device_tokens_DeviceToken",
                schema: "notificationservice",
                table: "user_device_tokens",
                column: "DeviceToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_device_tokens_IsActive",
                schema: "notificationservice",
                table: "user_device_tokens",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_user_device_tokens_UserId",
                schema: "notificationservice",
                table: "user_device_tokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email_notifications",
                schema: "notificationservice");

            migrationBuilder.DropTable(
                name: "notification_preferences",
                schema: "notificationservice");

            migrationBuilder.DropTable(
                name: "notification_queues",
                schema: "notificationservice");

            migrationBuilder.DropTable(
                name: "notifications",
                schema: "notificationservice");

            migrationBuilder.DropTable(
                name: "user_device_tokens",
                schema: "notificationservice");
        }
    }
}
