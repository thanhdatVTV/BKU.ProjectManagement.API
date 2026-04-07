using BKU.ProjectManagement.Shared.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace BKU.ProjectManagement.Services.DTOs.UserDTO
{
    // === AppUser ===
    public class UserResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid SsoUserId { get; set; }
        public string UserName { get; set; } = null!;
        public int UserType { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; }
        public int SyncStatus { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public int UserType { get; set; } // 1: Student, 2: Lecturer
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }

    public class UserCreateRequest
    {
        [Required]
        public Guid SsoUserId { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        public int UserType { get; set; }
    }

    public class UserUpdateRequest
    {
        public int? UserType { get; set; }
        public bool? IsActive { get; set; }
    }

    // === AppStudent ===
    public class StudentResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public string StudentCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid AppUserId { get; set; }
        public int? FacultyId { get; set; }
        public int? MajorId { get; set; }
        public Guid? ClassGroupId { get; set; }
    }

    public class StudentCreateRequest
    {
        [Required]
        public string StudentCode { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public Guid AppUserId { get; set; }
    }

    public class StudentUpdateRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? FacultyId { get; set; }
        public int? MajorId { get; set; }
        public Guid? ClassGroupId { get; set; }
    }

    // === AppLecturer ===
    public class LecturerResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public string TeacherCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid AppUserId { get; set; }
        public int? FacultyId { get; set; }
    }

    public class LecturerCreateRequest
    {
        [Required]
        public string TeacherCode { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public Guid AppUserId { get; set; }
    }

    public class LecturerUpdateRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? FacultyId { get; set; }
    }

    // === LecturerCapacity ===
    public class LecturerCapacityResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public Guid LecturerId { get; set; }
        public Guid SemesterId { get; set; }
        public int MaxProjectCount { get; set; }
        public int CurrentProjectCount { get; set; }
    }

    public class LecturerCapacityCreateRequest
    {
        public Guid LecturerId { get; set; }
        public Guid SemesterId { get; set; }
        public int MaxProjectCount { get; set; }
    }

    public class LecturerCapacityUpdateRequest
    {
        public int? MaxProjectCount { get; set; }
        public int? CurrentProjectCount { get; set; }
    }

    // === Common Paging ===
    public class UserGetPagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}
