using BKU.ProjectManagement.Services.DTOs.ProjectDTO;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Interfaces
{
    public interface IProjectPeriodService
    {
        Task<ApiResponse<List<ProjectPeriodResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<ProjectPeriodResponse>>> GetPaging(ProjectGetPagingRequest request);
        Task<ApiResponse<ProjectPeriodResponse>> GetById(Guid id);
        Task<ApiResponse<ProjectPeriodResponse>> Create(ProjectPeriodCreateRequest request);
        Task<ApiResponse<ProjectPeriodResponse>> Update(Guid id, ProjectPeriodUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }

    public interface IProjectTopicService
    {
        Task<ApiResponse<List<ProjectTopicResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<ProjectTopicResponse>>> GetPaging(ProjectGetPagingRequest request);
        Task<ApiResponse<ProjectTopicResponse>> GetById(Guid id);
        Task<ApiResponse<ProjectTopicResponse>> Create(ProjectTopicCreateRequest request);
        Task<ApiResponse<ProjectTopicResponse>> Update(Guid id, ProjectTopicUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }

    public interface IStudentProjectRegistrationService
    {
        Task<ApiResponse<List<RegistrationResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<RegistrationResponse>>> GetPaging(ProjectGetPagingRequest request);
        Task<ApiResponse<RegistrationResponse>> GetById(Guid id);
        Task<ApiResponse<RegistrationResponse>> Create(RegistrationCreateRequest request);
        Task<ApiResponse<RegistrationResponse>> Update(Guid id, RegistrationUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }

    public interface IRegistrationReviewHistoryService
    {
        Task<ApiResponse<List<ReviewHistoryResponse>>> GetByRegistrationId(Guid registrationId);
        Task<ApiResponse<ReviewHistoryResponse>> Create(ReviewHistoryCreateRequest request);
    }
}
