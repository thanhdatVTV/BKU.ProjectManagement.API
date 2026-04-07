using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Entities.SSO;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BKU.ProjectManagement.Repositories.Repositories.Implements
{
    public class AppFacultyRepository : GenericRepository<AppFaculty, ProjectManagementDbContext>, IAppFacultyRepository
    {
        public AppFacultyRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class AppMajorRepository : GenericRepository<AppMajor, ProjectManagementDbContext>, IAppMajorRepository
    {
        public AppMajorRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class AppCourseRepository : GenericRepository<AppCourse, ProjectManagementDbContext>, IAppCourseRepository
    {
        public AppCourseRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class AppClassGroupRepository : GenericRepository<AppClassGroup, ProjectManagementDbContext>, IAppClassGroupRepository
    {
        public AppClassGroupRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    // SSO Repositories
    public class TblFacultyRepository : GenericRepository<TblFaculty, ProjectManagementDbContext>, ITblFacultyRepository
    {
        public TblFacultyRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class TblMajorRepository : GenericRepository<TblMajor, ProjectManagementDbContext>, ITblMajorRepository
    {
        public TblMajorRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class TblCourseRepository : GenericRepository<TblCourse, ProjectManagementDbContext>, ITblCourseRepository
    {
        public TblCourseRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }

    public class TblClassGroupRepository : GenericRepository<TblClassGroup, ProjectManagementDbContext>, ITblClassGroupRepository
    {
        public TblClassGroupRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor) { }
    }
}
