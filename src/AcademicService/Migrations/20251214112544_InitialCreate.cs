using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubjectName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    TotalHours = table.Column<int>(type: "integer", nullable: false),
                    TheoryHours = table.Column<int>(type: "integer", nullable: true),
                    PracticeHours = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    PrerequisiteSubjects = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "syllabi",
                columns: table => new
                {
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    SyllabusCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SyllabusName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Semester = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AcademicYear = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LearningObjectives = table.Column<string>(type: "text", nullable: true),
                    CourseOutline = table.Column<string>(type: "text", nullable: true),
                    AssessmentMethods = table.Column<string>(type: "text", nullable: true),
                    RequiredMaterials = table.Column<string>(type: "text", nullable: true),
                    TeachingMethods = table.Column<string>(type: "text", nullable: true),
                    GradingScheme = table.Column<string>(type: "text", nullable: true),
                    TotalSlots = table.Column<int>(type: "integer", nullable: false, defaultValue: 15),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ApprovedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_syllabi", x => x.SyllabusId);
                    table.ForeignKey(
                        name: "FK_syllabi_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ClassName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Semester = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AcademicYear = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MaxStudents = table.Column<int>(type: "integer", nullable: false, defaultValue: 35),
                    CurrentStudents = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Schedule = table.Column<string>(type: "text", nullable: true),
                    Room = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PLANNED"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_classes_syllabi_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "syllabi",
                        principalColumn: "SyllabusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "syllabus_weeks",
                columns: table => new
                {
                    SyllabusWeekId = table.Column<Guid>(type: "uuid", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekNumber = table.Column<int>(type: "integer", nullable: false),
                    Topic = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LearningObjectives = table.Column<string>(type: "text", nullable: true),
                    Activities = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Materials = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_syllabus_weeks", x => x.SyllabusWeekId);
                    table.ForeignKey(
                        name: "FK_syllabus_weeks_syllabi_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "syllabi",
                        principalColumn: "SyllabusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "class_lecturers",
                columns: table => new
                {
                    ClassLecturerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "PRIMARY"),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    AssignedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_lecturers", x => x.ClassLecturerId);
                    table.ForeignKey(
                        name: "FK_class_lecturers_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "class_schedule_slots",
                columns: table => new
                {
                    ScheduleSlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false),
                    ScheduledDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Room = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Topic = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "SCHEDULED"),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_schedule_slots", x => x.ScheduleSlotId);
                    table.ForeignKey(
                        name: "FK_class_schedule_slots_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "class_students",
                columns: table => new
                {
                    ClassStudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "ENROLLED"),
                    FinalGrade = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    EnrolledBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DroppedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_students", x => x.ClassStudentId);
                    table.ForeignKey(
                        name: "FK_class_students_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_class_lecturers_ClassId",
                table: "class_lecturers",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_class_lecturers_ClassId_LecturerId",
                table: "class_lecturers",
                columns: new[] { "ClassId", "LecturerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_class_lecturers_LecturerId",
                table: "class_lecturers",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_slots_ClassId",
                table: "class_schedule_slots",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_slots_ClassId_SlotNumber",
                table: "class_schedule_slots",
                columns: new[] { "ClassId", "SlotNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_slots_ScheduledDate",
                table: "class_schedule_slots",
                column: "ScheduledDate");

            migrationBuilder.CreateIndex(
                name: "IX_class_students_ClassId",
                table: "class_students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_class_students_ClassId_StudentId",
                table: "class_students",
                columns: new[] { "ClassId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_class_students_Status",
                table: "class_students",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_class_students_StudentId",
                table: "class_students",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_classes_ClassCode",
                table: "classes",
                column: "ClassCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_classes_Semester_AcademicYear",
                table: "classes",
                columns: new[] { "Semester", "AcademicYear" });

            migrationBuilder.CreateIndex(
                name: "IX_classes_Status",
                table: "classes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_classes_SyllabusId",
                table: "classes",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_DepartmentId",
                table: "subjects",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_IsActive",
                table: "subjects",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_SubjectCode",
                table: "subjects",
                column: "SubjectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_syllabi_IsActive",
                table: "syllabi",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_syllabi_Semester_AcademicYear",
                table: "syllabi",
                columns: new[] { "Semester", "AcademicYear" });

            migrationBuilder.CreateIndex(
                name: "IX_syllabi_SubjectId",
                table: "syllabi",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_syllabi_SyllabusCode",
                table: "syllabi",
                column: "SyllabusCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_syllabus_weeks_SyllabusId",
                table: "syllabus_weeks",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_syllabus_weeks_SyllabusId_WeekNumber",
                table: "syllabus_weeks",
                columns: new[] { "SyllabusId", "WeekNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_syllabus_weeks_WeekNumber",
                table: "syllabus_weeks",
                column: "WeekNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "class_lecturers");

            migrationBuilder.DropTable(
                name: "class_schedule_slots");

            migrationBuilder.DropTable(
                name: "class_students");

            migrationBuilder.DropTable(
                name: "syllabus_weeks");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "syllabi");

            migrationBuilder.DropTable(
                name: "subjects");
        }
    }
}
