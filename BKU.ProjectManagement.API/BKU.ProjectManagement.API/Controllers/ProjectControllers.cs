using BKU.ProjectManagement.Services.DTOs.ProjectDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.API.Controllers
{
    // === ProjectPeriodController ===
    public class ProjectPeriodController : BaseController
    {
        private readonly IProjectPeriodService _service;
        public ProjectPeriodController(IProjectPeriodService service) { _service = service; }

        [HttpGet("public-data")]
        public async Task<ActionResult<ApiResponse<List<ProjectPeriodResponse>>>> GetAll()
        {
            var result = await _service.GetAllPublicData();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProjectPeriodResponse>>>> GetPaging([FromQuery] ProjectGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectPeriodResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectPeriodResponse>>> Create([FromBody] ProjectPeriodCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectPeriodResponse>>> Update(Guid id, [FromBody] ProjectPeriodUpdateRequest request)
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
    }

    // === ProjectTopicController ===
    public class ProjectTopicController : BaseController
    {
        private readonly IProjectTopicService _service;
        public ProjectTopicController(IProjectTopicService service) { _service = service; }

        [HttpGet("public-data")]
        public async Task<ActionResult<ApiResponse<List<ProjectTopicResponse>>>> GetAll()
        {
            var result = await _service.GetAllPublicData();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProjectTopicResponse>>>> GetPaging([FromQuery] ProjectGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectTopicResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectTopicResponse>>> Create([FromBody] ProjectTopicCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectTopicResponse>>> Update(Guid id, [FromBody] ProjectTopicUpdateRequest request)
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
    }

    // === StudentProjectRegistrationController ===
    public class StudentProjectRegistrationController : BaseController
    {
        private readonly IStudentProjectRegistrationService _service;
        private readonly IRegistrationReviewHistoryService _historyService;
        public StudentProjectRegistrationController(IStudentProjectRegistrationService service, IRegistrationReviewHistoryService historyService)
        {
            _service = service;
            _historyService = historyService;
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<RegistrationResponse>>>> GetPaging([FromQuery] ProjectGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> Register([FromBody] RegistrationCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> UpdateStatus(Guid id, [FromBody] RegistrationUpdateRequest request)
        {
            var result = await _service.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{id}/review")]
        public async Task<ActionResult<ApiResponse<ReviewHistoryResponse>>> AddReview(Guid id, [FromBody] ReviewHistoryCreateRequest request)
        {
            request.RegistrationId = id;
            var result = await _historyService.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}/review-history")]
        public async Task<ActionResult<ApiResponse<List<ReviewHistoryResponse>>>> GetReviewHistory(Guid id)
        {
            var result = await _historyService.GetByRegistrationId(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
