using BKU.ProjectManagement.Services.DTOs.UserDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.API.Controllers
{
    // === AppUserController ===
    public class AppUserController : BaseController
    {
        private readonly IAppUserService _service;
        public AppUserController(IAppUserService service) { _service = service; }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
        {
            var result = await _service.CheckLogin(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{id}/sync-profile")]
        public async Task<ActionResult<ApiResponse<string>>> SyncProfile([FromRoute] Guid id)
        {
            var result = await _service.SyncUserProfileFromSSO(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<UserResponse>>>> GetPaging([FromQuery] UserGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            var result = await _service.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }

    // === AppStudentController ===
    public class AppStudentController : BaseController
    {
        private readonly IAppStudentService _service;
        public AppStudentController(IAppStudentService service) { _service = service; }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<StudentResponse>>>> GetPaging([FromQuery] UserGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StudentResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<StudentResponse>>> Update(Guid id, [FromBody] StudentUpdateRequest request)
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

    // === AppLecturerController ===
    public class AppLecturerController : BaseController
    {
        private readonly IAppLecturerService _service;
        public AppLecturerController(IAppLecturerService service) { _service = service; }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<LecturerResponse>>>> GetPaging([FromQuery] UserGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<LecturerResponse>>> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<LecturerResponse>>> Update(Guid id, [FromBody] LecturerUpdateRequest request)
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

    // === LecturerCapacityController ===
    public class LecturerCapacityController : BaseController
    {
        private readonly ILecturerCapacityService _service;
        public LecturerCapacityController(ILecturerCapacityService service) { _service = service; }

        [HttpGet("paging")]
        public async Task<ActionResult<ApiResponse<PagedResult<LecturerCapacityResponse>>>> GetPaging([FromQuery] UserGetPagingRequest request)
        {
            var result = await _service.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LecturerCapacityResponse>>> Create([FromBody] LecturerCapacityCreateRequest request)
        {
            var result = await _service.Create(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<LecturerCapacityResponse>>> Update(Guid id, [FromBody] LecturerCapacityUpdateRequest request)
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
}
