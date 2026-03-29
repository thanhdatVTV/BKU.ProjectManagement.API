using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Repositories.Interfaces
{
    public interface ISemesterRepository: IGenericRepository<Semester, ProjectManagementDbContext>
    {
    }
}
