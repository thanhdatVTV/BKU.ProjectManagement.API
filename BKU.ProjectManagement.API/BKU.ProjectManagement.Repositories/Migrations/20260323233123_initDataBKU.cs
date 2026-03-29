using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BKU.ProjectManagement.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initDataBKU : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppClassGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SsoClassGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassGroupName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClassGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SsoCourseId = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SsoUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SyncStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SsoSyncLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SyncType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SsoEntityId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AppEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    SyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SsoSyncLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppFaculty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SsoFacultyId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    FacultyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFaculty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFaculty_AppCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "AppCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPeriod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AcademicYear = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    RegistrationStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignmentLockAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgressStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgressEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalSubmitStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalSubmitEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPeriod_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppMajor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SsoMajorId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    MajorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMajor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMajor_AppFaculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "AppFaculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppLecturer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SsoTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: true),
                    MajorId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaxGroupsDefault = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLecturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppLecturer_AppCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "AppCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppLecturer_AppFaculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "AppFaculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppLecturer_AppMajor_MajorId",
                        column: x => x.MajorId,
                        principalTable: "AppMajor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppLecturer_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppStudent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SsoStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MajorId = table.Column<int>(type: "int", nullable: false),
                    ClassGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStudent_AppClassGroup_ClassGroupId",
                        column: x => x.ClassGroupId,
                        principalTable: "AppClassGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppStudent_AppCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "AppCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppStudent_AppFaculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "AppFaculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppStudent_AppMajor_MajorId",
                        column: x => x.MajorId,
                        principalTable: "AppMajor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppStudent_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LecturerCapacity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorId = table.Column<int>(type: "int", nullable: true),
                    MaxStudents = table.Column<int>(type: "int", nullable: false),
                    MaxTeams = table.Column<int>(type: "int", nullable: true),
                    CurrentStudents = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CurrentTeams = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerCapacity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LecturerCapacity_AppLecturer_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LecturerCapacity_AppMajor_MajorId",
                        column: x => x.MajorId,
                        principalTable: "AppMajor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LecturerCapacity_ProjectPeriod_ProjectPeriodId",
                        column: x => x.ProjectPeriodId,
                        principalTable: "ProjectPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LeaderStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignedLecturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTeam_AppLecturer_AssignedLecturerId",
                        column: x => x.AssignedLecturerId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTeam_AppStudent_LeaderStudentId",
                        column: x => x.LeaderStudentId,
                        principalTable: "AppStudent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTeam_ProjectPeriod_ProjectPeriodId",
                        column: x => x.ProjectPeriodId,
                        principalTable: "ProjectPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentProjectRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedMajorId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApprovedLecturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProjectRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProjectRegistration_AppLecturer_ApprovedLecturerId",
                        column: x => x.ApprovedLecturerId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProjectRegistration_AppMajor_SelectedMajorId",
                        column: x => x.SelectedMajorId,
                        principalTable: "AppMajor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProjectRegistration_AppStudent_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AppStudent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProjectRegistration_AppUser_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProjectRegistration_ProjectPeriod_ProjectPeriodId",
                        column: x => x.ProjectPeriodId,
                        principalTable: "ProjectPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeamMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActiveMember = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeamMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTeamMember_AppStudent_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AppStudent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTeamMember_ProjectTeam_ProjectTeamId",
                        column: x => x.ProjectTeamId,
                        principalTable: "ProjectTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTopic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objectives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyStack = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ScopeIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScopeOut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTopic_AppLecturer_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTopic_AppUser_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTopic_ProjectTeam_ProjectTeamId",
                        column: x => x.ProjectTeamId,
                        principalTable: "ProjectTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherAssignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AssignedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_AppLecturer_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_AppUser_AssignedBy",
                        column: x => x.AssignedBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_ProjectPeriod_ProjectPeriodId",
                        column: x => x.ProjectPeriodId,
                        principalTable: "ProjectPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_ProjectTeam_ProjectTeamId",
                        column: x => x.ProjectTeamId,
                        principalTable: "ProjectTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingOfficeResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ResultStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    EvaluationNote = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiveStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ExternalRefNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingOfficeResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingOfficeResult_AppLecturer_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingOfficeResult_AppUser_SentBy",
                        column: x => x.SentBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingOfficeResult_ProjectPeriod_ProjectPeriodId",
                        column: x => x.ProjectPeriodId,
                        principalTable: "ProjectPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingOfficeResult_ProjectTeam_ProjectTeamId",
                        column: x => x.ProjectTeamId,
                        principalTable: "ProjectTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationReviewHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: true),
                    ToStatus = table.Column<int>(type: "int", nullable: true),
                    ActionBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationReviewHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationReviewHistory_AppUser_ActionBy",
                        column: x => x.ActionBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrationReviewHistory_StudentProjectRegistration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "StudentProjectRegistration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentProjectRegistrationChoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityOrder = table.Column<int>(type: "int", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProjectRegistrationChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProjectRegistrationChoice_AppLecturer_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProjectRegistrationChoice_StudentProjectRegistration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "StudentProjectRegistration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinalSubmission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VersionNo = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ReportTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Abstract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceCodeUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DemoUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewComment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalSubmission_AppUser_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalSubmission_ProjectTeam_ProjectTeamId",
                        column: x => x.ProjectTeamId,
                        principalTable: "ProjectTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalSubmission_ProjectTopic_ProjectTopicId",
                        column: x => x.ProjectTopicId,
                        principalTable: "ProjectTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgressReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportNo = table.Column<int>(type: "int", nullable: false),
                    WeekNo = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlannedWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentComplete = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedByTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeacherComment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedByStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressReport_AppLecturer_ReviewedByTeacherId",
                        column: x => x.ReviewedByTeacherId,
                        principalTable: "AppLecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgressReport_AppStudent_CreatedByStudentId",
                        column: x => x.CreatedByStudentId,
                        principalTable: "AppStudent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgressReport_ProjectTeam_ProjectTeamId",
                        column: x => x.ProjectTeamId,
                        principalTable: "ProjectTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgressReport_ProjectTopic_ProjectTopicId",
                        column: x => x.ProjectTopicId,
                        principalTable: "ProjectTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinalSubmissionAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileCategory = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalSubmissionAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalSubmissionAttachment_AppUser_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalSubmissionAttachment_FinalSubmission_FinalSubmissionId",
                        column: x => x.FinalSubmissionId,
                        principalTable: "FinalSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgressReportAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressReportAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressReportAttachment_AppUser_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgressReportAttachment_ProgressReport_ProgressReportId",
                        column: x => x.ProgressReportId,
                        principalTable: "ProgressReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppClassGroup_SsoClassGroupId",
                table: "AppClassGroup",
                column: "SsoClassGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCourse_SsoCourseId",
                table: "AppCourse",
                column: "SsoCourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppFaculty_CourseId",
                table: "AppFaculty",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaculty_SsoFacultyId",
                table: "AppFaculty",
                column: "SsoFacultyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLecturer_AppUserId",
                table: "AppLecturer",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLecturer_CourseId",
                table: "AppLecturer",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLecturer_FacultyId",
                table: "AppLecturer",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLecturer_MajorId",
                table: "AppLecturer",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLecturer_SsoTeacherId",
                table: "AppLecturer",
                column: "SsoTeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLecturer_TeacherCode",
                table: "AppLecturer",
                column: "TeacherCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppMajor_FacultyId",
                table: "AppMajor",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMajor_SsoMajorId",
                table: "AppMajor",
                column: "SsoMajorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppStudent_AppUserId",
                table: "AppStudent",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppStudent_ClassGroupId",
                table: "AppStudent",
                column: "ClassGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStudent_CourseId",
                table: "AppStudent",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStudent_FacultyId",
                table: "AppStudent",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStudent_MajorId",
                table: "AppStudent",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStudent_SsoStudentId",
                table: "AppStudent",
                column: "SsoStudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppStudent_StudentCode",
                table: "AppStudent",
                column: "StudentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_SsoUserId",
                table: "AppUser",
                column: "SsoUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_UserName",
                table: "AppUser",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalSubmission_ProjectTeamId_VersionNo",
                table: "FinalSubmission",
                columns: new[] { "ProjectTeamId", "VersionNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalSubmission_ProjectTopicId",
                table: "FinalSubmission",
                column: "ProjectTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalSubmission_ReviewedBy",
                table: "FinalSubmission",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FinalSubmissionAttachment_FinalSubmissionId",
                table: "FinalSubmissionAttachment",
                column: "FinalSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalSubmissionAttachment_UploadedBy",
                table: "FinalSubmissionAttachment",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerCapacity_LecturerId",
                table: "LecturerCapacity",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerCapacity_MajorId",
                table: "LecturerCapacity",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerCapacity_ProjectPeriodId_LecturerId",
                table: "LecturerCapacity",
                columns: new[] { "ProjectPeriodId", "LecturerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReport_CreatedByStudentId",
                table: "ProgressReport",
                column: "CreatedByStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReport_ProjectTeamId_ReportNo",
                table: "ProgressReport",
                columns: new[] { "ProjectTeamId", "ReportNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReport_ProjectTopicId",
                table: "ProgressReport",
                column: "ProjectTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReport_ReviewedByTeacherId",
                table: "ProgressReport",
                column: "ReviewedByTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReportAttachment_ProgressReportId",
                table: "ProgressReportAttachment",
                column: "ProgressReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReportAttachment_UploadedBy",
                table: "ProgressReportAttachment",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPeriod_SemesterId",
                table: "ProjectPeriod",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_AssignedLecturerId",
                table: "ProjectTeam",
                column: "AssignedLecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_LeaderStudentId",
                table: "ProjectTeam",
                column: "LeaderStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_ProjectPeriodId",
                table: "ProjectTeam",
                column: "ProjectPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_TeamCode",
                table: "ProjectTeam",
                column: "TeamCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeamMember_ProjectTeamId_StudentId",
                table: "ProjectTeamMember",
                columns: new[] { "ProjectTeamId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeamMember_StudentId",
                table: "ProjectTeamMember",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTopic_ApprovedBy",
                table: "ProjectTopic",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTopic_ProjectTeamId",
                table: "ProjectTopic",
                column: "ProjectTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTopic_TeacherId",
                table: "ProjectTopic",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationReviewHistory_ActionBy",
                table: "RegistrationReviewHistory",
                column: "ActionBy");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationReviewHistory_RegistrationId",
                table: "RegistrationReviewHistory",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_Code",
                table: "Semester",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistration_ApprovedLecturerId",
                table: "StudentProjectRegistration",
                column: "ApprovedLecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistration_ProjectPeriodId_StudentId",
                table: "StudentProjectRegistration",
                columns: new[] { "ProjectPeriodId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistration_ReviewedByUserId",
                table: "StudentProjectRegistration",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistration_SelectedMajorId",
                table: "StudentProjectRegistration",
                column: "SelectedMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistration_StudentId",
                table: "StudentProjectRegistration",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistrationChoice_LecturerId",
                table: "StudentProjectRegistrationChoice",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistrationChoice_RegistrationId_LecturerId",
                table: "StudentProjectRegistrationChoice",
                columns: new[] { "RegistrationId", "LecturerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectRegistrationChoice_RegistrationId_PriorityOrder",
                table: "StudentProjectRegistrationChoice",
                columns: new[] { "RegistrationId", "PriorityOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_AssignedBy",
                table: "TeacherAssignment",
                column: "AssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_ProjectPeriodId",
                table: "TeacherAssignment",
                column: "ProjectPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_ProjectTeamId",
                table: "TeacherAssignment",
                column: "ProjectTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_TeacherId",
                table: "TeacherAssignment",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingOfficeResult_ProjectPeriodId",
                table: "TrainingOfficeResult",
                column: "ProjectPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingOfficeResult_ProjectTeamId",
                table: "TrainingOfficeResult",
                column: "ProjectTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingOfficeResult_SentBy",
                table: "TrainingOfficeResult",
                column: "SentBy");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingOfficeResult_TeacherId",
                table: "TrainingOfficeResult",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalSubmissionAttachment");

            migrationBuilder.DropTable(
                name: "LecturerCapacity");

            migrationBuilder.DropTable(
                name: "ProgressReportAttachment");

            migrationBuilder.DropTable(
                name: "ProjectTeamMember");

            migrationBuilder.DropTable(
                name: "RegistrationReviewHistory");

            migrationBuilder.DropTable(
                name: "SsoSyncLog");

            migrationBuilder.DropTable(
                name: "StudentProjectRegistrationChoice");

            migrationBuilder.DropTable(
                name: "TeacherAssignment");

            migrationBuilder.DropTable(
                name: "TrainingOfficeResult");

            migrationBuilder.DropTable(
                name: "FinalSubmission");

            migrationBuilder.DropTable(
                name: "ProgressReport");

            migrationBuilder.DropTable(
                name: "StudentProjectRegistration");

            migrationBuilder.DropTable(
                name: "ProjectTopic");

            migrationBuilder.DropTable(
                name: "ProjectTeam");

            migrationBuilder.DropTable(
                name: "AppLecturer");

            migrationBuilder.DropTable(
                name: "AppStudent");

            migrationBuilder.DropTable(
                name: "ProjectPeriod");

            migrationBuilder.DropTable(
                name: "AppClassGroup");

            migrationBuilder.DropTable(
                name: "AppMajor");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "AppFaculty");

            migrationBuilder.DropTable(
                name: "AppCourse");
        }
    }
}
