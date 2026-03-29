using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Interfaces
{
    public interface ISemesterService
    {
        Task<List<SemesterPublicResponse>> GetAllPublicData(SemesterGetAllRequest request);
        Task<SemesterPublicResponse> GetById(Guid id);
    }
}
