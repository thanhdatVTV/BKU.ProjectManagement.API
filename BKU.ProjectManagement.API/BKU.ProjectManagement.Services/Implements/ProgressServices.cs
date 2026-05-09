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
        private readonly IAppStudentRepository _studentRepository;
        private readonly IStudentProjectRegistrationRepository _registrationRepository;
        private readonly IProjectPeriodRepository _periodRepository;
        private readonly IMapper _mapper;

        public ProjectTeamService(
            IProjectTeamRepository repository, 
            IProjectTeamMemberRepository memberRepository,
            IAppStudentRepository studentRepository,
            IStudentProjectRegistrationRepository registrationRepository,
            IProjectPeriodRepository periodRepository,
            IMapper mapper)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _studentRepository = studentRepository;
            _registrationRepository = registrationRepository;
            _periodRepository = periodRepository;
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
                x => !x.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || (x.TeamName != null && x.TeamName.Contains(request.SearchTerm))),
                includeProperties: "LeaderStudent,AssignedLecturer,ProjectPeriod");
            
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
            var team = (await _repository.GetByCondition(x => x.Id == id && !x.IsDelete, 
                includeProperties: "LeaderStudent,AssignedLecturer,ProjectPeriod")).FirstOrDefault();
            
            if (team == null) return ApiResponse<ProjectTeamResponse>.ErrorResult("Không tìm thấy thông tin nhóm.", 404);

            var members = await _memberRepository.GetByCondition(x => x.ProjectTeamId == id && x.Status == 1, 
                includeProperties: "Student");

            var response = _mapper.Map<ProjectTeamResponse>(team);
            
            // Đảm bảo gán đúng tên giảng viên nếu Mapper chưa xử lý được
            if (team.AssignedLecturer != null)
            {
                response.AssignedLecturerName = team.AssignedLecturer.FullName;
            }

            response.Members = _mapper.Map<List<TeamMemberResponse>>(members) ?? new List<TeamMemberResponse>();
            
            return ApiResponse<ProjectTeamResponse>.SuccessResult(response);
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

        // === Team Management cho sinh viên ===

        public async Task<ApiResponse<ProjectTeamResponse>> GetMyTeam(Guid userId)
        {
            try
            {
                var student = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
                if (student == null) return ApiResponse<ProjectTeamResponse>.ErrorResult("Student not found", 404);

                // Tìm thành viên đang hoạt động trong nhóm (Status = 1: Accepted)
                var activeMember = (await _memberRepository.GetByCondition(
                    x => x.StudentId == student.Id && x.Status == 1 && x.IsActiveMember)).FirstOrDefault();

                if (activeMember == null) 
                {
                    // Trả về success với null để FE dễ xử lý trong forkJoin
                    return ApiResponse<ProjectTeamResponse>.SuccessResult(null, "Student is not in any team");
                }

                return await GetById(activeMember.ProjectTeamId);
            }
            catch (Exception ex)
            {
                // Log error if needed
                return ApiResponse<ProjectTeamResponse>.ErrorResult($"Lỗi xử lý lấy thông tin nhóm: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<ProjectTeamResponse>> CreateTeam(TeamCreateByStudentRequest request, Guid userId)
        {
            var student = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
            if (student == null) return ApiResponse<ProjectTeamResponse>.ErrorResult("Student not found", 404);

            // 1. Kiểm tra GVHD approved
            var registration = (await _registrationRepository.GetByCondition(
                x => x.StudentId == student.Id && x.Status == 1 && x.ApprovedLecturerId != null,
                orderBy: o => o.OrderByDescending(x => x.CreatedDate))).FirstOrDefault();

            if (registration == null)
                return ApiResponse<ProjectTeamResponse>.ErrorResult("Bạn cần hoàn thành đăng ký GVHD và được phê duyệt trước khi tạo nhóm.", 400);

            // 2. Kiểm tra đã thuộc nhóm nào chưa
            var existingTeam = (await _memberRepository.GetByCondition(x => x.StudentId == student.Id && x.Status == 1 && x.IsActiveMember)).FirstOrDefault();
            if (existingTeam != null)
                return ApiResponse<ProjectTeamResponse>.ErrorResult("Bạn đã thuộc một nhóm đồ án khác.", 400);

            // 3. Tìm đợt đồ án hiện tại (Stage 3)
            var currentPeriod = (await _periodRepository.GetByCondition(x => x.Id == registration.ProjectPeriodId)).FirstOrDefault();
            if (currentPeriod == null)
                return ApiResponse<ProjectTeamResponse>.ErrorResult("Không tìm thấy đợt đồ án tương ứng.", 400);

            // Tạo nhóm
            var team = new ProjectTeam
            {
                Id = Guid.NewGuid(),
                ProjectPeriodId = currentPeriod.Id,
                TeamCode = $"TEAM-{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}",
                TeamName = string.IsNullOrEmpty(request.TeamName) ? $"Nhóm của {student.FullName}" : request.TeamName,
                LeaderStudentId = student.Id,
                AssignedLecturerId = registration.ApprovedLecturerId,
                Status = 1,
                CreatedDate = DateTime.Now,
                CreatedBy = userId.ToString()
            };

            await _repository.Insert(team);

            // Thêm leader vào member
            var leaderMember = new ProjectTeamMember
            {
                ProjectTeamId = team.Id,
                StudentId = student.Id,
                Role = 1, // Leader
                JoinedAt = DateTime.Now,
                IsActiveMember = true,
                Status = 1, // Accepted
                CreatedDate = DateTime.Now,
                CreatedBy = userId.ToString()
            };

            await _memberRepository.Insert(leaderMember);

            return await GetById(team.Id);
        }

        public async Task<ApiResponse<bool>> InviteMember(Guid teamId, TeamInviteRequest request, Guid userId)
        {
            var leader = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
            if (leader == null) return ApiResponse<bool>.ErrorResult("Leader not found", 404);

            var team = await _repository.GetById(teamId);
            if (team == null || team.IsDelete) return ApiResponse<bool>.ErrorResult("Team not found", 404);

            if (team.LeaderStudentId != leader.Id)
                return ApiResponse<bool>.ErrorResult("Chỉ trưởng nhóm mới có quyền mời thành viên.", 403);

            // Kiểm tra số lượng
            var period = await _periodRepository.GetById(team.ProjectPeriodId);
            var maxMembers = (period != null && period.MaxTeamMembers > 0) ? period.MaxTeamMembers : 2;
            
            var currentMemberCount = (await _memberRepository.GetByCondition(x => x.ProjectTeamId == teamId && x.Status == 1 && x.IsActiveMember)).Count();
            var pendingInviteCount = (await _memberRepository.GetByCondition(x => x.ProjectTeamId == teamId && x.Status == 0 && x.IsActiveMember)).Count();
            
            if (currentMemberCount + pendingInviteCount >= maxMembers)
                return ApiResponse<bool>.ErrorResult($"Nhóm đã đạt số lượng thành viên tối đa cho phép ({maxMembers} người).", 400);

            // Tìm sinh viên được mời
            var invitedStudent = (await _studentRepository.GetByCondition(x => x.StudentCode == request.StudentCode)).FirstOrDefault();
            if (invitedStudent == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy sinh viên với mã số này.", 404);

            if (invitedStudent.Id == leader.Id)
                return ApiResponse<bool>.ErrorResult("Bạn không thể tự mời chính mình.", 400);

            // Kiểm tra cùng GVHD
            var invitedReg = (await _registrationRepository.GetByCondition(
                x => x.StudentId == invitedStudent.Id && x.Status == 1 && x.ProjectPeriodId == team.ProjectPeriodId)).FirstOrDefault();
            
            if (invitedReg == null || invitedReg.ApprovedLecturerId != team.AssignedLecturerId)
                return ApiResponse<bool>.ErrorResult("Sinh viên này chưa đăng ký hoặc không cùng GVHD với nhóm bạn.", 400);

            // Kiểm tra đã thuộc nhóm nào chưa
            var existingMember = (await _memberRepository.GetByCondition(x => x.StudentId == invitedStudent.Id && x.Status == 1 && x.IsActiveMember)).FirstOrDefault();
            if (existingMember != null)
                return ApiResponse<bool>.ErrorResult("Sinh viên này đã thuộc một nhóm khác.", 400);

            // Kiểm tra lời mời đã tồn tại chưa
            var pendingInvite = (await _memberRepository.GetByCondition(x => x.StudentId == invitedStudent.Id && x.ProjectTeamId == teamId && x.Status == 0)).FirstOrDefault();
            if (pendingInvite != null)
                return ApiResponse<bool>.ErrorResult("Lời mời đã được gửi trước đó.", 400);

            // Tạo lời mời
            var invite = new ProjectTeamMember
            {
                ProjectTeamId = teamId,
                StudentId = invitedStudent.Id,
                Role = 2, // Member
                JoinedAt = DateTime.Now,
                IsActiveMember = true,
                Status = 0, // Invited
                InvitedBy = leader.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = userId.ToString()
            };

            await _memberRepository.Insert(invite);
            return ApiResponse<bool>.SuccessResult(true, "Đã gửi lời mời tham gia nhóm.");
        }

        public async Task<ApiResponse<bool>> RespondToInvite(TeamInviteRespondRequest request, Guid userId)
        {
            var student = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
            if (student == null) return ApiResponse<bool>.ErrorResult("Student not found", 404);

            var invite = (await _memberRepository.GetByCondition(x => x.ProjectTeamId == request.TeamId && x.StudentId == student.Id && x.Status == 0)).FirstOrDefault();
            if (invite == null) return ApiResponse<bool>.ErrorResult("Không tìm thấy lời mời.", 404);

            if (request.Accept)
            {
                // Kiểm tra xem team còn chỗ không
                var team = await _repository.GetById(request.TeamId);
                var period = await _periodRepository.GetById(team.ProjectPeriodId);
                var maxMembers = (period != null && period.MaxTeamMembers > 0) ? period.MaxTeamMembers : 2;
                var currentMemberCount = (await _memberRepository.GetByCondition(x => x.ProjectTeamId == request.TeamId && x.Status == 1 && x.IsActiveMember)).Count();

                if (currentMemberCount >= maxMembers)
                    return ApiResponse<bool>.ErrorResult("Nhóm đã đầy, không thể tham gia.", 400);

                // Kiểm tra SV đã vào nhóm khác chưa (đề phòng bấm muộn)
                var existingMember = (await _memberRepository.GetByCondition(x => x.StudentId == student.Id && x.Status == 1 && x.IsActiveMember)).FirstOrDefault();
                if (existingMember != null)
                    return ApiResponse<bool>.ErrorResult("Bạn đã tham gia một nhóm khác.", 400);

                invite.Status = 1; // Accepted
                invite.JoinedAt = DateTime.Now;
            }
            else
            {
                invite.Status = 2; // Declined
                invite.IsActiveMember = false;
            }

            invite.UpdatedDate = DateTime.Now;
            invite.UpdatedBy = userId.ToString();
            await _memberRepository.Update(invite);

            return ApiResponse<bool>.SuccessResult(true, request.Accept ? "Đã tham gia nhóm thành công." : "Đã từ chối lời mời.");
        }

        public async Task<ApiResponse<bool>> LeaveTeam(Guid teamId, Guid userId)
        {
            var student = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
            if (student == null) return ApiResponse<bool>.ErrorResult("Student not found", 404);

            var member = (await _memberRepository.GetByCondition(x => x.ProjectTeamId == teamId && x.StudentId == student.Id && x.Status == 1)).FirstOrDefault();
            if (member == null) return ApiResponse<bool>.ErrorResult("Bạn không thuộc nhóm này.", 404);

            var team = await _repository.GetById(teamId);
            if (team.LeaderStudentId == student.Id)
                return ApiResponse<bool>.ErrorResult("Trưởng nhóm không thể rời nhóm. Vui lòng giải tán nhóm nếu cần.", 400);

            member.Status = 3; // Left
            member.IsActiveMember = false;
            member.UpdatedDate = DateTime.Now;
            member.UpdatedBy = userId.ToString();

            await _memberRepository.Update(member);
            return ApiResponse<bool>.SuccessResult(true, "Đã rời nhóm thành công.");
        }

        public async Task<ApiResponse<List<TeamMemberResponse>>> GetPendingInvites(Guid userId)
        {
            var student = (await _studentRepository.GetByCondition(x => x.AppUserId == userId)).FirstOrDefault();
            if (student == null) return ApiResponse<List<TeamMemberResponse>>.ErrorResult("Student not found", 404);

            var invites = await _memberRepository.GetByCondition(
                x => x.StudentId == student.Id && x.Status == 0,
                includeProperties: "ProjectTeam,ProjectTeam.LeaderStudent,ProjectTeam.AssignedLecturer");

            var result = invites.Select(i => {
                var dto = _mapper.Map<TeamMemberResponse>(i);
                dto.TeamName = i.ProjectTeam.TeamName;
                dto.TeamCode = i.ProjectTeam.TeamCode;
                return dto;
            }).ToList();

            return ApiResponse<List<TeamMemberResponse>>.SuccessResult(result);
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
