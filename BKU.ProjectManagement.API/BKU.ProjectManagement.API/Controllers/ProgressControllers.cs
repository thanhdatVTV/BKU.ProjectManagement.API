using BKU.ProjectManagement.Services.DTOs.ProgressDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.API.Controllers
{
    // === ProjectTeamController ===
    public class ProjectTeamController : BaseController
    {
        private readonly IProjectTeamService _service;
        private readonly ITeacherAssignmentService _assignmentService;

        public ProjectTeamController(IProjectTeamService service, ITeacherAssignmentService assignmentService)
        {
            _service = service;
            _assignmentService = assignmentService;
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProjectTeamResponse>>>> GetPaging([FromQuery] ProgressGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectTeamResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectTeamResponse>>> Create([FromBody] ProjectTeamCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectTeamResponse>>> UpdateStatus(Guid id, [FromBody] ProjectTeamUpdateRequest request)
        {
            var result = await _service.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            var result = await _service.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}/assignments")]
        public async Task<ActionResult<ApiResponse<List<TeacherAssignmentResponse>>>> GetAssignments(Guid id)
        {
            var result = await _assignmentService.GetByTeamId(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{id}/assignments")]
        public async Task<ActionResult<ApiResponse<TeacherAssignmentResponse>>> AssignTeacher(Guid id, [FromBody] TeacherAssignmentCreateRequest request)
        {
            request.ProjectTeamId = id;
            var result = await _assignmentService.Create(request);
            return StatusCode(result.StatusCode, result);
        }
    }

    // === ProgressReportController ===
    public class ProgressReportController : BaseController
    {
        private readonly IProgressReportService _service;
        public ProgressReportController(IProgressReportService service) { _service = service; }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProgressReportResponse>>>> GetPaging([FromQuery] ProgressGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProgressReportResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProgressReportResponse>>> SubmitReport([FromBody] ProgressReportCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            var result = await _service.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }

    // === FinalSubmissionController ===
    public class FinalSubmissionController : BaseController
    {
        private readonly IFinalSubmissionService _service;
        public FinalSubmissionController(IFinalSubmissionService service) { _service = service; }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<ApiResponse<FinalSubmissionResponse>>> GetByTeamId(Guid teamId)
        {
            var result = await _service.GetByTeamId(teamId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<FinalSubmissionResponse>>> SubmitFinal([FromBody] FinalSubmissionCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            var result = await _service.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }

    // === TrainingOfficeResultController ===
    public class TrainingOfficeResultController : BaseController
    {
        private readonly ITrainingOfficeResultService _service;
        public TrainingOfficeResultController(ITrainingOfficeResultService service) { _service = service; }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<ApiResponse<TrainingResultResponse>>> GetResult(Guid teamId)
        {
            var result = await _service.GetByTeamId(teamId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TrainingResultResponse>>> CreateResult([FromBody] TrainingResultCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }
    }

    // === SyncLogController ===
    public class SyncLogController : BaseController
    {
        private readonly ISsoSyncLogService _service;
        public SyncLogController(ISsoSyncLogService service) { _service = service; }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<SyncLogResponse>>>> GetLogs()
        {
            var result = await _service.GetAllLogs();
            return StatusCode(result.StatusCode, result);
        }
    }
}
