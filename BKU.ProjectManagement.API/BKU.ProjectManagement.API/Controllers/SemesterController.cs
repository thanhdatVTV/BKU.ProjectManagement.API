using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.API.Controllers
{
    public sealed class SemesterController : BaseController
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpGet("public-data")]
        public async Task<ActionResult<ApiResponse<List<SemesterPublicResponse>>>> GetAllPublicData([FromQuery] SemesterGetAllRequest request)
        {
            var result = await _semesterService.GetAllPublicData(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<SemesterPublicResponse>>>> GetPaging([FromQuery] SemesterGetPagingRequest request)
        {
            var result = await _semesterService.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SemesterPublicResponse>>> GetById([FromRoute] Guid id)
        {
            var result = await _semesterService.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<SemesterPublicResponse>>> Create([FromBody] SemesterCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<SemesterPublicResponse>.ErrorResult("Invalid data"));

            var result = await _semesterService.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<SemesterPublicResponse>>> Update([FromRoute] Guid id, [FromBody] SemesterUpdateRequest request)
        {
            var result = await _semesterService.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDelete([FromRoute] Guid id)
        {
            var result = await _semesterService.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
