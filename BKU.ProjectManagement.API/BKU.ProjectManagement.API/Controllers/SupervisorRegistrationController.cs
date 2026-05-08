using BKU.ProjectManagement.Services.DTOs.ProjectDTO;
using BKU.ProjectManagement.Services.DTOs.UserDTO;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.API.Controllers
{
    /// <summary>
    /// Controller cho tính năng "Đăng ký Giáo viên Hướng dẫn" (Giai đoạn 2).
    /// Tách biệt với StudentProjectRegistrationController (Giai đoạn 1).
    /// </summary>
    [Route("api/supervisor-registrations")]
    public class SupervisorRegistrationController : BaseController
    {
        private readonly IStudentProjectRegistrationService _registrationService;
        private readonly IAppStudentService _studentService;
        private readonly IAppLecturerService _lecturerService;
        private readonly IRegistrationReviewHistoryService _historyService;
        private readonly IProjectPeriodService _periodService;

        public SupervisorRegistrationController(
            IStudentProjectRegistrationService registrationService,
            IAppStudentService studentService,
            IAppLecturerService lecturerService,
            IRegistrationReviewHistoryService historyService,
            IProjectPeriodService periodService)
        {
            _registrationService = registrationService;
            _studentService = studentService;
            _lecturerService = lecturerService;
            _historyService = historyService;
            _periodService = periodService;
        }

        /// <summary>
        /// Lấy danh sách giảng viên có thể đăng ký làm GVHD.
        /// </summary>
        [HttpGet("lecturers")]
        public async Task<ActionResult<ApiResponse<PagedResult<LecturerResponse>>>> GetLecturers([FromQuery] UserGetPagingRequest request)
        {
            var result = await _lecturerService.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Sinh viên gửi đăng ký GVHD. Chỉ cho phép khi đã đăng ký chuyên ngành.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> Register(
            [FromBody] SupervisorRegistrationCreateRequest request,
            [FromHeader] Guid userId)
        {
            var result = await _registrationService.RegisterSupervisor(request, userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Sinh viên xem đăng ký GVHD của mình.
        /// </summary>
        [HttpGet("my-registration")]
        public async Task<ActionResult<ApiResponse<List<RegistrationResponse>>>> GetMyRegistration([FromHeader] Guid userId)
        {
            var result = await _registrationService.GetMyRegistration(userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Admin/PĐT xem danh sách đăng ký GVHD (có paging/filter).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<RegistrationResponse>>>> GetPaging([FromQuery] ProjectGetPagingRequest request)
        {
            var result = await _registrationService.GetPaging(request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Admin/PĐT phê duyệt đăng ký GVHD.
        /// </summary>
        [HttpPut("{id}/approve")]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> Approve(
            Guid id,
            [FromBody] SupervisorApproveRequest request,
            [FromHeader] Guid userId)
        {
            var result = await _registrationService.ApproveSupervisor(id, request, userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Admin/PĐT từ chối đăng ký GVHD.
        /// </summary>
        [HttpPut("{id}/reject")]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> Reject(
            Guid id,
            [FromBody] SupervisorRejectRequest request,
            [FromHeader] Guid userId)
        {
            var result = await _registrationService.RejectSupervisor(id, request, userId);
            return StatusCode(result.StatusCode, result);
        }
 
        /// <summary>
        /// Sinh viên hủy đăng ký GVHD của mình (Soft delete).
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            // 1. Kiểm tra registration tồn tại
            var registrationResult = await _registrationService.GetById(id);
            if (registrationResult.StatusCode != 200 || registrationResult.Data == null)
                return StatusCode(404, ApiResponse<bool>.ErrorResult("Không tìm thấy thông tin đăng ký.", 404));

            var registration = registrationResult.Data;

            // 2. Chỉ cho phép hủy khi đang ở trạng thái Chờ duyệt (Pending = 0) hoặc Bị từ chối (Rejected = 2)
            if (registration.Status == 1)
                return StatusCode(400, ApiResponse<bool>.ErrorResult(
                    "Đăng ký đã được phê duyệt, không thể tự hủy. Vui lòng liên hệ Phòng Đào tạo.", 400));

            // 3. Thực hiện Soft Delete
            var result = await _registrationService.SoftDelete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
