using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class JobInformationService: IJobInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobInformationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(JobInformation entity)
        {
            if (!await _unitOfWork.JobInformationTypeRepository.IsExist(c => c.Id == entity.JobInformationTypeId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Job Information Type does not exist"
            };
           else if (!await _unitOfWork.JobRepository.IsExist(c => c.Id == entity.JobId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Job does not exist"
            };
            await _unitOfWork.JobInformationRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "JobInformation  Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "JobInformation does not exist."
            };
            JobInformation jobInformation = await GetEntity(a => a.Id == id);
            jobInformation.IsActive = false;
            jobInformation.RemovedAt = DateTime.Now;
            await _unitOfWork.JobInformationRepository.Update(jobInformation);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"JobInformation successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "JobInformation does not exist."
            };
            JobInformation jobInformation = await GetEntity(a => a.Id == id);
            await _unitOfWork.JobInformationRepository.Delete(jobInformation);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"JobInformation successfully deleted from db."
            };
        }

        public async Task<List<JobInformation>> GetAll(Expression<Func<JobInformation, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.JobInformationRepository.GetAll(predicate, includes);
        }

        public async Task<JobInformation> GetEntity(Expression<Func<JobInformation, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.JobInformationRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<JobInformation, bool>> predicate = null)
        {
            return await _unitOfWork.JobInformationRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(JobInformation entity)
        {
            if (!await _unitOfWork.JobInformationTypeRepository.IsExist(c => c.Id == entity.JobInformationTypeId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Job Information Type does not exist"
            };
            else if (!await _unitOfWork.JobRepository.IsExist(c => c.Id == entity.JobId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Job does not exist"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.JobInformationRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Job Information {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

