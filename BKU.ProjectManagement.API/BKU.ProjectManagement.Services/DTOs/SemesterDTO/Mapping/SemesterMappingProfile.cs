using AutoMapper;
using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.DTOs.SemesterDTO.Mapping
{
    public class SemesterMappingProfile : Profile
    {
        public SemesterMappingProfile() {
            CreateMap<Semester, SemesterPublicResponse>();
        }
    }
}
