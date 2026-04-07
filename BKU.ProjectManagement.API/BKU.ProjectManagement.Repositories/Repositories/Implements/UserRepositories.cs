using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Entities.SSO;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BKU.ProjectManagement.Repositories.Repositories.Implements
{
    public class AppUserRepository : GenericRepository<AppUser, ProjectManagementDbContext>, IAppUserRepository
    {
        public AppUserRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class AppStudentRepository : GenericRepository<AppStudent, ProjectManagementDbContext>, IAppStudentRepository
    {
        public AppStudentRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class AppLecturerRepository : GenericRepository<AppLecturer, ProjectManagementDbContext>, IAppLecturerRepository
    {
        public AppLecturerRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class LecturerCapacityRepository : GenericRepository<LecturerCapacity, ProjectManagementDbContext>, ILecturerCapacityRepository
    {
        public LecturerCapacityRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    // SSO Repositories
    public class TblUserRepository : GenericRepository<TblUser, ProjectManagementDbContext>, ITblUserRepository
    {
        public TblUserRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class TblStudentRepository : GenericRepository<TblStudent, ProjectManagementDbContext>, ITblStudentRepository
    {
        public TblStudentRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class TblTeacherRepository : GenericRepository<TblTeacher, ProjectManagementDbContext>, ITblTeacherRepository
    {
        public TblTeacherRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }
}
