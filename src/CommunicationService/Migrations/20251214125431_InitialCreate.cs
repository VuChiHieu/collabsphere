using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunicationService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "communicationservice");

            migrationBuilder.CreateTable(
                name: "channels",
                schema: "communicationservice",
                columns: table => new
                {
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ChannelType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PUBLIC"),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    MemberCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    AvatarUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channels", x => x.ChannelId);
                });

            migrationBuilder.CreateTable(
                name: "direct_messages",
                schema: "communicationservice",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageText = table.Column<string>(type: "text", nullable: false),
                    MessageType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TEXT"),
                    Attachments = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsEdited = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EditedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direct_messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "channel_members",
                schema: "communicationservice",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "MEMBER"),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    LastReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsMuted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channel_members", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_channel_members_channels_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "communicationservice",
                        principalTable: "channels",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "meetings",
                schema: "communicationservice",
                columns: table => new
                {
                    MeetingId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: true),
                    MeetingTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MeetingType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "VIDEO"),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "SCHEDULED"),
                    MeetingUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ScheduledStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ScheduledEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    Agenda = table.Column<string>(type: "text", nullable: true),
                    RecordingUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meetings", x => x.MeetingId);
                    table.ForeignKey(
                        name: "FK_meetings_channels_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "communicationservice",
                        principalTable: "channels",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "communicationservice",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageText = table.Column<string>(type: "text", nullable: false),
                    MessageType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "TEXT"),
                    Attachments = table.Column<string>(type: "text", nullable: true),
                    ParentMessageId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsEdited = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EditedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_messages_channels_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "communicationservice",
                        principalTable: "channels",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_messages_ParentMessageId",
                        column: x => x.ParentMessageId,
                        principalSchema: "communicationservice",
                        principalTable: "messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "meeting_notes",
                schema: "communicationservice",
                columns: table => new
                {
                    NoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uuid", nullable: false),
                    NoteContent = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsShared = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EditedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meeting_notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_meeting_notes_meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "communicationservice",
                        principalTable: "meetings",
                        principalColumn: "MeetingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "meeting_participants",
                schema: "communicationservice",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "ATTENDEE"),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "INVITED"),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LeftAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    HasAttended = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meeting_participants", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_meeting_participants_meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "communicationservice",
                        principalTable: "meetings",
                        principalColumn: "MeetingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message_reactions",
                schema: "communicationservice",
                columns: table => new
                {
                    ReactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Emoji = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ReactedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_reactions", x => x.ReactionId);
                    table.ForeignKey(
                        name: "FK_message_reactions_messages_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "communicationservice",
                        principalTable: "messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message_reads",
                schema: "communicationservice",
                columns: table => new
                {
                    ReadId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_reads", x => x.ReadId);
                    table.ForeignKey(
                        name: "FK_message_reads_messages_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "communicationservice",
                        principalTable: "messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_channel_members_ChannelId",
                schema: "communicationservice",
                table: "channel_members",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_channel_members_ChannelId_UserId",
                schema: "communicationservice",
                table: "channel_members",
                columns: new[] { "ChannelId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_channel_members_UserId",
                schema: "communicationservice",
                table: "channel_members",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_channels_ChannelType",
                schema: "communicationservice",
                table: "channels",
                column: "ChannelType");

            migrationBuilder.CreateIndex(
                name: "IX_channels_IsDefault",
                schema: "communicationservice",
                table: "channels",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "IX_channels_IsPrivate",
                schema: "communicationservice",
                table: "channels",
                column: "IsPrivate");

            migrationBuilder.CreateIndex(
                name: "IX_channels_TeamId",
                schema: "communicationservice",
                table: "channels",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_direct_messages_IsRead",
                schema: "communicationservice",
                table: "direct_messages",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_direct_messages_ReceiverId",
                schema: "communicationservice",
                table: "direct_messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_direct_messages_SenderId",
                schema: "communicationservice",
                table: "direct_messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_direct_messages_SenderId_ReceiverId",
                schema: "communicationservice",
                table: "direct_messages",
                columns: new[] { "SenderId", "ReceiverId" });

            migrationBuilder.CreateIndex(
                name: "IX_meeting_notes_CreatedBy",
                schema: "communicationservice",
                table: "meeting_notes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_meeting_notes_MeetingId",
                schema: "communicationservice",
                table: "meeting_notes",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_meeting_participants_MeetingId",
                schema: "communicationservice",
                table: "meeting_participants",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_meeting_participants_MeetingId_UserId",
                schema: "communicationservice",
                table: "meeting_participants",
                columns: new[] { "MeetingId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_meeting_participants_UserId",
                schema: "communicationservice",
                table: "meeting_participants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_meetings_ChannelId",
                schema: "communicationservice",
                table: "meetings",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_meetings_ScheduledStartTime",
                schema: "communicationservice",
                table: "meetings",
                column: "ScheduledStartTime");

            migrationBuilder.CreateIndex(
                name: "IX_meetings_Status",
                schema: "communicationservice",
                table: "meetings",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_meetings_TeamId",
                schema: "communicationservice",
                table: "meetings",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_message_reactions_MessageId",
                schema: "communicationservice",
                table: "message_reactions",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_message_reactions_MessageId_UserId_Emoji",
                schema: "communicationservice",
                table: "message_reactions",
                columns: new[] { "MessageId", "UserId", "Emoji" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_message_reactions_UserId",
                schema: "communicationservice",
                table: "message_reactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_message_reads_MessageId",
                schema: "communicationservice",
                table: "message_reads",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_message_reads_MessageId_UserId",
                schema: "communicationservice",
                table: "message_reads",
                columns: new[] { "MessageId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_message_reads_UserId",
                schema: "communicationservice",
                table: "message_reads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ChannelId",
                schema: "communicationservice",
                table: "messages",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_CreatedAt",
                schema: "communicationservice",
                table: "messages",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_messages_IsPinned",
                schema: "communicationservice",
                table: "messages",
                column: "IsPinned");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ParentMessageId",
                schema: "communicationservice",
                table: "messages",
                column: "ParentMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_SenderId",
                schema: "communicationservice",
                table: "messages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channel_members",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "direct_messages",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "meeting_notes",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "meeting_participants",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "message_reactions",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "message_reads",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "meetings",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "messages",
                schema: "communicationservice");

            migrationBuilder.DropTable(
                name: "channels",
                schema: "communicationservice");
        }
    }
}
