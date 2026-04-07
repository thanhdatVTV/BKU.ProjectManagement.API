using BKU.ProjectManagement.Repositories.Repositories.Implements;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.Implements;
using BKU.ProjectManagement.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BKU.ProjectManagement.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            // === Group 1: Master Data & SSO ===
            services.AddScoped<IAppFacultyRepository, AppFacultyRepository>();
            services.AddScoped<IAppMajorRepository, AppMajorRepository>();
            services.AddScoped<IAppCourseRepository, AppCourseRepository>();
            services.AddScoped<IAppClassGroupRepository, AppClassGroupRepository>();
            
            services.AddScoped<ITblFacultyRepository, TblFacultyRepository>();
            services.AddScoped<ITblMajorRepository, TblMajorRepository>();
            services.AddScoped<ITblCourseRepository, TblCourseRepository>();
            services.AddScoped<ITblClassGroupRepository, TblClassGroupRepository>();

            services.AddScoped<IAppFacultyService, AppFacultyService>();
            services.AddScoped<IAppMajorService, AppMajorService>();
            services.AddScoped<IAppCourseService, AppCourseService>();
            services.AddScoped<IAppClassGroupService, AppClassGroupService>();

            // === Group 2: Users & Persons ===
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppStudentRepository, AppStudentRepository>();
            services.AddScoped<IAppLecturerRepository, AppLecturerRepository>();
            services.AddScoped<ILecturerCapacityRepository, LecturerCapacityRepository>();
            
            services.AddScoped<ITblUserRepository, TblUserRepository>();
            services.AddScoped<ITblStudentRepository, TblStudentRepository>();
            services.AddScoped<ITblTeacherRepository, TblTeacherRepository>();

            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppStudentService, AppStudentService>();
            services.AddScoped<IAppLecturerService, AppLecturerService>();
            services.AddScoped<ILecturerCapacityService, LecturerCapacityService>();

            // === Group 3: Project & Registration ===
            services.AddScoped<IProjectPeriodRepository, ProjectPeriodRepository>();
            services.AddScoped<IProjectTopicRepository, ProjectTopicRepository>();
            services.AddScoped<IStudentProjectRegistrationRepository, StudentProjectRegistrationRepository>();
            services.AddScoped<IStudentProjectRegistrationChoiceRepository, StudentProjectRegistrationChoiceRepository>();
            services.AddScoped<IRegistrationReviewHistoryRepository, RegistrationReviewHistoryRepository>();

            services.AddScoped<IProjectPeriodService, ProjectPeriodService>();
            services.AddScoped<IProjectTopicService, ProjectTopicService>();
            services.AddScoped<IStudentProjectRegistrationService, StudentProjectRegistrationService>();
            services.AddScoped<IRegistrationReviewHistoryService, RegistrationReviewHistoryService>();

            // === Group 4: Progress & Results ===
            services.AddScoped<IProjectTeamRepository, ProjectTeamRepository>();
            services.AddScoped<IProjectTeamMemberRepository, ProjectTeamMemberRepository>();
            services.AddScoped<ITeacherAssignmentRepository, TeacherAssignmentRepository>();
            services.AddScoped<IProgressReportRepository, ProgressReportRepository>();
            services.AddScoped<IProgressReportAttachmentRepository, ProgressReportAttachmentRepository>();
            services.AddScoped<IFinalSubmissionRepository, FinalSubmissionRepository>();
            services.AddScoped<IFinalSubmissionAttachmentRepository, FinalSubmissionAttachmentRepository>();
            services.AddScoped<ITrainingOfficeResultRepository, TrainingOfficeResultRepository>();
            services.AddScoped<ISsoSyncLogRepository, SsoSyncLogRepository>();

            services.AddScoped<IProjectTeamService, ProjectTeamService>();
            services.AddScoped<ITeacherAssignmentService, TeacherAssignmentService>();
            services.AddScoped<IProgressReportService, ProgressReportService>();
            services.AddScoped<IFinalSubmissionService, FinalSubmissionService>();
            services.AddScoped<ITrainingOfficeResultService, TrainingOfficeResultService>();
            services.AddScoped<ISsoSyncLogService, SsoSyncLogService>();

            return services;
        }
    }
}
