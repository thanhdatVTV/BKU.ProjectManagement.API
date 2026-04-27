using BKU.ProjectManagement.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BKU.ProjectManagement.Services.DTOs.ProjectDTO
{
    // === ProjectPeriod ===
    public class ProjectPeriodResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string AcademicYear { get; set; } = null!;
        public int Stage { get; set; }
        public int Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public Guid SemesterId { get; set; }
    }

    public class ProjectPeriodCreateRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public string AcademicYear { get; set; } = null!;
        public int Stage { get; set; }
        public int Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SemesterId { get; set; }
    }

    public class ProjectPeriodUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AcademicYear { get; set; }
        public int? Stage { get; set; }
        public int? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
    }

    // === ProjectTopic ===
    public class ProjectTopicResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid TeacherId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Status { get; set; }
        public Guid? ApprovedBy { get; set; }
    }

    public class ProjectTopicCreateRequest
    {
        [Required]
        public Guid ProjectTeamId { get; set; }
        [Required]
        public Guid TeacherId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class ProjectTopicUpdateRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
    }

    // === StudentProjectRegistration ===
    public class RegistrationResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentCode { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public int SelectedMajorId { get; set; }
        public string? SelectedMajorName { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int Status { get; set; }
    }

    public class RegistrationCreateRequest
    {
        public Guid StudentId { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public int SelectedMajorId { get; set; }
        public List<RegistrationChoiceRequest> Choices { get; set; } = new List<RegistrationChoiceRequest>();
    }

    public class RegistrationUpdateRequest
    {
        public int? Status { get; set; }
        public Guid? ApprovedLecturerId { get; set; }
        public string? RejectReason { get; set; }
    }

    // === StudentProjectRegistrationChoice ===
    public class RegistrationChoiceResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid RegistrationId { get; set; }
        public Guid LecturerId { get; set; }
        public int PriorityOrder { get; set; }
        public int Status { get; set; }
    }

    public class RegistrationChoiceRequest
    {
        public Guid LecturerId { get; set; }
        public int PriorityOrder { get; set; }
    }

    // === RegistrationReviewHistory ===
    public class ReviewHistoryResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid RegistrationId { get; set; }
        public Guid? ReviewedByUserId { get; set; }
        public string? RejectReason { get; set; }
    }

    public class ReviewHistoryCreateRequest
    {
        public Guid RegistrationId { get; set; }
        public Guid? ReviewedByUserId { get; set; }
        public string? RejectReason { get; set; }
    }

    // === Common Paging ===
    public class ProjectGetPagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? SemesterId { get; set; }
    }
}
