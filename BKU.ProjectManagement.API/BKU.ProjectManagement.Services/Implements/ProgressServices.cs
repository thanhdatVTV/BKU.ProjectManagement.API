using AutoMapper;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.DTOs.ProgressDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Implements
{
    // === ProjectTeamService ===
    public class ProjectTeamService : IProjectTeamService
    {
        private readonly IProjectTeamRepository _repository;
        private readonly IProjectTeamMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public ProjectTeamService(IProjectTeamRepository repository, IProjectTeamMemberRepository memberRepository, IMapper mapper)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ProjectTeamResponse>>> GetAllPublicData()
        {
            var data = await _repository.GetByCondition(x => !x.IsDelete);
            return ApiResponse<List<ProjectTeamResponse>>.SuccessResult(_mapper.Map<List<ProjectTeamResponse>>(data));
        }

        public async Task<ApiResponse<PagedResult<ProjectTeamResponse>>> GetPaging(ProgressGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, 
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || x.TeamName.Contains(request.SearchTerm)));
            
            var result = new PagedResult<ProjectTeamResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<ProjectTeamResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<ProjectTeamResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<ProjectTeamResponse>> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null || data.IsDelete) return ApiResponse<ProjectTeamResponse>.ErrorResult("Team not found", 404);
            return ApiResponse<ProjectTeamResponse>.SuccessResult(_mapper.Map<ProjectTeamResponse>(data));
        }

        public async Task<ApiResponse<ProjectTeamResponse>> Create(ProjectTeamCreateRequest request)
        {
            var team = _mapper.Map<ProjectTeam>(request);
            team.Status = 1; // Active
            await _repository.Insert(team);

            if (request.MemberStudentIds != null && request.MemberStudentIds.Any())
            {
                var members = request.MemberStudentIds.Select((sid, index) => new ProjectTeamMember
                {
                    ProjectTeamId = team.Id,
                    StudentId = sid,
                    Role = (index == 0) ? 1 : 2, // 1 for leader, 2 for member
                    JoinedAt = DateTime.Now,
                    IsActiveMember = true
                }).ToList();
                await _memberRepository.Insert(members);
            }

            return ApiResponse<ProjectTeamResponse>.SuccessResult(_mapper.Map<ProjectTeamResponse>(team));
        }

        public async Task<ApiResponse<ProjectTeamResponse>> Update(Guid id, ProjectTeamUpdateRequest request)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<ProjectTeamResponse>.ErrorResult("Team not found", 404);

            _mapper.Map(request, entity);
            await _repository.Update(entity);
            return ApiResponse<ProjectTeamResponse>.SuccessResult(_mapper.Map<ProjectTeamResponse>(entity));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Team not found", 404);

            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true, "Team deleted successfully");
        }
    }

    // === TeacherAssignmentService ===
    public class TeacherAssignmentService : ITeacherAssignmentService
    {
        private readonly ITeacherAssignmentRepository _repository;
        private readonly IMapper _mapper;

        public TeacherAssignmentService(ITeacherAssignmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TeacherAssignmentResponse>>> GetByTeamId(Guid teamId)
        {
            var data = await _repository.GetByCondition(x => x.ProjectTeamId == teamId && !x.IsDelete);
            return ApiResponse<List<TeacherAssignmentResponse>>.SuccessResult(_mapper.Map<List<TeacherAssignmentResponse>>(data));
        }

        public async Task<ApiResponse<TeacherAssignmentResponse>> Create(TeacherAssignmentCreateRequest request)
        {
            var entity = _mapper.Map<TeacherAssignment>(request);
            await _repository.Insert(entity);
            return ApiResponse<TeacherAssignmentResponse>.SuccessResult(_mapper.Map<TeacherAssignmentResponse>(entity));
        }

        public async Task<ApiResponse<bool>> Delete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return ApiResponse<bool>.ErrorResult("Assignment not found", 404);
            await _repository.Delete(id);
            return ApiResponse<bool>.SuccessResult(true);
        }
    }

    // === ProgressReportService ===
    public class ProgressReportService : IProgressReportService
    {
        private readonly IProgressReportRepository _repository;
        private readonly IProgressReportAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;

        public ProgressReportService(IProgressReportRepository repository, IProgressReportAttachmentRepository attachmentRepository, IMapper mapper)
        {
            _repository = repository;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResult<ProgressReportResponse>>> GetPaging(ProgressGetPagingRequest request)
        {
            var pagedData = await _repository.GetWithPaging(request.PageIndex, request.PageSize, x => !x.IsDelete);
            var result = new PagedResult<ProgressReportResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<ProgressReportResponse>>(pagedData.Results)
            };
            return ApiResponse<PagedResult<ProgressReportResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<ProgressReportResponse>> GetById(Guid id)
        {
            var data = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete, includeProperties: "ProgressReportAttachments")).FirstOrDefault();
            if (data == null) return ApiResponse<ProgressReportResponse>.ErrorResult("Report not found", 404);
            return ApiResponse<ProgressReportResponse>.SuccessResult(_mapper.Map<ProgressReportResponse>(data));
        }

        public async Task<ApiResponse<ProgressReportResponse>> Create(ProgressReportCreateRequest request)
        {
            var report = _mapper.Map<ProgressReport>(request);
            report.SubmittedAt = DateTime.Now;
            await _repository.Insert(report);

            if (request.Attachments != null && request.Attachments.Any())
            {
                var attachments = _mapper.Map<List<ProgressReportAttachment>>(request.Attachments);
                attachments.ForEach(a => a.ProgressReportId = report.Id);
                await _attachmentRepository.Insert(attachments);
            }

            return ApiResponse<ProgressReportResponse>.SuccessResult(_mapper.Map<ProgressReportResponse>(report));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Report not found", 404);
            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true);
        }
    }

    // === FinalSubmissionService ===
    public class FinalSubmissionService : IFinalSubmissionService
    {
        private readonly IFinalSubmissionRepository _repository;
        private readonly IFinalSubmissionAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;

        public FinalSubmissionService(IFinalSubmissionRepository repository, IFinalSubmissionAttachmentRepository attachmentRepository, IMapper mapper)
        {
            _repository = repository;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<FinalSubmissionResponse>> GetByTeamId(Guid teamId)
        {
            var data = (await _repository.GetByCondition(x => x.ProjectTeamId == teamId && !x.IsDelete, includeProperties: "FinalSubmissionAttachments")).FirstOrDefault();
            if (data == null) return ApiResponse<FinalSubmissionResponse>.ErrorResult("Submission not found", 404);
            return ApiResponse<FinalSubmissionResponse>.SuccessResult(_mapper.Map<FinalSubmissionResponse>(data));
        }

        public async Task<ApiResponse<FinalSubmissionResponse>> Create(FinalSubmissionCreateRequest request)
        {
            var submission = _mapper.Map<FinalSubmission>(request);
            submission.SubmittedAt = DateTime.Now;
            await _repository.Insert(submission);

            if (request.Attachments != null && request.Attachments.Any())
            {
                var attachments = _mapper.Map<List<FinalSubmissionAttachment>>(request.Attachments);
                attachments.ForEach(a => a.FinalSubmissionId = submission.Id);
                await _attachmentRepository.Insert(attachments);
            }

            return ApiResponse<FinalSubmissionResponse>.SuccessResult(_mapper.Map<FinalSubmissionResponse>(submission));
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null || entity.IsDelete) return ApiResponse<bool>.ErrorResult("Submission not found", 404);
            entity.IsDelete = true;
            await _repository.Update(entity);
            return ApiResponse<bool>.SuccessResult(true);
        }
    }

    // === TrainingOfficeResultService ===
    public class TrainingOfficeResultService : ITrainingOfficeResultService
    {
        private readonly ITrainingOfficeResultRepository _repository;
        private readonly IMapper _mapper;

        public TrainingOfficeResultService(ITrainingOfficeResultRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<TrainingResultResponse>> GetByTeamId(Guid teamId)
        {
            var data = (await _repository.GetByCondition(x => x.ProjectTeamId == teamId && !x.IsDelete)).FirstOrDefault();
            if (data == null) return ApiResponse<TrainingResultResponse>.ErrorResult("Result not found", 404);
            return ApiResponse<TrainingResultResponse>.SuccessResult(_mapper.Map<TrainingResultResponse>(data));
        }

        public async Task<ApiResponse<TrainingResultResponse>> Create(TrainingResultCreateRequest request)
        {
            var entity = _mapper.Map<TrainingOfficeResult>(request);
            await _repository.Insert(entity);
            return ApiResponse<TrainingResultResponse>.SuccessResult(_mapper.Map<TrainingResultResponse>(entity));
        }
    }

    // === ISsoSyncLogService ===
    public class SsoSyncLogService : ISsoSyncLogService
    {
        private readonly ISsoSyncLogRepository _repository;
        private readonly IMapper _mapper;

        public SsoSyncLogService(ISsoSyncLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<SyncLogResponse>>> GetAllLogs()
        {
            var data = await _repository.GetByCondition(orderBy: o => o.OrderByDescending(x => x.CreatedDate));
            return ApiResponse<List<SyncLogResponse>>.SuccessResult(_mapper.Map<List<SyncLogResponse>>(data));
        }

        public async Task<ApiResponse<Guid>> CreateLog(string syncType, int status, string message, string ssoId = null, Guid? appId = null)
        {
            var log = new SsoSyncLog
            {
                SyncType = syncType,
                Status = status,
                Message = message,
                SsoEntityId = ssoId,
                AppEntityId = appId,
                SyncedAt = DateTime.Now
            };
            await _repository.Insert(log);
            return ApiResponse<Guid>.SuccessResult(log.Id);
        }
    }
}
