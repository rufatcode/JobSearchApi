using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class JobInformationTypeService: IJobInformationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobInformationTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(JobInformationType entity)
        {
            if (await IsExist(e => e.Name == entity.Name)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Name} already has taken"
            };
            await _unitOfWork.JobInformationTypeRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "Job Information Type Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Job Information Type does not exist."
            };
            JobInformationType jobInformationType = await GetEntity(a => a.Id == id);
            jobInformationType.IsActive = false;
            jobInformationType.RemovedAt = DateTime.Now;
            await _unitOfWork.JobInformationTypeRepository.Update(jobInformationType);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Job Information Type {jobInformationType.Id}.{jobInformationType.Name} successfully deleted."
            }; 
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Job Information Type does not exist."
            };
            JobInformationType jobInformationType = await GetEntity(a => a.Id == id);
            await _unitOfWork.JobInformationTypeRepository.Delete(jobInformationType);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Job Information Type {jobInformationType.Id}.{jobInformationType.Name} successfully deleted from db."
            };
        }

        public async Task<List<JobInformationType>> GetAll(Expression<Func<JobInformationType, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.JobInformationTypeRepository.GetAll(predicate, includes);
        }

        public async Task<JobInformationType> GetEntity(Expression<Func<JobInformationType, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.JobInformationTypeRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<JobInformationType, bool>> predicate = null)
        {
            return await _unitOfWork.JobInformationTypeRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(JobInformationType entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "JobInformationType does not exist"
            };
            else if (await IsExist(e => e.Name == entity.Name && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"JobInformationType {entity.Id}.{entity.Name} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.JobInformationTypeRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"JobInformationType {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

