using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class PhoneNumberHeadlingService: IPhoneNumberHeadlingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhoneNumberHeadlingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(PhoneNumberHeadling entity)
        {
            if (await IsExist(e => e.Headling == entity.Headling)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Headling} already has taken"
            };
            await _unitOfWork.PhoneNumberHeadlingRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "PhoneNumberHeadling Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "PhoneNumberHeadling does not exist."
            };
            PhoneNumberHeadling phoneNumberHeadling = await GetEntity(a => a.Id == id);
            phoneNumberHeadling.IsActive = false;
            phoneNumberHeadling.RemovedAt = DateTime.Now;
            await _unitOfWork.PhoneNumberHeadlingRepository.Update(phoneNumberHeadling);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"PhoneNumberHeadling {phoneNumberHeadling.Id}.{phoneNumberHeadling.Headling} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "PhoneNumberHeadling does not exist."
            };
            PhoneNumberHeadling phoneNumberHeadling = await GetEntity(a => a.Id == id);
            await _unitOfWork.PhoneNumberHeadlingRepository.Delete(phoneNumberHeadling);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"PhoneNumberHeadling {phoneNumberHeadling.Id}.{phoneNumberHeadling.Headling} successfully deleted from db."
            };
        }

        public async Task<List<PhoneNumberHeadling>> GetAll(Expression<Func<PhoneNumberHeadling, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.PhoneNumberHeadlingRepository.GetAll(predicate, includes);
        }

        public async Task<PhoneNumberHeadling> GetEntity(Expression<Func<PhoneNumberHeadling, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.PhoneNumberHeadlingRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<PhoneNumberHeadling, bool>> predicate = null)
        {
            return await _unitOfWork.PhoneNumberHeadlingRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(PhoneNumberHeadling entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "PhoneNumberHeadling does not exist"
            };
            else if (await IsExist(e => e.Headling == entity.Headling && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"PhoneNumberHeadling {entity.Id}.{entity.Headling} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.PhoneNumberHeadlingRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"PhoneNumberHeadling {entity.Id}.{entity.Headling}  successfully updated"
            };
        }
    }
}

