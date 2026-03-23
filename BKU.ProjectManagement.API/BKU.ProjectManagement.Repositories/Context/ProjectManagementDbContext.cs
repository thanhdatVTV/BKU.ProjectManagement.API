using BKU.ProjectManagement.Repositories.Entities.SSO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Context
{
    public class ProjectManagementDbContext : DbContext
    {
        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
        : base(options)
        {
        }

        // SSO schema
        public DbSet<TblUser> TblUsers => Set<TblUser>();
        public DbSet<TblTeacher> TblTeachers => Set<TblTeacher>();
        public DbSet<TblStudent> TblStudents => Set<TblStudent>();
        public DbSet<TblMajor> TblMajors => Set<TblMajor>();
        public DbSet<TblIndustry> TblIndustries => Set<TblIndustry>();
        public DbSet<TblFaculty> TblFaculties => Set<TblFaculty>();
        public DbSet<TblCourse> TblCourses => Set<TblCourse>();
        public DbSet<TblClassGroup> TblClassGroups => Set<TblClassGroup>();

        //public DbSet<AppUser> AppUsers => Set<AppUser>();
        //public DbSet<AppCourse> AppCourses => Set<AppCourse>();
        //public DbSet<AppFaculty> AppFaculties => Set<AppFaculty>();
        //public DbSet<AppMajor> AppMajors => Set<AppMajor>();
        //public DbSet<AppClassGroup> AppClassGroups => Set<AppClassGroup>();
        //public DbSet<AppStudent> AppStudents => Set<AppStudent>();
        //public DbSet<AppLecturer> AppLecturers => Set<AppLecturer>();
        //public DbSet<SsoSyncLog> SsoSyncLogs => Set<SsoSyncLog>();
        //public DbSet<Semester> Semesters => Set<Semester>();
        //public DbSet<ProjectPeriod> ProjectPeriods => Set<ProjectPeriod>();
        //public DbSet<LecturerCapacity> LecturerCapacities => Set<LecturerCapacity>();
        //public DbSet<StudentProjectRegistration> StudentProjectRegistrations => Set<StudentProjectRegistration>();
        //public DbSet<StudentProjectRegistrationChoice> StudentProjectRegistrationChoices => Set<StudentProjectRegistrationChoice>();
        //public DbSet<RegistrationReviewHistory> RegistrationReviewHistories => Set<RegistrationReviewHistory>();
        //public DbSet<ProjectTeam> ProjectTeams => Set<ProjectTeam>();
        //public DbSet<ProjectTeamMember> ProjectTeamMembers => Set<ProjectTeamMember>();
        //public DbSet<TeacherAssignment> TeacherAssignments => Set<TeacherAssignment>();
        //public DbSet<ProjectTopic> ProjectTopics => Set<ProjectTopic>();
        //public DbSet<ProgressReport> ProgressReports => Set<ProgressReport>();
        //public DbSet<ProgressReportAttachment> ProgressReportAttachments => Set<ProgressReportAttachment>();
        //public DbSet<FinalSubmission> FinalSubmissions => Set<FinalSubmission>();
        //public DbSet<FinalSubmissionAttachment> FinalSubmissionAttachments => Set<FinalSubmissionAttachment>();
        //public DbSet<TrainingOfficeResult> TrainingOfficeResults => Set<TrainingOfficeResult>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectManagementDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
