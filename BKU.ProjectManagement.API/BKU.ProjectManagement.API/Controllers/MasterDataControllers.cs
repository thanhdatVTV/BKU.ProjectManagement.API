using BKU.ProjectManagement.Services.DTOs.MasterDataDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.API.Controllers
{
    // === AppFacultyController ===
    public class AppFacultyController : BaseController
    {
        private readonly IAppFacultyService _service;
        public AppFacultyController(IAppFacultyService service) { _service = service; }

        [HttpGet("public-data")]
        public async Task<ActionResult<ApiResponse<List<FacultyResponse>>>> GetAll()
        {
            var result = await _service.GetAllPublicData();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<FacultyResponse>>>> GetPaging([FromQuery] MasterDataGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<FacultyResponse>>> GetById(int id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<FacultyResponse>>> Create([FromBody] FacultyCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<FacultyResponse>>> Update(int id, [FromBody] FacultyUpdateRequest request)
        {
            var result = await _service.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var result = await _service.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("sync-sso")]
        public async Task<ActionResult<ApiResponse<int>>> SyncFromSSO()
        {
            var result = await _service.SyncFromDataSSO();
            return StatusCode(result.StatusCode, result);
        }
    }

    // === AppMajorController ===
    [Route("api/majors")]
    public class AppMajorController : BaseController
    {
        private readonly IAppMajorService _service;
        public AppMajorController(IAppMajorService service) { _service = service; }

        [HttpGet]
        [HttpGet("public-data")]
        public async Task<ActionResult<ApiResponse<List<MajorResponse>>>> GetAll()
        {
            var result = await _service.GetAllPublicData();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<MajorResponse>>>> GetPaging([FromQuery] MasterDataGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<MajorResponse>>> GetById(int id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<MajorResponse>>> Create([FromBody] MajorCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<MajorResponse>>> Update(int id, [FromBody] MajorUpdateRequest request)
        {
            var result = await _service.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var result = await _service.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("sync-sso")]
        public async Task<ActionResult<ApiResponse<int>>> SyncFromSSO()
        {
            var result = await _service.SyncFromDataSSO();
            return StatusCode(result.StatusCode, result);
        }
    }

    // === AppCourseController ===
    public class AppCourseController : BaseController
    {
        private readonly IAppCourseService _service;
        public AppCourseController(IAppCourseService service) { _service = service; }

        [HttpGet("public-data")]
        public async Task<ActionResult<ApiResponse<List<CourseResponse>>>> GetAll()
        {
            var result = await _service.GetAllPublicData();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<CourseResponse>>>> GetPaging([FromQuery] MasterDataGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CourseResponse>>> GetById(int id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CourseResponse>>> Create([FromBody] CourseCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CourseResponse>>> Update(int id, [FromBody] CourseUpdateRequest request)
        {
            var result = await _service.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var result = await _service.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("sync-sso")]
        public async Task<ActionResult<ApiResponse<int>>> SyncFromSSO()
        {
            var result = await _service.SyncFromDataSSO();
            return StatusCode(result.StatusCode, result);
        }
    }

    // === AppClassGroupController ===
    public class AppClassGroupController : BaseController
    {
        private readonly IAppClassGroupService _service;
        public AppClassGroupController(IAppClassGroupService service) { _service = service; }

        [HttpGet("public-data")]
        public async Task<ActionResult<ApiResponse<List<ClassGroupResponse>>>> GetAll()
        {
            var result = await _service.GetAllPublicData();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<ClassGroupResponse>>>> GetPaging([FromQuery] MasterDataGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ClassGroupResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ClassGroupResponse>>> Create([FromBody] ClassGroupCreateRequest request)
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

        [HttpPost("sync-sso")]
        public async Task<ActionResult<ApiResponse<int>>> SyncFromSSO()
        {
            var result = await _service.SyncFromDataSSO();
            return StatusCode(result.StatusCode, result);
        }
    }
}
