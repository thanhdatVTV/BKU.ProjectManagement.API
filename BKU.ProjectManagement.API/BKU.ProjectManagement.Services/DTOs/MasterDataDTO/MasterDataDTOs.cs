using BKU.ProjectManagement.Shared.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace BKU.ProjectManagement.Services.DTOs.MasterDataDTO
{
    // === AppFaculty ===
    public class FacultyResponse : BaseResponses
    {
        public int Id { get; set; }
        public int SsoFacultyId { get; set; }
        public int? CourseId { get; set; }
        public string FacultyName { get; set; } = null!;
        public DateTime LastSyncedAt { get; set; }
    }

    public class FacultyCreateRequest
    {
        [Required]
        public int SsoFacultyId { get; set; }
        public int? CourseId { get; set; }
        [Required]
        public string FacultyName { get; set; } = null!;
    }

    public class FacultyUpdateRequest
    {
        public int? SsoFacultyId { get; set; }
        public int? CourseId { get; set; }
        public string? FacultyName { get; set; }
    }

    // === AppMajor ===
    public class MajorResponse : BaseResponses
    {
        public int Id { get; set; }
        public int SsoMajorId { get; set; }
        public int? FacultyId { get; set; }
        public string MajorName { get; set; } = null!;
    }

    public class MajorCreateRequest
    {
        [Required]
        public int SsoMajorId { get; set; }
        public int? FacultyId { get; set; }
        [Required]
        public string MajorName { get; set; } = null!;
    }

    public class MajorUpdateRequest
    {
        public int? SsoMajorId { get; set; }
        public int? FacultyId { get; set; }
        public string? MajorName { get; set; }
    }

    // === AppCourse ===
    public class CourseResponse : BaseResponses
    {
        public int Id { get; set; }
        public int SsoCourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public int? TotalCredits { get; set; }
    }

    public class CourseCreateRequest
    {
        [Required]
        public int SsoCourseId { get; set; }
        [Required]
        public string CourseName { get; set; } = null!;
        public int? TotalCredits { get; set; }
    }

    public class CourseUpdateRequest
    {
        public int? SsoCourseId { get; set; }
        public string? CourseName { get; set; }
        public int? TotalCredits { get; set; }
    }

    // === AppClassGroup ===
    public class ClassGroupResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid SsoClassGroupId { get; set; }
        public string ClassGroupName { get; set; } = null!;
        public int? MajorId { get; set; }
    }

    public class ClassGroupCreateRequest
    {
        [Required]
        public Guid SsoClassGroupId { get; set; }
        [Required]
        public string ClassGroupName { get; set; } = null!;
        public int? MajorId { get; set; }
    }

    public class ClassGroupUpdateRequest
    {
        public Guid? SsoClassGroupId { get; set; }
        public string? ClassGroupName { get; set; }
        public int? MajorId { get; set; }
    }

    // === Common Paging Request ===
    public class MasterDataGetPagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}
