using BKU.ProjectManagement.Services.DTOs.ProjectDTO;
using BKU.ProjectManagement.Services.DTOs.UserDTO;
using BKU.ProjectManagement.Services.DTOs.ProgressDTO;
using AutoMapper;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests;
using BKU.ProjectManagement.Services.DTOs.MasterDataDTO;

namespace BKU.ProjectManagement.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Semester
            CreateMap<Semester, SemesterPublicResponse>();
            CreateMap<SemesterCreateRequest, Semester>();
            CreateMap<SemesterUpdateRequest, Semester>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppFaculty
            CreateMap<AppFaculty, FacultyResponse>();
            CreateMap<FacultyCreateRequest, AppFaculty>();
            CreateMap<FacultyUpdateRequest, AppFaculty>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppMajor
            CreateMap<AppMajor, MajorResponse>();
            CreateMap<MajorCreateRequest, AppMajor>();
            CreateMap<MajorUpdateRequest, AppMajor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppCourse
            CreateMap<AppCourse, CourseResponse>();
            CreateMap<CourseCreateRequest, AppCourse>();
            CreateMap<CourseUpdateRequest, AppCourse>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppClassGroup
            CreateMap<AppClassGroup, ClassGroupResponse>();
            CreateMap<ClassGroupCreateRequest, AppClassGroup>();
            CreateMap<ClassGroupUpdateRequest, AppClassGroup>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppUser
            CreateMap<AppUser, UserResponse>();
            CreateMap<UserCreateRequest, AppUser>();
            CreateMap<UserUpdateRequest, AppUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppStudent
            CreateMap<AppStudent, StudentResponse>();
            CreateMap<StudentCreateRequest, AppStudent>();
            CreateMap<StudentUpdateRequest, AppStudent>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppLecturer
            CreateMap<AppLecturer, LecturerResponse>();
            CreateMap<LecturerCreateRequest, AppLecturer>();
            CreateMap<LecturerUpdateRequest, AppLecturer>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // LecturerCapacity
            CreateMap<LecturerCapacity, LecturerCapacityResponse>();
            CreateMap<LecturerCapacityCreateRequest, LecturerCapacity>();
            CreateMap<LecturerCapacityUpdateRequest, LecturerCapacity>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ProjectPeriod
            CreateMap<ProjectPeriod, ProjectPeriodResponse>();
            CreateMap<ProjectPeriodCreateRequest, ProjectPeriod>();
            CreateMap<ProjectPeriodUpdateRequest, ProjectPeriod>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ProjectTopic
            CreateMap<ProjectTopic, ProjectTopicResponse>();
            CreateMap<ProjectTopicCreateRequest, ProjectTopic>();
            CreateMap<ProjectTopicUpdateRequest, ProjectTopic>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Registration
            CreateMap<StudentProjectRegistration, RegistrationResponse>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
                .ForMember(dest => dest.StudentCode, opt => opt.MapFrom(src => src.Student.StudentCode))
                .ForMember(dest => dest.SelectedMajorName, opt => opt.MapFrom(src => src.SelectedMajor.MajorName));
            CreateMap<RegistrationCreateRequest, StudentProjectRegistration>();
            CreateMap<RegistrationUpdateRequest, StudentProjectRegistration>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Choice
            CreateMap<StudentProjectRegistrationChoice, RegistrationChoiceResponse>();
            CreateMap<RegistrationChoiceRequest, StudentProjectRegistrationChoice>();

            // ReviewHistory
            CreateMap<RegistrationReviewHistory, ReviewHistoryResponse>();
            CreateMap<ReviewHistoryCreateRequest, RegistrationReviewHistory>();

            // ProjectTeam
            CreateMap<ProjectTeam, ProjectTeamResponse>();
            CreateMap<ProjectTeamCreateRequest, ProjectTeam>();
            CreateMap<ProjectTeamUpdateRequest, ProjectTeam>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // TeamMember
            CreateMap<ProjectTeamMember, TeamMemberResponse>();

            // TeacherAssignment
            CreateMap<TeacherAssignment, TeacherAssignmentResponse>();
            CreateMap<TeacherAssignmentCreateRequest, TeacherAssignment>();

            // ProgressReport & Attachments
            CreateMap<ProgressReport, ProgressReportResponse>();
            CreateMap<ProgressReportCreateRequest, ProgressReport>();
            CreateMap<ProgressReportAttachment, ProgressReportAttachmentResponse>();
            CreateMap<AttachmentRequest, ProgressReportAttachment>();

            // FinalSubmission & Attachments
            CreateMap<FinalSubmission, FinalSubmissionResponse>();
            CreateMap<FinalSubmissionCreateRequest, FinalSubmission>();
            CreateMap<FinalSubmissionAttachment, FinalSubmissionAttachmentResponse>();
            CreateMap<AttachmentRequest, FinalSubmissionAttachment>();

            // TrainingOfficeResult
            CreateMap<TrainingOfficeResult, TrainingResultResponse>();
            CreateMap<TrainingResultCreateRequest, TrainingOfficeResult>();

            // SsoSyncLog
            CreateMap<SsoSyncLog, SyncLogResponse>();
        }
    }
}
