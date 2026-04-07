using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BKU.ProjectManagement.Repositories.Repositories.Implements
{
    public class ProjectPeriodRepository : GenericRepository<ProjectPeriod, ProjectManagementDbContext>, IProjectPeriodRepository
    {
        public ProjectPeriodRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class ProjectTopicRepository : GenericRepository<ProjectTopic, ProjectManagementDbContext>, IProjectTopicRepository
    {
        public ProjectTopicRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class StudentProjectRegistrationRepository : GenericRepository<StudentProjectRegistration, ProjectManagementDbContext>, IStudentProjectRegistrationRepository
    {
        public StudentProjectRegistrationRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class StudentProjectRegistrationChoiceRepository : GenericRepository<StudentProjectRegistrationChoice, ProjectManagementDbContext>, IStudentProjectRegistrationChoiceRepository
    {
        public StudentProjectRegistrationChoiceRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class RegistrationReviewHistoryRepository : GenericRepository<RegistrationReviewHistory, ProjectManagementDbContext>, IRegistrationReviewHistoryRepository
    {
        public RegistrationReviewHistoryRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }
}
