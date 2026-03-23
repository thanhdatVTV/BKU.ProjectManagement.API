using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities.SSO
{
    public class TblIndustry : BaseEntity
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string IndustryName { get; set; } = null!;
    }
}
