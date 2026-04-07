using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BKU.ProjectManagement.Repositories.Repositories.Implements
{
    public class ProjectTeamRepository : GenericRepository<ProjectTeam, ProjectManagementDbContext>, IProjectTeamRepository
    {
        public ProjectTeamRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class ProjectTeamMemberRepository : GenericRepository<ProjectTeamMember, ProjectManagementDbContext>, IProjectTeamMemberRepository
    {
        public ProjectTeamMemberRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class TeacherAssignmentRepository : GenericRepository<TeacherAssignment, ProjectManagementDbContext>, ITeacherAssignmentRepository
    {
        public TeacherAssignmentRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class ProgressReportRepository : GenericRepository<ProgressReport, ProjectManagementDbContext>, IProgressReportRepository
    {
        public ProgressReportRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class ProgressReportAttachmentRepository : GenericRepository<ProgressReportAttachment, ProjectManagementDbContext>, IProgressReportAttachmentRepository
    {
        public ProgressReportAttachmentRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class FinalSubmissionRepository : GenericRepository<FinalSubmission, ProjectManagementDbContext>, IFinalSubmissionRepository
    {
        public FinalSubmissionRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class FinalSubmissionAttachmentRepository : GenericRepository<FinalSubmissionAttachment, ProjectManagementDbContext>, IFinalSubmissionAttachmentRepository
    {
        public FinalSubmissionAttachmentRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class TrainingOfficeResultRepository : GenericRepository<TrainingOfficeResult, ProjectManagementDbContext>, ITrainingOfficeResultRepository
    {
        public TrainingOfficeResultRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class SsoSyncLogRepository : GenericRepository<SsoSyncLog, ProjectManagementDbContext>, ISsoSyncLogRepository
    {
        public SsoSyncLogRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }
}
