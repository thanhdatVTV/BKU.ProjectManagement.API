using BKU.ProjectManagement.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BKU.ProjectManagement.Services.DTOs.ProgressDTO
{
    // === ProjectTeam ===
    public class ProjectTeamResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public string TeamCode { get; set; } = null!;
        public string? TeamName { get; set; }
        public Guid? LeaderStudentId { get; set; }
        public string? LeaderStudentName { get; set; }
        public Guid? AssignedLecturerId { get; set; }
        public string? AssignedLecturerName { get; set; }
        public int Status { get; set; }
        public int MaxMembers { get; set; }
        public List<TeamMemberResponse> Members { get; set; } = new();
    }

    public class ProjectTeamCreateRequest
    {
        public Guid ProjectPeriodId { get; set; }
        [Required]
        public string TeamName { get; set; } = null!;
        public List<Guid> MemberStudentIds { get; set; } = new List<Guid>();
    }

    public class ProjectTeamUpdateRequest
    {
        public string? TeamName { get; set; }
        public int? Status { get; set; }
    }

    // Request tạo nhóm bởi sinh viên (tự động xác định leader, GVHD)
    public class TeamCreateByStudentRequest
    {
        public string? TeamName { get; set; }
    }

    // Request mời thành viên vào nhóm
    public class TeamInviteRequest
    {
        [Required]
        public string StudentCode { get; set; } = null!;
    }

    // Request phản hồi lời mời
    public class TeamInviteRespondRequest
    {
        [Required]
        public Guid TeamId { get; set; }
        [Required]
        public bool Accept { get; set; }
    }

    // === ProjectTeamMember ===
    public class TeamMemberResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentCode { get; set; }
        public int Role { get; set; } // 1: Leader, 2: Member
        public bool IsActiveMember { get; set; }
        public int Status { get; set; } // 0: Invited, 1: Accepted, 2: Declined, 3: Left
        public DateTime JoinedAt { get; set; }
        public string? TeamName { get; set; }
        public string? TeamCode { get; set; }
    }

    // === TeacherAssignment ===
    public class TeacherAssignmentResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid LecturerId { get; set; }
        public int Role { get; set; } // 1: Advisor, 2: Reviewer, 3: Council Chair
    }

    public class TeacherAssignmentCreateRequest
    {
        public Guid ProjectTeamId { get; set; }
        public Guid LecturerId { get; set; }
        public int Role { get; set; }
    }

    // === ProgressReport ===
    public class ProgressReportResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid ProjectTopicId { get; set; }
        public string Title { get; set; } = null!;
        public string? Summary { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public List<ProgressReportAttachmentResponse> Attachments { get; set; } = new List<ProgressReportAttachmentResponse>();
    }

    public class ProgressReportCreateRequest
    {
        public Guid ProjectTeamId { get; set; }
        public Guid ProjectTopicId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Summary { get; set; }
        public List<AttachmentRequest> Attachments { get; set; } = new List<AttachmentRequest>();
    }

    public class AttachmentRequest
    {
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
    }

    public class ProgressReportAttachmentResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
    }

    // === FinalSubmission ===
    public class FinalSubmissionResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid ProjectTopicId { get; set; }
        public string ReportTitle { get; set; } = null!;
        public DateTime? SubmittedAt { get; set; }
        public List<FinalSubmissionAttachmentResponse> Attachments { get; set; } = new List<FinalSubmissionAttachmentResponse>();
    }

    public class FinalSubmissionCreateRequest
    {
        public Guid ProjectTeamId { get; set; }
        public Guid ProjectTopicId { get; set; }
        [Required]
        public string ReportTitle { get; set; } = null!;
        public List<AttachmentRequest> Attachments { get; set; } = new List<AttachmentRequest>();
    }

    public class FinalSubmissionAttachmentResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
    }

    // === TrainingOfficeResult ===
    public class TrainingResultResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public double FinalScore { get; set; }
        public string? Grade { get; set; }
        public bool IsPassed { get; set; }
    }

    public class TrainingResultCreateRequest
    {
        public Guid ProjectTeamId { get; set; }
        public double FinalScore { get; set; }
        public string? Grade { get; set; }
        public bool IsPassed { get; set; }
    }

    // === SsoSyncLog ===
    public class SyncLogResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public string SyncType { get; set; } = null!;
        public string? SsoEntityId { get; set; }
        public Guid? AppEntityId { get; set; }
        public int Status { get; set; }
        public string? Message { get; set; }
        public DateTime SyncedAt { get; set; }
    }

    // === Common Paging ===
    public class ProgressGetPagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}
