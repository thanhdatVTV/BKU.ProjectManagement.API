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
            // 1. Tìm thông tin sinh viên từ userId
            var studentResult = await _studentService.GetByUserId(userId);
            if (studentResult.StatusCode != 200 || studentResult.Data == null)
                return StatusCode(404, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Không tìm thấy thông tin sinh viên.", 404));

            var studentId = studentResult.Data.Id;

            // 2. Kiểm tra sinh viên đã đăng ký chuyên ngành chưa
            var myRegistrations = await _registrationService.GetMyRegistration(userId);
            if (myRegistrations.StatusCode != 200 || myRegistrations.Data == null || myRegistrations.Data.Count == 0)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Bạn chưa đăng ký nguyện vọng chuyên ngành. Vui lòng hoàn thành Giai đoạn 1 trước.", 400));

            var latestRegistration = myRegistrations.Data[0];

            // 3. Tìm đợt đồ án Giai đoạn 2 đang diễn ra
            var periodsResult = await _periodService.GetPaging(new ProjectGetPagingRequest { PageIndex = 1, PageSize = 100 });
            var stageTwoPeriod = (periodsResult.StatusCode == 200 && periodsResult.Data != null)
                ? Array.Find(periodsResult.Data.Results.ToArray(), p => p.Stage == 2 && p.Status == 1)
                : null;

            if (stageTwoPeriod == null)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Hiện không có đợt đăng ký GVHD (Giai đoạn 2) nào đang mở.", 400));

            // 4. Kiểm tra sinh viên đã có đăng ký GVHD đang chờ duyệt hoặc đã duyệt trong đợt này
            // (Thực tế là kiểm tra xem latestRegistration có phải của đợt này không và status thế nào)
            // Hoặc đơn giản là tìm registration của sinh viên trong stageTwoPeriod.Id
            var existingRegsResult = await _registrationService.GetPaging(new ProjectGetPagingRequest 
            { 
                PageIndex = 1, 
                PageSize = 10,
                SearchTerm = studentResult.Data.StudentCode
            });
            
            var existingReg = (existingRegsResult.StatusCode == 200 && existingRegsResult.Data != null)
                ? Array.Find(existingRegsResult.Data.Results.ToArray(), r => r.StudentId == studentId && r.ProjectPeriodId == stageTwoPeriod.Id)
                : null;

            if (existingReg != null && existingReg.Status == 1) // Approved
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Đăng ký GVHD của bạn đã được phê duyệt trong đợt này. Không thể đăng ký lại.", 400));

            // 5. Kiểm tra giảng viên có tồn tại không
            var lecturerResult = await _lecturerService.GetById(request.LecturerId);
            if (lecturerResult.StatusCode != 200 || lecturerResult.Data == null)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Giảng viên không tồn tại hoặc không hợp lệ.", 400));

            // 6. Gửi đăng ký (Tạo mới hoặc cập nhật registration cho Stage 2)
            var registrationPayload = new RegistrationCreateRequest
            {
                StudentId = studentId,
                ProjectPeriodId = stageTwoPeriod.Id,
                SelectedMajorId = latestRegistration.SelectedMajorId,
                Choices = new List<RegistrationChoiceRequest>
                {
                    new RegistrationChoiceRequest
                    {
                        LecturerId = request.LecturerId,
                        PriorityOrder = 1
                    }
                }
            };

            var result = await _registrationService.Create(registrationPayload);
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
            // 1. Kiểm tra registration tồn tại
            var registrationResult = await _registrationService.GetById(id);
            if (registrationResult.StatusCode != 200 || registrationResult.Data == null)
                return StatusCode(404, ApiResponse<RegistrationResponse>.ErrorResult("Không tìm thấy thông tin đăng ký.", 404));

            var registration = registrationResult.Data;

            // 2. Kiểm tra trạng thái hiện tại (phải là Pending = 0)
            if (registration.Status != 0)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    $"Không thể phê duyệt đăng ký ở trạng thái hiện tại ({registration.Status}).", 400));

            // 3. Kiểm tra giai đoạn của period
            var periodResult = await _periodService.GetById(registration.ProjectPeriodId);
            if (periodResult.StatusCode != 200 || periodResult.Data == null)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult("Không tìm thấy đợt đồ án liên quan.", 400));

            // Nếu đăng ký đang bị gán nhầm vào Giai đoạn 1 (do lỗi logic cũ)
            if (periodResult.Data.Stage == 1)
            {
                // Tìm đợt Giai đoạn 2 đang mở để "di cư" đăng ký này sang
                var activePeriods = await _periodService.GetPaging(new ProjectGetPagingRequest { PageIndex = 1, PageSize = 100 });
                var activeStageTwo = (activePeriods.StatusCode == 200 && activePeriods.Data != null)
                    ? Array.Find(activePeriods.Data.Results.ToArray(), p => p.Stage == 2 && p.Status == 1)
                    : null;

                if (activeStageTwo != null)
                {
                    // Cập nhật lại ProjectPeriodId cho registration này về Stage 2
                    registration.ProjectPeriodId = activeStageTwo.Id;
                }
                else
                {
                    return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                        "Đăng ký này thuộc Giai đoạn 1 và hiện không có đợt Giai đoạn 2 nào đang mở để phê duyệt.", 400));
                }
            }
            else if (periodResult.Data.Stage != 2)
            {
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Đăng ký này không thuộc Giai đoạn 2 (Đăng ký GVHD).", 400));
            }

            // 4. Kiểm tra sinh viên đã chọn GVHD chưa
            if (registration.Choices == null || registration.Choices.Count == 0)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Sinh viên chưa chọn giảng viên hướng dẫn. Không thể phê duyệt.", 400));

            // 5. Kiểm tra giảng viên được approve có tồn tại không
            var lecturerResult = await _lecturerService.GetById(request.ApprovedLecturerId);
            if (lecturerResult.StatusCode != 200 || lecturerResult.Data == null)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    "Giảng viên được chọn để phê duyệt không tồn tại.", 400));

            var updatePayload = new RegistrationUpdateRequest
            {
                Status = 1, // Approved
                ApprovedLecturerId = request.ApprovedLecturerId,
                RejectReason = null,
                ProjectPeriodId = registration.ProjectPeriodId // Sử dụng ID đã được "di cư" lên Stage 2 nếu cần
            };

            var result = await _registrationService.Update(id, updatePayload);

            // Ghi lịch sử review
            if (result.StatusCode == 200)
            {
                await _historyService.Create(new ReviewHistoryCreateRequest
                {
                    RegistrationId = id,
                    ReviewedByUserId = userId,
                    RejectReason = null
                });
            }

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
            // 1. Kiểm tra registration tồn tại
            var registrationResult = await _registrationService.GetById(id);
            if (registrationResult.StatusCode != 200 || registrationResult.Data == null)
                return StatusCode(404, ApiResponse<RegistrationResponse>.ErrorResult("Không tìm thấy thông tin đăng ký.", 404));

            var registration = registrationResult.Data;

            // 2. Kiểm tra trạng thái hiện tại (phải là Pending = 0)
            if (registration.Status != 0)
                return StatusCode(400, ApiResponse<RegistrationResponse>.ErrorResult(
                    $"Không thể từ chối đăng ký ở trạng thái hiện tại ({registration.Status}).", 400));

            // 3. Kiểm tra giai đoạn và di cư nếu cần
            var pResult = await _periodService.GetById(registration.ProjectPeriodId);
            if (pResult.StatusCode == 200 && pResult.Data != null && pResult.Data.Stage == 1)
            {
                var aPeriods = await _periodService.GetPaging(new ProjectGetPagingRequest { PageIndex = 1, PageSize = 100 });
                var aStageTwo = (aPeriods.StatusCode == 200 && aPeriods.Data != null)
                    ? Array.Find(aPeriods.Data.Results.ToArray(), p => p.Stage == 2 && p.Status == 1)
                    : null;
                if (aStageTwo != null) registration.ProjectPeriodId = aStageTwo.Id;
            }

            var updatePayload = new RegistrationUpdateRequest
            {
                Status = 2, // Rejected
                ApprovedLecturerId = null,
                RejectReason = request.RejectReason ?? "Bị từ chối bởi Phòng Đào tạo.",
                ProjectPeriodId = registration.ProjectPeriodId
            };

            var result = await _registrationService.Update(id, updatePayload);

            // Ghi lịch sử review
            if (result.StatusCode == 200)
            {
                await _historyService.Create(new ReviewHistoryCreateRequest
                {
                    RegistrationId = id,
                    ReviewedByUserId = userId,
                    RejectReason = updatePayload.RejectReason
                });
            }

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
