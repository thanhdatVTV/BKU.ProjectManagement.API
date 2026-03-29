using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Repositories.Implements
{
    public class SemesterRepository : GenericRepository<Semester, ProjectManagementDbContext>, ISemesterRepository
    {
        public SemesterRepository(ProjectManagementDbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, config, httpContextAccessor)
        {
        }
    }
}
