using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class PhoneNumberService: IPhoneNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhoneNumberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(PhoneNumber entity)
        {
            if (await IsExist(e => e.Phone == entity.Phone)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Phone} already has taken"
            };
            else if (!await _unitOfWork.PhoneNumberHeadlingRepository.IsExist(e => e.Id == entity.PhoneNumberHeadlingId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"PhoneNumberHeadling does not exist"
            };
            await _unitOfWork.PhoneNumberRespository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "PhoneNumber Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "PhoneNumber does not exist."
            };
            PhoneNumber phoneNumber = await GetEntity(a => a.Id == id);
            phoneNumber.IsActive = false;
            phoneNumber.RemovedAt = DateTime.Now;
            await _unitOfWork.PhoneNumberRespository.Update(phoneNumber);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"PhoneNumber {phoneNumber.Id}.{phoneNumber.Phone} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "PhoneNumber does not exist."
            };
            PhoneNumber phoneNumber = await GetEntity(a => a.Id == id);
            await _unitOfWork.PhoneNumberRespository.Delete(phoneNumber);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"PhoneNumber {phoneNumber.Id}.{phoneNumber.Phone} successfully deleted."
            };
        }

        public async Task<List<PhoneNumber>> GetAll(Expression<Func<PhoneNumber, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.PhoneNumberRespository.GetAll(predicate, includes);
        }

        public async Task<PhoneNumber> GetEntity(Expression<Func<PhoneNumber, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.PhoneNumberRespository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<PhoneNumber, bool>> predicate = null)
        {
            return await _unitOfWork.PhoneNumberRespository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(PhoneNumber entity)
        {
            if (await IsExist(e => e.Phone == entity.Phone)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Phone} already has taken"
            };
            else if (!await _unitOfWork.PhoneNumberHeadlingRepository.IsExist(e => e.Id == entity.PhoneNumberHeadlingId)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"PhoneNumberHeadling does not exist"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.PhoneNumberRespository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"City {entity.Id}.{entity.Phone}  successfully updated"
            };
        }
    }
}

