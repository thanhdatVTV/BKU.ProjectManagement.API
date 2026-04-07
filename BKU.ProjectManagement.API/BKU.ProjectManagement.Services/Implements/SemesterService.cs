using AutoMapper;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses;
using BKU.ProjectManagement.Services.Interfaces;
using BKU.ProjectManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Implements
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;

        public SemesterService(ISemesterRepository semesterRepository, IMapper mapper)
        {
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<SemesterPublicResponse>>> GetAllPublicData(SemesterGetAllRequest request)
        {
            var semesters = await _semesterRepository.GetByCondition(s => !s.IsDelete);
            var result = _mapper.Map<List<SemesterPublicResponse>>(semesters);
            return ApiResponse<List<SemesterPublicResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<PagedResult<SemesterPublicResponse>>> GetPaging(SemesterGetPagingRequest request)
        {
            var pagedData = await _semesterRepository.GetWithPaging(
                request.PageIndex, 
                request.PageSize, 
                s => !s.IsDelete && (string.IsNullOrEmpty(request.SearchTerm) || s.Name.Contains(request.SearchTerm) || s.Code.Contains(request.SearchTerm)),
                o => o.OrderByDescending(s => s.CreatedDate)
            );
            
            var result = new PagedResult<SemesterPublicResponse>
            {
                CurrentPage = pagedData.CurrentPage,
                PageCount = pagedData.PageCount,
                PageSize = pagedData.PageSize,
                RowCount = pagedData.RowCount,
                Results = _mapper.Map<List<SemesterPublicResponse>>(pagedData.Results)
            };

            return ApiResponse<PagedResult<SemesterPublicResponse>>.SuccessResult(result);
        }

        public async Task<ApiResponse<SemesterPublicResponse>> GetById(Guid id)
        {
            var semester = await _semesterRepository.GetById(id);
            if (semester == null || semester.IsDelete)
                return ApiResponse<SemesterPublicResponse>.ErrorResult("Semester not found", 404);

            return ApiResponse<SemesterPublicResponse>.SuccessResult(_mapper.Map<SemesterPublicResponse>(semester));
        }

        public async Task<ApiResponse<SemesterPublicResponse>> Create(SemesterCreateRequest request)
        {
            // Check duplicate code
            var existing = await _semesterRepository.GetByCondition(s => s.Code == request.Code && !s.IsDelete);
            if (existing.Any())
                return ApiResponse<SemesterPublicResponse>.ErrorResult($"Semester code {request.Code} already exists");

            var semester = _mapper.Map<Semester>(request);
            await _semesterRepository.Insert(semester);
            
            return ApiResponse<SemesterPublicResponse>.SuccessResult(_mapper.Map<SemesterPublicResponse>(semester), "Semester created successfully");
        }

        public async Task<ApiResponse<SemesterPublicResponse>> Update(Guid id, SemesterUpdateRequest request)
        {
            var semester = await _semesterRepository.GetById(id);
            if (semester == null || semester.IsDelete)
                return ApiResponse<SemesterPublicResponse>.ErrorResult("Semester not found", 404);

            // Check duplicate code if code is changed
            if (!string.IsNullOrEmpty(request.Code) && request.Code != semester.Code)
            {
                var existing = await _semesterRepository.GetByCondition(s => s.Code == request.Code && !s.IsDelete);
                if (existing.Any())
                    return ApiResponse<SemesterPublicResponse>.ErrorResult($"Semester code {request.Code} already exists");
            }

            _mapper.Map(request, semester);
            await _semesterRepository.Update(semester);

            return ApiResponse<SemesterPublicResponse>.SuccessResult(_mapper.Map<SemesterPublicResponse>(semester), "Semester updated successfully");
        }

        public async Task<ApiResponse<bool>> SoftDelete(Guid id)
        {
            var semester = await _semesterRepository.GetById(id);
            if (semester == null || semester.IsDelete)
                return ApiResponse<bool>.ErrorResult("Semester not found", 404);

            semester.IsDelete = true;
            await _semesterRepository.Update(semester);

            return ApiResponse<bool>.SuccessResult(true, "Semester deleted successfully");
        }
    }
}
