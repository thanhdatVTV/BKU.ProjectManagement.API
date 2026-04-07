using System;
using System.ComponentModel.DataAnnotations;

namespace BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests
{
    public class SemesterCreateRequest
    {
        [Required(ErrorMessage = "Semester code is required")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Semester name is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class SemesterUpdateRequest
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SemesterGetPagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}
