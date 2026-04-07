using AutoMapper;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Entities.SSO;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.DTOs.UserDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Implements
{
    // === AppUserService ===
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _repository;
        private readonly ITblUserRepository _ssoRepository;
        private readonly ITblStudentRepository _ssoStudentRepository;
        private readonly ITblTeacherRepository _ssoTeacherRepository;
        private readonly IAppStudentRepository _studentRepository;
        private readonly IAppLecturerRepository _lecturerRepository;
        private readonly IMapper _mapper;

        public AppUserService(
            IAppUserRepository repository, 
            ITblUserRepository ssoRepository, 
            ITblStudentRepository ssoStudentRepository,
            ITblTeacherRepository ssoTeacherRepository,
            IAppStudentRepository studentRepository,
            IAppLecturerRepository lecturerRepository,
            IMapper mapper)
        {
            _repository = repository;
            _ssoRepository = ssoRepository;
            _ssoStudentRepository = ssoStudentRepository;
            _ssoTeacherRepository = ssoTeacherRepository;
            _studentRepository = studentRepository;
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<UserResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<UserResponse>>.SuccessResult(_mapper.Map<List<UserResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<UserResponse>>> GetPaging(UserGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.UserName.Contains(request.SearchTerm)));
            
            var result = new PagedResult<UserResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<UserResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<UserResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<UserResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<UserResponse>.ErrorResult("User not found", 404);
            return ApiResponse<UserResponse>.SuccessResult(_mapper.Map<UserResponse>(data));
        }

        public async Task<ApiResponse<UserResponse>> Create(UserCreateRequest request)
        {
            var existing = (await _repository.GetByCondition(x => x.UserName == request.UserName && !x.IsDelete)).FirstOrDefault();
            if (existing != null) return ApiResponse<UserResponse>.ErrorResult("Username already exists");

            var entity = _mapper.Map<AppUser>(request);
            await _repository.Insert(entity);
            return ApiResponse<UserResponse>.SuccessResult(_mapper.Map<UserResponse>(entity));
        }

        public async Task<ApiResponse<UserResponse>> Update(Guid id, UserUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<UserResponse>.ErrorResult("User not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<UserResponse>.SuccessResult(_mapper.Map<UserResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("User not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "User deleted successfully");
        }

        public async Task<ApiResponse<AuthResponse>> CheckLogin(LoginRequest request)
        {
            // 1. Check in SSO TblUser
            var ssoUser = (await _ssoRepository.GetByCondition(x => x.UserName == request.UserName && x.Password == request.Password)).FirstOrDefault();
            if (ssoUser == null) return ApiResponse<AuthResponse>.ErrorResult("Invalid username or password", 401);

            // 2. Sync to local AppUser if not exists
            var localUser = (await _repository.GetByCondition(x => x.SsoUserId == ssoUser.Id)).FirstOrDefault();
            if (localUser == null)
            {
                localUser = new AppUser
                {
                    SsoUserId = ssoUser.Id,
                    UserName = ssoUser.UserName,
                    UserType = ssoUser.Type,
                    IsActive = true
                };
                await _repository.Insert(localUser);
            }

            localUser.LastLoginAt = DateTime.Now;
            await _repository.Update(localUser);

            var result = new AuthResponse
            {
                Id = localUser.Id,
                UserName = localUser.UserName,
                UserType = localUser.UserType
            };

            return ApiResponse<AuthResponse>.SuccessResult(result, "Login successful");
        }

        public async Task<ApiResponse<string>> SyncUserProfileFromSSO(Guid userId)
        {
            var localUser = await _repository.GetById(userId);
            if (localUser == null) return ApiResponse<string>.ErrorResult("User not found");

            if (localUser.UserType == 1) // Student
            {
                var ssoStudent = (await _ssoStudentRepository.GetByCondition(x => x.UserId == localUser.SsoUserId)).FirstOrDefault();
                if (ssoStudent == null) return ApiResponse<string>.ErrorResult("SSO Student profile not found");

                var localStudent = (await _studentRepository.GetByCondition(x => x.AppUserId == localUser.Id)).FirstOrDefault();
                if (localStudent == null)
                {
                    localStudent = new AppStudent
                    {
                        AppUserId = localUser.Id,
                        SsoStudentId = ssoStudent.Id,
                        StudentCode = ssoStudent.StudentId,
                        FullName = ssoStudent.FullName,
                        Email = localUser.UserName + "@student.bku.edu.vn", // Demo email
                        PhoneNumber = "",
                        ClassGroupId = ssoStudent.ClassGroupId,
                        MajorId = ssoStudent.MajorId,
                        LastSyncedAt = DateTime.Now
                    };
                    await _studentRepository.Insert(localStudent);
                }
                else
                {
                    localStudent.FullName = ssoStudent.FullName;
                    localStudent.ClassGroupId = ssoStudent.ClassGroupId;
                    localStudent.MajorId = ssoStudent.MajorId;
                    localStudent.LastSyncedAt = DateTime.Now;
                    await _studentRepository.Update(localStudent);
                }
                return ApiResponse<string>.SuccessResult("Student profile synced successfully");
            }
            else if (localUser.UserType == 2) // Lecturer
            {
                var ssoTeacher = (await _ssoTeacherRepository.GetByCondition(x => x.UserId == localUser.SsoUserId)).FirstOrDefault();
                if (ssoTeacher == null) return ApiResponse<string>.ErrorResult("SSO Teacher profile not found");

                var localLecturer = (await _lecturerRepository.GetByCondition(x => x.AppUserId == localUser.Id)).FirstOrDefault();
                if (localLecturer == null)
                {
                    localLecturer = new AppLecturer
                    {
                        AppUserId = localUser.Id,
                        SsoTeacherId = ssoTeacher.Id,
                        TeacherCode = ssoTeacher.TeacherId,
                        FullName = ssoTeacher.FullName,
                        Email = localUser.UserName + "@bku.edu.vn", // Demo email
                        PhoneNumber = "",
                        CourseId = ssoTeacher.CourseId,
                        LastSyncedAt = DateTime.Now
                    };
                    await _lecturerRepository.Insert(localLecturer);
                }
                else
                {
                    localLecturer.FullName = ssoTeacher.FullName;
                    localLecturer.CourseId = ssoTeacher.CourseId;
                    localLecturer.LastSyncedAt = DateTime.Now;
                    await _lecturerRepository.Update(localLecturer);
                }
                return ApiResponse<string>.SuccessResult("Lecturer profile synced successfully");
            }

            return ApiResponse<string>.ErrorResult("Invalid user type for syncing profile");
        }
    }

    // === AppStudentService ===
    public class AppStudentService : IAppStudentService
    {
        private readonly IAppStudentRepository _repository;
        private readonly IMapper _mapper;

        public AppStudentService(IAppStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<StudentResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<StudentResponse>>.SuccessResult(_mapper.Map<List<StudentResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<StudentResponse>>> GetPaging(UserGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.FullName.Contains(request.SearchTerm) || x.StudentCode.Contains(request.SearchTerm)));
            
            var result = new PagedResult<StudentResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<StudentResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<StudentResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<StudentResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<StudentResponse>.ErrorResult("Student not found", 404);
            return ApiResponse<StudentResponse>.SuccessResult(_mapper.Map<StudentResponse>(data));
        }

        public async Task<ApiResponse<StudentResponse>> Create(StudentCreateRequest request)
        {
            var entity = _mapper.Map<AppStudent>(request);
            await _repository.Insert(entity);
            return ApiResponse<StudentResponse>.SuccessResult(_mapper.Map<StudentResponse>(entity));
        }

        public async Task<ApiResponse<StudentResponse>> Update(Guid id, StudentUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<StudentResponse>.ErrorResult("Student not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<StudentResponse>.SuccessResult(_mapper.Map<StudentResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Student not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Student deleted successfully");
        }
    }

    // === AppLecturerService ===
    public class AppLecturerService : IAppLecturerService
    {
        private readonly IAppLecturerRepository _repository;
        private readonly IMapper _mapper;

        public AppLecturerService(IAppLecturerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<LecturerResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<LecturerResponse>>.SuccessResult(_mapper.Map<List<LecturerResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<LecturerResponse>>> GetPaging(UserGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.FullName.Contains(request.SearchTerm) || x.TeacherCode.Contains(request.SearchTerm)));
            
            var result = new PagedResult<LecturerResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<LecturerResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<LecturerResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<LecturerResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<LecturerResponse>.ErrorResult("Lecturer not found", 404);
            return ApiResponse<LecturerResponse>.SuccessResult(_mapper.Map<LecturerResponse>(data));
        }

        public async Task<ApiResponse<LecturerResponse>> Create(LecturerCreateRequest request)
        {
            var entity = _mapper.Map<AppLecturer>(request);
            await _repository.Insert(entity);
            return ApiResponse<LecturerResponse>.SuccessResult(_mapper.Map<LecturerResponse>(entity));
        }

        public async Task<ApiResponse<LecturerResponse>> Update(Guid id, LecturerUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<LecturerResponse>.ErrorResult("Lecturer not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<LecturerResponse>.SuccessResult(_mapper.Map<LecturerResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Lecturer not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Lecturer deleted successfully");
        }
    }

    // === LecturerCapacityService ===
    public class LecturerCapacityService : ILecturerCapacityService
    {
        private readonly ILecturerCapacityRepository _repository;
        private readonly IMapper _mapper;

        public LecturerCapacityService(ILecturerCapacityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<LecturerCapacityResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<LecturerCapacityResponse>>.SuccessResult(_mapper.Map<List<LecturerCapacityResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<LecturerCapacityResponse>>> GetPaging(UserGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, x => !x.IsDelete);
            
            var result = new PagedResult<LecturerCapacityResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<LecturerCapacityResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<LecturerCapacityResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<LecturerCapacityResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<LecturerCapacityResponse>.ErrorResult("Capacity not found", 404);
            return ApiResponse<LecturerCapacityResponse>.SuccessResult(_mapper.Map<LecturerCapacityResponse>(data));
        }

        public async Task<ApiResponse<LecturerCapacityResponse>> Create(LecturerCapacityCreateRequest request)
        {
            var entity = _mapper.Map<LecturerCapacity>(request);
            await _repository.Insert(entity);
            return ApiResponse<LecturerCapacityResponse>.SuccessResult(_mapper.Map<LecturerCapacityResponse>(entity));
        }

        public async Task<ApiResponse<LecturerCapacityResponse>> Update(Guid id, LecturerCapacityUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<LecturerCapacityResponse>.ErrorResult("Capacity not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<LecturerCapacityResponse>.SuccessResult(_mapper.Map<LecturerCapacityResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Capacity not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Capacity deleted successfully");
        }
    }
}
