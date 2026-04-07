using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Shared.Repositories;

namespace BKU.ProjectManagement.Repositories.Repositories.Interfaces
{
    public interface IProjectPeriodRepository : IGenericRepository<ProjectPeriod, ProjectManagementDbContext> { }
    public interface IProjectTopicRepository : IGenericRepository<ProjectTopic, ProjectManagementDbContext> { }
    public interface IStudentProjectRegistrationRepository : IGenericRepository<StudentProjectRegistration, ProjectManagementDbContext> { }
    public interface IStudentProjectRegistrationChoiceRepository : IGenericRepository<StudentProjectRegistrationChoice, ProjectManagementDbContext> { }
    public interface IRegistrationReviewHistoryRepository : IGenericRepository<RegistrationReviewHistory, ProjectManagementDbContext> { }
}
