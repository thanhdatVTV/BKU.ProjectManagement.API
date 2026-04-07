using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Shared.Repositories;

namespace BKU.ProjectManagement.Repositories.Repositories.Interfaces
{
    public interface IProjectTeamRepository : IGenericRepository<ProjectTeam, ProjectManagementDbContext> { }
    public interface IProjectTeamMemberRepository : IGenericRepository<ProjectTeamMember, ProjectManagementDbContext> { }
    public interface ITeacherAssignmentRepository : IGenericRepository<TeacherAssignment, ProjectManagementDbContext> { }
    public interface IProgressReportRepository : IGenericRepository<ProgressReport, ProjectManagementDbContext> { }
    public interface IProgressReportAttachmentRepository : IGenericRepository<ProgressReportAttachment, ProjectManagementDbContext> { }
    public interface IFinalSubmissionRepository : IGenericRepository<FinalSubmission, ProjectManagementDbContext> { }
    public interface IFinalSubmissionAttachmentRepository : IGenericRepository<FinalSubmissionAttachment, ProjectManagementDbContext> { }
    public interface ITrainingOfficeResultRepository : IGenericRepository<TrainingOfficeResult, ProjectManagementDbContext> { }
    public interface ISsoSyncLogRepository : IGenericRepository<SsoSyncLog, ProjectManagementDbContext> { }
}
