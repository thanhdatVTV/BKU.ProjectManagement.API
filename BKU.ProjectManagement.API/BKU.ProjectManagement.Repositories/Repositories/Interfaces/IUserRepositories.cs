using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Entities.SSO;
using BKU.ProjectManagement.Shared.Repositories;

namespace BKU.ProjectManagement.Repositories.Repositories.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser, ProjectManagementDbContext> { }
    public interface IAppStudentRepository : IGenericRepository<AppStudent, ProjectManagementDbContext> { }
    public interface IAppLecturerRepository : IGenericRepository<AppLecturer, ProjectManagementDbContext> { }
    public interface ILecturerCapacityRepository : IGenericRepository<LecturerCapacity, ProjectManagementDbContext> { }

    // SSO Repositories
    public interface ITblUserRepository : IGenericRepository<TblUser, ProjectManagementDbContext> { }
    public interface ITblStudentRepository : IGenericRepository<TblStudent, ProjectManagementDbContext> { }
    public interface ITblTeacherRepository : IGenericRepository<TblTeacher, ProjectManagementDbContext> { }
}
