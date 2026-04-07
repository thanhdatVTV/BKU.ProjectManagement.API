using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Interfaces
{
    public interface ISemesterService
    {
        Task<ApiResponse<List<SemesterPublicResponse>>> GetAllPublicData(SemesterGetAllRequest request);
        Task<ApiResponse<PagedResult<SemesterPublicResponse>>> GetPaging(SemesterGetPagingRequest request);
        Task<ApiResponse<SemesterPublicResponse>> GetById(Guid id);
        Task<ApiResponse<SemesterPublicResponse>> Create(SemesterCreateRequest request);
        Task<ApiResponse<SemesterPublicResponse>> Update(Guid id, SemesterUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }
}
