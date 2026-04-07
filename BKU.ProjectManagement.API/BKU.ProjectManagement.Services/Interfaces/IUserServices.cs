using BKU.ProjectManagement.Services.DTOs.UserDTO;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Interfaces
{
    public interface IAppUserService
    {
        Task<ApiResponse<List<UserResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<UserResponse>>> GetPaging(UserGetPagingRequest request);
        Task<ApiResponse<UserResponse>> GetById(Guid id);
        Task<ApiResponse<UserResponse>> Create(UserCreateRequest request);
        Task<ApiResponse<UserResponse>> Update(Guid id, UserUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
        
        // Special APIs
        Task<ApiResponse<AuthResponse>> CheckLogin(LoginRequest request);
        Task<ApiResponse<string>> SyncUserProfileFromSSO(Guid userId);
    }

    public interface IAppStudentService
    {
        Task<ApiResponse<List<StudentResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<StudentResponse>>> GetPaging(UserGetPagingRequest request);
        Task<ApiResponse<StudentResponse>> GetById(Guid id);
        Task<ApiResponse<StudentResponse>> Create(StudentCreateRequest request);
        Task<ApiResponse<StudentResponse>> Update(Guid id, StudentUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }

    public interface IAppLecturerService
    {
        Task<ApiResponse<List<LecturerResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<LecturerResponse>>> GetPaging(UserGetPagingRequest request);
        Task<ApiResponse<LecturerResponse>> GetById(Guid id);
        Task<ApiResponse<LecturerResponse>> Create(LecturerCreateRequest request);
        Task<ApiResponse<LecturerResponse>> Update(Guid id, LecturerUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }

    public interface ILecturerCapacityService
    {
        Task<ApiResponse<List<LecturerCapacityResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<LecturerCapacityResponse>>> GetPaging(UserGetPagingRequest request);
        Task<ApiResponse<LecturerCapacityResponse>> GetById(Guid id);
        Task<ApiResponse<LecturerCapacityResponse>> Create(LecturerCapacityCreateRequest request);
        Task<ApiResponse<LecturerCapacityResponse>> Update(Guid id, LecturerCapacityUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }
}
