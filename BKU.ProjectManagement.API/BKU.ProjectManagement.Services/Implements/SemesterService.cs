using AutoMapper;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Requests;
using BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses;
using BKU.ProjectManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.Implements
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _semesterRepository;
        public SemesterService(ISemesterRepository semesterRepository) {
            this._semesterRepository = semesterRepository;
        }

        public async Task<List<SemesterPublicResponse>> GetAllPublicData(SemesterGetAllRequest request)
        {
            var semesterEnities = await _semesterRepository.GetByCondition(d => !d.IsDelete);
            return semesterEnities.Select(s => new SemesterPublicResponse
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsActive = s.IsActive,
                CreatedDate = s.CreatedDate,
                CreatedBy = s.CreatedBy,
                UpdatedDate = s.UpdatedDate,
                UpdatedBy = s.UpdatedBy,
                IsDelete = s.IsDelete
            }).ToList();
        }

        public async Task<SemesterPublicResponse> GetById(Guid id)
        {
            var semesterEntity = await _semesterRepository.GetById(id);
            if (semesterEntity == null || semesterEntity.IsDelete)
                return null;

            return new SemesterPublicResponse
            {
                Id = semesterEntity.Id,
                Code = semesterEntity.Code,
                Name = semesterEntity.Name,
                StartDate = semesterEntity.StartDate,
                EndDate = semesterEntity.EndDate,
                IsActive = semesterEntity.IsActive,
                CreatedDate = semesterEntity.CreatedDate,
                CreatedBy = semesterEntity.CreatedBy,
                UpdatedDate = semesterEntity.UpdatedDate,
                UpdatedBy = semesterEntity.UpdatedBy,
                IsDelete = semesterEntity.IsDelete
            };
        }

        //public async Task<MachineResponse> GetById(Guid id)
        //{
        //    var machineEntity = await machineRepository.GetByConditionQueryable(x => x.Id == id).FirstOrDefaultAsync();
        //    return mapper.Map<MachineResponse>(machineEntity);
        //}

        //public async Task<PagedResult<MachineResponse>> GetPaging(MachineGetPagingRequest request)
        //{
        //    var lineEnities = await machineRepository.GetWithPaging(request.PageIndex, request.PageSize, BaseFilter(request), null, "RoughnessLevel");
        //    return mapper.Map<PagedResult<MachineResponse>>(lineEnities);
        //}

        //public async Task<MachineResponse> SoftDelete(Guid id)
        //{
        //    var machineEntity = await machineRepository.GetByConditionQueryable(x => x.Id == id).FirstOrDefaultAsync();
        //    if (machineEntity == null)
        //        throw CommonErrorCode.DeleteIdNotFound.ErrorException();
        //    machineEntity.IsDelete = true;
        //    await machineRepository.Update(machineEntity);
        //    return mapper.Map<MachineResponse>(machineEntity);
        //}

        //public async Task<List<MachineResponse>> SyncMachineFromDataMaster()
        //{
        //    var itemDataMaster = await masterService.GetMachines();
        //    var validProcessIds = new List<short?>
        //{
        //    (short)EnumProcessId.Calibrating,
        //    (short)EnumProcessId.Rolling
        //};
        //    itemDataMaster = itemDataMaster.Where(d => validProcessIds.Contains(d.ProcessId));
        //    if (itemDataMaster == null || !itemDataMaster.Any())
        //        return new List<MachineResponse>();

        //    var machineEnities = (await machineRepository.GetAll()).ToList();

        //    foreach (var masterItem in itemDataMaster)
        //    {
        //        var existingLineEntity = (await lineRepository.GetByCondition(d => d.LineId == masterItem.LineID && !d.IsDelete)).FirstOrDefault();
        //        if (existingLineEntity == null)
        //            continue;

        //        var existingMachineEntity = machineEnities.FirstOrDefault(e => e.MachineId == masterItem.MachineId);
        //        var roughnessLevelHighId = (await roughnessLevelRepository.GetByCondition(d => d.LevelName == EnumRoughnessLevels.High.GetDescription())).FirstOrDefault().Id;

        //        if (roughnessLevelHighId == null)
        //            return new List<MachineResponse>();

        //        if (existingMachineEntity == null)
        //        {
        //            var newEntity = new Machine
        //            {
        //                MachineId = masterItem.MachineId,
        //                MachineName = masterItem.MachineName,
        //                MachineDescription = masterItem.MachineDescription,
        //                MachineNumber = masterItem.MachineNumber,
        //                RoughnessLevelId = roughnessLevelHighId,
        //                LineId = existingLineEntity.Id
        //            };

        //            await machineRepository.Insert(newEntity);
        //            machineEnities.Add(newEntity);
        //        }
        //        else
        //        {
        //            existingMachineEntity.MachineName = masterItem.MachineName;
        //            existingMachineEntity.MachineDescription = masterItem.MachineDescription;
        //            existingMachineEntity.MachineNumber = masterItem.MachineNumber;
        //            existingMachineEntity.LineId = existingLineEntity.Id;
        //            await machineRepository.Update(existingMachineEntity);
        //        }
        //    }
        //    return mapper.Map<List<MachineResponse>>(machineEnities.Where(d => !d.IsDelete));
        //}

        //public async Task<MachineResponse> Update(Guid id, MachineUpdateRequest input)
        //{
        //    var machineEntity = await machineRepository.GetByConditionQueryable(x => x.Id == id).FirstOrDefaultAsync();
        //    if (machineEntity == null)
        //        throw CommonErrorCode.UpdateIdNotFound.ErrorException();
        //    mapper.Map(input, machineEntity);
        //    await machineRepository.Update(machineEntity);
        //    return mapper.Map<MachineResponse>(machineEntity);
        //}
    }
}
