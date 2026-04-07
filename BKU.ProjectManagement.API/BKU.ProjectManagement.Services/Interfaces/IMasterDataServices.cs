using BKU.ProjectManagement.Services.DTOs.MasterDataDTO;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Interfaces
{
    public interface IAppFacultyService
    {
        Task<ApiResponse<List<FacultyResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<FacultyResponse>>> GetPaging(MasterDataGetPagingRequest request);
        Task<ApiResponse<FacultyResponse>> GetById(int id);
        Task<ApiResponse<FacultyResponse>> Create(FacultyCreateRequest request);
        Task<ApiResponse<FacultyResponse>> Update(int id, FacultyUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(int id);
        Task<ApiResponse<int>> SyncFromDataSSO();
    }

    public interface IAppMajorService
    {
        Task<ApiResponse<List<MajorResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<MajorResponse>>> GetPaging(MasterDataGetPagingRequest request);
        Task<ApiResponse<MajorResponse>> GetById(int id);
        Task<ApiResponse<MajorResponse>> Create(MajorCreateRequest request);
        Task<ApiResponse<MajorResponse>> Update(int id, MajorUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(int id);
        Task<ApiResponse<int>> SyncFromDataSSO();
    }

    public interface IAppCourseService
    {
        Task<ApiResponse<List<CourseResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<CourseResponse>>> GetPaging(MasterDataGetPagingRequest request);
        Task<ApiResponse<CourseResponse>> GetById(int id);
        Task<ApiResponse<CourseResponse>> Create(CourseCreateRequest request);
        Task<ApiResponse<CourseResponse>> Update(int id, CourseUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(int id);
        Task<ApiResponse<int>> SyncFromDataSSO();
    }

    public interface IAppClassGroupService
    {
        Task<ApiResponse<List<ClassGroupResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<ClassGroupResponse>>> GetPaging(MasterDataGetPagingRequest request);
        Task<ApiResponse<ClassGroupResponse>> GetById(Guid id);
        Task<ApiResponse<ClassGroupResponse>> Create(ClassGroupCreateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
        Task<ApiResponse<int>> SyncFromDataSSO();
    }
}
