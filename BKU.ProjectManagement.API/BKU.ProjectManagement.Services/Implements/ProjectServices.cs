using AutoMapper;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.DTOs.ProjectDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Implements
{
    // === ProjectPeriodService ===
    public class ProjectPeriodService : IProjectPeriodService
    {
        private readonly IProjectPeriodRepository _repository;
        private readonly IMapper _mapper;

        public ProjectPeriodService(IProjectPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ProjectPeriodResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete && x.Status == 1);
            return ApiResponse<List<ProjectPeriodResponse>>.SuccessResult(_mapper.Map<List<ProjectPeriodResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<ProjectPeriodResponse>>> GetPaging(ProjectGetPagingRequest request)
        {
            Guid? semesterGuid = null;
            if (!string.IsNullOrEmpty(request.SemesterId) && Guid.TryParse(request.SemesterId, out var guid))
            {
                semesterGuid = guid;
            }

            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete 
                && (string.IsNullOrEmpty(request.SearchTerm) || x.Name.Contains(request.SearchTerm))
                && (!semesterGuid.HasValue || x.SemesterId == semesterGuid.Value));
            
            var result = new PagedResult<ProjectPeriodResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<ProjectPeriodResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<ProjectPeriodResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<ProjectPeriodResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<ProjectPeriodResponse>.ErrorResult("Project period not found", 404);
            return ApiResponse<ProjectPeriodResponse>.SuccessResult(_mapper.Map<ProjectPeriodResponse>(data));
        }

        public async Task<ApiResponse<ProjectPeriodResponse>> Create(ProjectPeriodCreateRequest request)
        {
            var entity = _mapper.Map<ProjectPeriod>(request);
            entity.Status = 1; // Active by default
            await _repository.Insert(entity);
            return ApiResponse<ProjectPeriodResponse>.SuccessResult(_mapper.Map<ProjectPeriodResponse>(entity));
        }

        public async Task<ApiResponse<ProjectPeriodResponse>> Update(Guid id, ProjectPeriodUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<ProjectPeriodResponse>.ErrorResult("Project period not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<ProjectPeriodResponse>.SuccessResult(_mapper.Map<ProjectPeriodResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Project period not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Project period deleted successfully");
        }
    }

    // === ProjectTopicService ===
    public class ProjectTopicService : IProjectTopicService
    {
        private readonly IProjectTopicRepository _repository;
        private readonly IMapper _mapper;

        public ProjectTopicService(IProjectTopicRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ProjectTopicResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete && x.Status == 1);
            return ApiResponse<List<ProjectTopicResponse>>.SuccessResult(_mapper.Map<List<ProjectTopicResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<ProjectTopicResponse>>> GetPaging(ProjectGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.Title.Contains(request.SearchTerm)));
            
            var result = new PagedResult<ProjectTopicResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<ProjectTopicResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<ProjectTopicResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<ProjectTopicResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<ProjectTopicResponse>.ErrorResult("Topic not found", 404);
            return ApiResponse<ProjectTopicResponse>.SuccessResult(_mapper.Map<ProjectTopicResponse>(data));
        }

        public async Task<ApiResponse<ProjectTopicResponse>> Create(ProjectTopicCreateRequest request)
        {
            var entity = _mapper.Map<ProjectTopic>(request);
            entity.Status = 0; // Draft
            entity.SubmittedAt = DateTime.Now;
            await _repository.Insert(entity);
            return ApiResponse<ProjectTopicResponse>.SuccessResult(_mapper.Map<ProjectTopicResponse>(entity));
        }

        public async Task<ApiResponse<ProjectTopicResponse>> Update(Guid id, ProjectTopicUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<ProjectTopicResponse>.ErrorResult("Topic not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<ProjectTopicResponse>.SuccessResult(_mapper.Map<ProjectTopicResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Topic not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Topic deleted successfully");
        }
    }

    // === StudentProjectRegistrationService ===
    public class StudentProjectRegistrationService : IStudentProjectRegistrationService
    {
        private readonly IStudentProjectRegistrationRepository _repository;
        private readonly IStudentProjectRegistrationChoiceRepository _choiceRepository;
        private readonly IAppStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentProjectRegistrationService(
            IStudentProjectRegistrationRepository repository, 
            IStudentProjectRegistrationChoiceRepository choiceRepository, 
            IAppStudentRepository studentRepository,
            IMapper mapper)
        {
            _repository = repository;
            _choiceRepository = choiceRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<RegistrationResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete, includeProperties: "Student,SelectedMajor");
            return ApiResponse<List<RegistrationResponse>>.SuccessResult(_mapper.Map<List<RegistrationResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<RegistrationResponse>>> GetPaging(ProjectGetPagingRequest request)
        {
            Guid? semesterGuid = null;
            if (!string.IsNullOrEmpty(request.SemesterId) && Guid.TryParse(request.SemesterId, out var guid))
            {
                semesterGuid = guid;
            }

            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (!semesterGuid.HasValue || x.ProjectPeriod.SemesterId == semesterGuid.Value),
                includeProperties: "Student,SelectedMajor");
            
            var result = new PagedResult<RegistrationResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<RegistrationResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<RegistrationResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<RegistrationResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<RegistrationResponse>.ErrorResult("Registration not found", 404);
            return ApiResponse<RegistrationResponse>.SuccessResult(_mapper.Map<RegistrationResponse>(data));
        }

        public async Task<ApiResponse<List<RegistrationResponse>>> GetMyRegistration(Guid userId)
        {
            var student = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
            if (student == null) return ApiResponse<List<RegistrationResponse>>.ErrorResult("Student profile not found", 404);

            var registrations = (await _repository.GetByCondition(x => x.StudentId == student.Id && !x.IsDelete, includeProperties: "Student,SelectedMajor"))
                .OrderByDescending(x => x.SubmittedAt)
                .ToList();

            return ApiResponse<List<RegistrationResponse>>.SuccessResult(_mapper.Map<List<RegistrationResponse>>(registrations));
        }

        public async Task<ApiResponse<RegistrationResponse>> Create(RegistrationCreateRequest request)
        {
            // Check if ANY registration exists for this student in this period
            var existing = (await _repository.GetByCondition(x => 
                x.StudentId == request.StudentId && 
                x.ProjectPeriodId == request.ProjectPeriodId)).FirstOrDefault();

            if (existing != null)
            {
                // If switching to a different major, clear old choices as they are no longer valid
                if (existing.SelectedMajorId != request.SelectedMajorId)
                {
                    var oldChoices = await _choiceRepository.GetByCondition(x => x.RegistrationId == existing.Id);
                    foreach (var oc in oldChoices) await _choiceRepository.Delete(oc.Id);
                }

                // Update existing registration
                existing.SelectedMajorId = request.SelectedMajorId;
                existing.SubmittedAt = DateTime.Now;
                existing.Status = 0; // Reset to pending
                existing.IsDelete = false; // Re-activate if it was deleted
                await _repository.Update(existing);

                // Update choices if provided
                if (request.Choices != null && request.Choices.Any())
                {
                    // Remove old choices
                    var oldChoices = await _choiceRepository.GetByCondition(x => x.RegistrationId == existing.Id);
                    foreach (var oc in oldChoices) await _choiceRepository.Delete(oc.Id);

                    // Add new choices
                    var choices = request.Choices.Select(c => new StudentProjectRegistrationChoice
                    {
                        RegistrationId = existing.Id,
                        LecturerId = c.LecturerId,
                        PriorityOrder = c.PriorityOrder
                    }).ToList();
                    await _choiceRepository.Insert(choices);
                }

                return ApiResponse<RegistrationResponse>.SuccessResult(_mapper.Map<RegistrationResponse>(existing), "Registration updated successfully");
            }

            var registration = new StudentProjectRegistration
            {
                StudentId = request.StudentId,
                ProjectPeriodId = request.ProjectPeriodId,
                SelectedMajorId = request.SelectedMajorId,
                SubmittedAt = DateTime.Now,
                Status = 0 // Pending
            };

            await _repository.Insert(registration);

            if (request.Choices != null && request.Choices.Any())
            {
                var choices = request.Choices.Select(c => new StudentProjectRegistrationChoice
                {
                    RegistrationId = registration.Id,
                    LecturerId = c.LecturerId,
                    PriorityOrder = c.PriorityOrder
                }).ToList();
                await _choiceRepository.Insert(choices);
            }

            return ApiResponse<RegistrationResponse>.SuccessResult(_mapper.Map<RegistrationResponse>(registration), "Registration successful");
        }

        public async Task<ApiResponse<RegistrationResponse>> Update(Guid id, RegistrationUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<RegistrationResponse>.ErrorResult("Registration not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<RegistrationResponse>.SuccessResult(_mapper.Map<RegistrationResponse>(entity));
        }

        public async Task<ApiResponse<bool>> CancelRegistration(Guid userId, int majorId)
        {
            var student = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
            if (student == null) return ApiResponse<bool>.ErrorResult("Student profile not found", 404);

            var registration = (await _repository.GetByCondition(x => x.StudentId == student.Id && x.SelectedMajorId == majorId && !x.IsDelete))
                .OrderByDescending(x => x.SubmittedAt)
                .FirstOrDefault();

            if (registration == null) return ApiResponse<bool>.ErrorResult("Registration not found", 404);

            registration.IsDelete = true;
            await _repository.Update(registration);
            return ApiResponse<bool>.SuccessResult(true, "Registration cancelled successfully");
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Registration not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Registration deleted successfully");
        }
    }

    // === RegistrationReviewHistoryService ===
    public class RegistrationReviewHistoryService : IRegistrationReviewHistoryService
    {
        private readonly IRegistrationReviewHistoryRepository _repository;
        private readonly IMapper _mapper;

        public RegistrationReviewHistoryService(IRegistrationReviewHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ReviewHistoryResponse>>> GetByRegistrationId(Guid registrationId)
        {
            var data = await _repository.GetByCondition(x => x.RegistrationId == registrationId, orderBy: o => o.OrderByDescending(x => x.CreatedDate));
            return ApiResponse<List<ReviewHistoryResponse>>.SuccessResult(_mapper.Map<List<ReviewHistoryResponse>>(data));
        }

        public async Task<ApiResponse<ReviewHistoryResponse>> Create(ReviewHistoryCreateRequest request)
        {
            var entity = _mapper.Map<RegistrationReviewHistory>(request);
            await _repository.Insert(entity);
            return ApiResponse<ReviewHistoryResponse>.SuccessResult(_mapper.Map<ReviewHistoryResponse>(entity));
        }
    }
}
