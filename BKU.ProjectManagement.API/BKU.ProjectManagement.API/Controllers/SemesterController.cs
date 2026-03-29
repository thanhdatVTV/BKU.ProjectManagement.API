using AutoWrapper.Wrappers;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests;
using BKU.ProjectManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ApiResponse> GetAllPublicData([FromQuery] SemesterGetAllRequest request)
        {
            return new ApiResponse(await _semesterService.GetAllPublicData(request));
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetById([FromRoute] Guid id)
        {
            var result = await _semesterService.GetById(id);
            if (result == null)
            {
                // Return 404 (Not Found) if the result is null. AutoWrapper deals with it.
                return new ApiResponse("Semester not found.", 404);
            }
            return new ApiResponse(result);
        }

    }
}
