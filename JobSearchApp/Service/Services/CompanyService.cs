using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class CompanyService: ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Company entity)
        {
            if (await IsExist(e => e.Name == entity.Name)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Name} already has taken"
            };
            await _unitOfWork.CompanyRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "Company Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Company does not exist."
            };
            Company company = await GetEntity(a => a.Id == id);
            company.IsActive = false;
            company.RemovedAt = DateTime.Now;
            await _unitOfWork.CompanyRepository.Update(company);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Company {company.Id}.{company.Name} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Company does not exist."
            };
            Company company = await GetEntity(a => a.Id == id);
            await _unitOfWork.CompanyRepository.Delete(company);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Company {company.Id}.{company.Name} successfully deleted from db ."
            };
        }

        public async Task<List<Company>> GetAll(Expression<Func<Company, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CompanyRepository.GetAll(predicate, includes);
        }

        public async Task<Company> GetEntity(Expression<Func<Company, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CompanyRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Company, bool>> predicate = null)
        {
            return await _unitOfWork.CompanyRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Company entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Company does not exist"
            };
            else if (await IsExist(e => e.Name == entity.Name && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Company {entity.Id}.{entity.Name} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.CompanyRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Company {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

