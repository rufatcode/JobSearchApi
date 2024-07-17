using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class EmploymentTypeService: IEmploymentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmploymentTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(EmploymentType entity)
        {
            if (await IsExist(e => e.Name == entity.Name)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Name} already has taken"
            };
            await _unitOfWork.EmploymentTypeRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "EmploymentType Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "EmploymentType does not exist."
            };
            EmploymentType company = await GetEntity(a => a.Id == id);
            company.IsActive = false;
            company.RemovedAt = DateTime.Now;
            await _unitOfWork.EmploymentTypeRepository.Update(company);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"EmploymentType {company.Id}.{company.Name} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "EmploymentType does not exist."
            };
            EmploymentType company = await GetEntity(a => a.Id == id);
            await _unitOfWork.EmploymentTypeRepository.Delete(company);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"EmploymentType {company.Id}.{company.Name} successfully deleted from db."
            };
        }

        public async Task<List<EmploymentType>> GetAll(Expression<Func<EmploymentType, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.EmploymentTypeRepository.GetAll(predicate, includes);
        }

        public async Task<EmploymentType> GetEntity(Expression<Func<EmploymentType, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.EmploymentTypeRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<EmploymentType, bool>> predicate = null)
        {
            return await _unitOfWork.EmploymentTypeRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(EmploymentType entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "EmploymentType does not exist"
            };
            else if (await IsExist(e => e.Name == entity.Name && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"EmploymentType {entity.Id}.{entity.Name} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.EmploymentTypeRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"EmploymentType {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

