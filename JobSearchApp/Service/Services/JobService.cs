using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class JobService: IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Job entity)
        {
            if (!await _unitOfWork.EmploymentTypeRepository.IsExist(e=>e.Id==entity.EmploymentTypeId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"EmploymentType does not exist"
            };
            else if (!await _unitOfWork.CityRepository.IsExist(e => e.Id == entity.CityId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"City does not exist"
            };
            else if (!await _unitOfWork.PositionRepository.IsExist(e => e.Id == entity.PositionId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Position does not exist"
            };
            else if (!await _unitOfWork.AdvertaismetRepository.IsExist(e => e.Id == entity.AdvertaismetId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Advertaismet does not exist"
            };
            else if (!await _unitOfWork.CategoryRepository.IsExist(e => e.Id == entity.CategoryId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Category does not exist"
            };
            else if (!await _unitOfWork.CompanyRepository.IsExist(e => e.Id == entity.CompanyId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Compamy does not exist"
            };
            await _unitOfWork.JobRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "Job Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Job does not exist."
            };
            Job job = await GetEntity(a => a.Id == id);
            job.IsActive = false;
            job.RemovedAt = DateTime.Now;
            await _unitOfWork.JobRepository.Update(job);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Job {job.Id}.{job.Name} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Job does not exist."
            };
            Job job = await GetEntity(a => a.Id == id);
            await _unitOfWork.JobRepository.Delete(job);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Job {job.Id}.{job.Name} successfully deleted from db."
            };
        }

        public async Task<List<Job>> GetAll(Expression<Func<Job, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.JobRepository.GetAll(predicate, includes);
        }

        public async Task<Job> GetEntity(Expression<Func<Job, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.JobRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Job, bool>> predicate = null)
        {
            return await _unitOfWork.JobRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Job entity)
        {
            if (!await _unitOfWork.EmploymentTypeRepository.IsExist(e => e.Id == entity.EmploymentTypeId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"EmploymentType does not exist"
            };
            else if (!await _unitOfWork.CityRepository.IsExist(e => e.Id == entity.CityId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"City does not exist"
            };
            else if (!await _unitOfWork.PositionRepository.IsExist(e => e.Id == entity.PositionId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Position does not exist"
            };
            else if (!await _unitOfWork.AdvertaismetRepository.IsExist(e => e.Id == entity.AdvertaismetId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Advertaismet does not exist"
            };
            else if (!await _unitOfWork.CategoryRepository.IsExist(e => e.Id == entity.CategoryId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Category does not exist"
            };
            else if (!await _unitOfWork.CompanyRepository.IsExist(e => e.Id == entity.CompanyId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Compamy does not exist"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.JobRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Job {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

