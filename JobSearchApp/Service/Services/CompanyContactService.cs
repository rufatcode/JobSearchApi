using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class CompanyContactService: ICompanyContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(CompanyContact entity)
        {
            if (!await _unitOfWork.CompanyRepository.IsExist(c=>c.Id==entity.CompanyId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Company does not exist"
            };
            else if (!await _unitOfWork.PhoneNumberRespository.IsExist(c => c.Id == entity.PhoneNumberId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Phone Number does not exist"
            };
            else if (await IsExist(e=>e.CompanyId==entity.CompanyId&&e.PhoneNumberId==entity.PhoneNumberId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"data already has taken"
            };
            await _unitOfWork.CompanyContactRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "Company Contact Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "CompanyContact does not exist."
            };
            CompanyContact companyContact = await GetEntity(a => a.Id == id);
            companyContact.IsActive = false;
            companyContact.RemovedAt = DateTime.Now;
            await _unitOfWork.CompanyContactRepository.Update(companyContact);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"CompanyContact successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "CompanyContact does not exist."
            };
            CompanyContact companyContact = await GetEntity(a => a.Id == id);
            await _unitOfWork.CompanyContactRepository.Delete(companyContact);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"CompanyContact successfully deleted from db."
            };
        }

        public async Task<List<CompanyContact>> GetAll(Expression<Func<CompanyContact, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CompanyContactRepository.GetAll(predicate, includes);
        }

        public async Task<CompanyContact> GetEntity(Expression<Func<CompanyContact, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CompanyContactRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<CompanyContact, bool>> predicate = null)
        {
            return await _unitOfWork.CompanyContactRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(CompanyContact entity)
        {
            if (!await _unitOfWork.CompanyRepository.IsExist(c => c.Id == entity.CompanyId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Company does not exist"
            };
            else if (!await _unitOfWork.PhoneNumberRespository.IsExist(c => c.Id == entity.PhoneNumberId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Phone Number does not exist"
            };
            else if (await IsExist(e => e.CompanyId == entity.CompanyId && e.PhoneNumberId == entity.PhoneNumberId&&e.Id!=entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"data already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.CompanyContactRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Company Contact {entity.Id}.  successfully updated"
            };
        }
    }
}

