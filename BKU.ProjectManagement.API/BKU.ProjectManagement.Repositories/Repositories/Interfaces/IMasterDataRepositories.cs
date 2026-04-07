using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Entities.SSO;
using BKU.ProjectManagement.Shared.Repositories;

namespace BKU.ProjectManagement.Repositories.Repositories.Interfaces
{
    public interface IAppFacultyRepository : IGenericRepository<AppFaculty, ProjectManagementDbContext> { }
    public interface IAppMajorRepository : IGenericRepository<AppMajor, ProjectManagementDbContext> { }
    public interface IAppCourseRepository : IGenericRepository<AppCourse, ProjectManagementDbContext> { }
    public interface IAppClassGroupRepository : IGenericRepository<AppClassGroup, ProjectManagementDbContext> { }
    
    // Repositories for SSO Tables
    public interface ITblFacultyRepository : IGenericRepository<TblFaculty, ProjectManagementDbContext> { }
    public interface ITblMajorRepository : IGenericRepository<TblMajor, ProjectManagementDbContext> { }
    public interface ITblCourseRepository : IGenericRepository<TblCourse, ProjectManagementDbContext> { }
    public interface ITblClassGroupRepository : IGenericRepository<TblClassGroup, ProjectManagementDbContext> { }
}
