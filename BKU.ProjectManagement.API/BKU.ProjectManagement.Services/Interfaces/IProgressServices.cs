using BKU.ProjectManagement.Services.DTOs.ProgressDTO;
using BKU.ProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Interfaces
{
    public interface IProjectTeamService
    {
        Task<ApiResponse<List<ProjectTeamResponse>>> GetAllPublicData();
        Task<ApiResponse<PagedResult<ProjectTeamResponse>>> GetPaging(ProgressGetPagingRequest request);
        Task<ApiResponse<ProjectTeamResponse>> GetById(Guid id);
        Task<ApiResponse<ProjectTeamResponse>> Create(ProjectTeamCreateRequest request);
        Task<ApiResponse<ProjectTeamResponse>> Update(Guid id, ProjectTeamUpdateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);

        // === Team Management cho sinh viên ===
        Task<ApiResponse<ProjectTeamResponse>> GetMyTeam(Guid userId);
        Task<ApiResponse<ProjectTeamResponse>> CreateTeam(TeamCreateByStudentRequest request, Guid userId);
        Task<ApiResponse<bool>> InviteMember(Guid teamId, TeamInviteRequest request, Guid userId);
        Task<ApiResponse<bool>> RespondToInvite(TeamInviteRespondRequest request, Guid userId);
        Task<ApiResponse<bool>> LeaveTeam(Guid teamId, Guid userId);
        Task<ApiResponse<List<TeamMemberResponse>>> GetPendingInvites(Guid userId);
    }

    public interface ITeacherAssignmentService
    {
        Task<ApiResponse<List<TeacherAssignmentResponse>>> GetByTeamId(Guid teamId);
        Task<ApiResponse<TeacherAssignmentResponse>> Create(TeacherAssignmentCreateRequest request);
        Task<ApiResponse<bool>> Delete(Guid id);
    }

    public interface IProgressReportService
    {
        Task<ApiResponse<PagedResult<ProgressReportResponse>>> GetPaging(ProgressGetPagingRequest request);
        Task<ApiResponse<ProgressReportResponse>> GetById(Guid id);
        Task<ApiResponse<ProgressReportResponse>> Create(ProgressReportCreateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }

    public interface IFinalSubmissionService
    {
        Task<ApiResponse<FinalSubmissionResponse>> GetByTeamId(Guid teamId);
        Task<ApiResponse<FinalSubmissionResponse>> Create(FinalSubmissionCreateRequest request);
        Task<ApiResponse<bool>> SoftDelete(Guid id);
    }

    public interface ITrainingOfficeResultService
    {
        Task<ApiResponse<TrainingResultResponse>> GetByTeamId(Guid teamId);
        Task<ApiResponse<TrainingResultResponse>> Create(TrainingResultCreateRequest request);
    }

    public interface ISsoSyncLogService
    {
        Task<ApiResponse<List<SyncLogResponse>>> GetAllLogs();
        Task<ApiResponse<Guid>> CreateLog(string syncType, int status, string message, string ssoId = null, Guid? appId = null);
    }
}
