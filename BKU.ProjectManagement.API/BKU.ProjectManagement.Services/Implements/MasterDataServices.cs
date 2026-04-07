using AutoMapper;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Entities.SSO;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.DTOs.MasterDataDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Implements
{
    // === AppFacultyService ===
    public class AppFacultyService : IAppFacultyService
    {
        private readonly IAppFacultyRepository _repository;
        private readonly ITblFacultyRepository _ssoRepository;
        private readonly IAppCourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AppFacultyService(IAppFacultyRepository repository, ITblFacultyRepository ssoRepository, IAppCourseRepository courseRepository, IMapper mapper)
        {
            _repository = repository;
            _ssoRepository = ssoRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<FacultyResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<FacultyResponse>>.SuccessResult(_mapper.Map<List<FacultyResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<FacultyResponse>>> GetPaging(MasterDataGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.FacultyName.Contains(request.SearchTerm)));
            
            var result = new PagedResult<FacultyResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<FacultyResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<FacultyResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<FacultyResponse>> GetById(int id)
        {
            var data = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (data == null) return ApiResponse<FacultyResponse>.ErrorResult("Faculty not found", 404);
            return ApiResponse<FacultyResponse>.SuccessResult(_mapper.Map<FacultyResponse>(data));
        }

        public async Task<ApiResponse<FacultyResponse>> Create(FacultyCreateRequest request)
        {
            var existing = (await _repository.GetByCondition(x => x.SsoFacultyId == request.SsoFacultyId && !x.IsDelete)).FirstOrDefault();
            if (existing != null) return ApiResponse<FacultyResponse>.ErrorResult("Faculty already exists");

            var entity = _mapper.Map<AppFaculty>(request);
            entity.LastSyncedAt = DateTime.Now;
            await _repository.Insert(entity);
            return ApiResponse<FacultyResponse>.SuccessResult(_mapper.Map<FacultyResponse>(entity));
        }

        public async Task<ApiResponse<FacultyResponse>> Update(int id, FacultyUpdateRequest request)
        {
            var entity = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (entity == null) return ApiResponse<FacultyResponse>.ErrorResult("Faculty not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<FacultyResponse>.SuccessResult(_mapper.Map<FacultyResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(int id)
        {
            var entity = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (entity == null) return ApiResponse<bool>.ErrorResult("Faculty not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Faculty deleted successfully");
        }

        public async Task<ApiResponse<int>> SyncFromDataSSO()
        {
            var ssoList = await _ssoRepository.GetAll();
            var localList = await _repository.GetAll();
            var courseList = await _courseRepository.GetAll();
            int count = 0;

            foreach (var sso in ssoList)
            {
                var local = localList.FirstOrDefault(x => x.FacultyName == sso.FacultyName);
                var localCourseId = courseList.FirstOrDefault(x => x.SsoCourseId == sso.CourseId)?.Id;

                if (local == null)
                {
                    local = new AppFaculty
                    {
                        FacultyName = sso.FacultyName,
                        SsoFacultyId = sso.Id,
                        CourseId = localCourseId,
                        LastSyncedAt = DateTime.Now
                    };
                    await _repository.Insert(local);
                }
                else
                {
                    local.SsoFacultyId = sso.Id;
                    local.CourseId = localCourseId;
                    local.LastSyncedAt = DateTime.Now;
                    await _repository.Update(local);
                }
                count++;
            }
            return ApiResponse<int>.SuccessResult(count, $"Synced {count} faculties");
        }
    }

    // === AppMajorService ===
    public class AppMajorService : IAppMajorService
    {
        private readonly IAppMajorRepository _repository;
        private readonly ITblMajorRepository _ssoRepository;
        private readonly IAppFacultyRepository _facultyRepository;
        private readonly IMapper _mapper;

        public AppMajorService(IAppMajorRepository repository, ITblMajorRepository ssoRepository, IAppFacultyRepository facultyRepository, IMapper mapper)
        {
            _repository = repository;
            _ssoRepository = ssoRepository;
            _facultyRepository = facultyRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<MajorResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<MajorResponse>>.SuccessResult(_mapper.Map<List<MajorResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<MajorResponse>>> GetPaging(MasterDataGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.MajorName.Contains(request.SearchTerm)));
            
            var result = new PagedResult<MajorResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<MajorResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<MajorResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<MajorResponse>> GetById(int id)
        {
            var data = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (data == null) return ApiResponse<MajorResponse>.ErrorResult("Major not found", 404);
            return ApiResponse<MajorResponse>.SuccessResult(_mapper.Map<MajorResponse>(data));
        }

        public async Task<ApiResponse<MajorResponse>> Create(MajorCreateRequest request)
        {
            var entity = _mapper.Map<AppMajor>(request);
            await _repository.Insert(entity);
            return ApiResponse<MajorResponse>.SuccessResult(_mapper.Map<MajorResponse>(entity));
        }

        public async Task<ApiResponse<MajorResponse>> Update(int id, MajorUpdateRequest request)
        {
            var entity = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (entity == null) return ApiResponse<MajorResponse>.ErrorResult("Major not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<MajorResponse>.SuccessResult(_mapper.Map<MajorResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(int id)
        {
            var entity = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (entity == null) return ApiResponse<bool>.ErrorResult("Major not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Major deleted successfully");
        }

        public async Task<ApiResponse<int>> SyncFromDataSSO()
        {
            var ssoList = await _ssoRepository.GetAll();
            var localList = await _repository.GetAll();
            var facultyList = await _facultyRepository.GetAll();
            int count = 0;

            foreach (var sso in ssoList)
            {
                var local = localList.FirstOrDefault(x => x.MajorName == sso.MajorName);
                var localFacultyId = facultyList.FirstOrDefault(x => x.SsoFacultyId == sso.FacultyId)?.Id;

                if (localFacultyId == null) continue; // Skip if faculty not found locally

                if (local == null)
                {
                    local = new AppMajor
                    {
                        MajorName = sso.MajorName,
                        SsoMajorId = sso.Id,
                        FacultyId = localFacultyId.Value,
                        LastSyncedAt = DateTime.Now
                    };
                    await _repository.Insert(local);
                }
                else
                {
                    local.SsoMajorId = sso.Id;
                    local.FacultyId = localFacultyId.Value;
                    local.LastSyncedAt = DateTime.Now;
                    await _repository.Update(local);
                }
                count++;
            }
            return ApiResponse<int>.SuccessResult(count, $"Synced {count} majors");
        }
    }

    // === AppCourseService ===
    public class AppCourseService : IAppCourseService
    {
        private readonly IAppCourseRepository _repository;
        private readonly ITblCourseRepository _ssoRepository;
        private readonly IMapper _mapper;

        public AppCourseService(IAppCourseRepository repository, ITblCourseRepository ssoRepository, IMapper mapper)
        {
            _repository = repository;
            _ssoRepository = ssoRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CourseResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<CourseResponse>>.SuccessResult(_mapper.Map<List<CourseResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<CourseResponse>>> GetPaging(MasterDataGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.CourseName.Contains(request.SearchTerm)));
            
            var result = new PagedResult<CourseResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<CourseResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<CourseResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<CourseResponse>> GetById(int id)
        {
            var data = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (data == null) return ApiResponse<CourseResponse>.ErrorResult("Course not found", 404);
            return ApiResponse<CourseResponse>.SuccessResult(_mapper.Map<CourseResponse>(data));
        }

        public async Task<ApiResponse<CourseResponse>> Create(CourseCreateRequest request)
        {
            var entity = _mapper.Map<AppCourse>(request);
            await _repository.Insert(entity);
            return ApiResponse<CourseResponse>.SuccessResult(_mapper.Map<CourseResponse>(entity));
        }

        public async Task<ApiResponse<CourseResponse>> Update(int id, CourseUpdateRequest request)
        {
            var entity = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (entity == null) return ApiResponse<CourseResponse>.ErrorResult("Course not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<CourseResponse>.SuccessResult(_mapper.Map<CourseResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(int id)
        {
            var entity = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete)).FirstOrDefault();
            if (entity == null) return ApiResponse<bool>.ErrorResult("Course not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Course deleted successfully");
        }

        public async Task<ApiResponse<int>> SyncFromDataSSO()
        {
            var ssoList = await _ssoRepository.GetAll();
            var localList = await _repository.GetAll();
            int count = 0;

            foreach (var sso in ssoList)
            {
                var local = localList.FirstOrDefault(x => x.CourseName == sso.CourseName);
                if (local == null)
                {
                    local = new AppCourse
                    {
                        SsoCourseId = sso.Id,
                        CourseName = sso.CourseName,
                        LastSyncedAt = DateTime.Now
                    };
                    await _repository.Insert(local);
                }
                else
                {
                    local.LastSyncedAt = DateTime.Now;
                    await _repository.Update(local);
                }
                count++;
            }
            return ApiResponse<int>.SuccessResult(count, $"Synced {count} courses");
        }
    }

    // === AppClassGroupService ===
    public class AppClassGroupService : IAppClassGroupService
    {
        private readonly IAppClassGroupRepository _repository;
        private readonly ITblClassGroupRepository _ssoRepository;
        private readonly IMapper _mapper;

        public AppClassGroupService(IAppClassGroupRepository repository, ITblClassGroupRepository ssoRepository, IMapper mapper)
        {
            _repository = repository;
            _ssoRepository = ssoRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ClassGroupResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<ClassGroupResponse>>.SuccessResult(_mapper.Map<List<ClassGroupResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<ClassGroupResponse>>> GetPaging(MasterDataGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.ClassGroupName.Contains(request.SearchTerm)));
            
            var result = new PagedResult<ClassGroupResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<ClassGroupResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<ClassGroupResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<ClassGroupResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<ClassGroupResponse>.ErrorResult("ClassGroup not found", 404);
            return ApiResponse<ClassGroupResponse>.SuccessResult(_mapper.Map<ClassGroupResponse>(data));
        }

        public async Task<ApiResponse<ClassGroupResponse>> Create(ClassGroupCreateRequest request)
        {
            var entity = _mapper.Map<AppClassGroup>(request);
            entity.LastSyncedAt = DateTime.Now;
            await _repository.Insert(entity);
            return ApiResponse<ClassGroupResponse>.SuccessResult(_mapper.Map<ClassGroupResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("ClassGroup not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "ClassGroup deleted successfully");
        }

        public async Task<ApiResponse<int>> SyncFromDataSSO()
        {
            var ssoList = await _ssoRepository.GetAll();
            var localList = await _repository.GetAll();
            int count = 0;

            foreach (var sso in ssoList)
            {
                var local = localList.FirstOrDefault(x => x.ClassGroupName == sso.ClassGroupName);
                if (local == null)
                {
                    local = new AppClassGroup
                    {
                        SsoClassGroupId = sso.Id,
                        ClassGroupName = sso.ClassGroupName,
                        LastSyncedAt = DateTime.Now
                    };
                    await _repository.Insert(local);
                }
                else
                {
                    local.LastSyncedAt = DateTime.Now;
                    await _repository.Update(local);
                }
                count++;
            }
            return ApiResponse<int>.SuccessResult(count, $"Synced {count} class groups");
        }
    }
}
