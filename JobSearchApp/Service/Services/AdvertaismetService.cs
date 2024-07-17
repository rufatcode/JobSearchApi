using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class AdvertaismetService: IAdvertaismetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdvertaismetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Advertaismet entity)
        {
            if (await IsExist(e => e.Name == entity.Name)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Name} already has taken"
            };
            await _unitOfWork.AdvertaismetRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "Advertaismet Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Advertaismet does not exist."
            };
            Advertaismet advertaismet = await GetEntity(a => a.Id == id);
            advertaismet.IsActive = false;
            advertaismet.RemovedAt = DateTime.Now;
            await _unitOfWork.AdvertaismetRepository.Update(advertaismet);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Advertaismet {advertaismet.Id}.{advertaismet.Name} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id )) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Advertaismet does not exist."
            };
            Advertaismet advertaismet = await GetEntity(a => a.Id == id);
            await _unitOfWork.AdvertaismetRepository.Delete(advertaismet);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Advertaismet {advertaismet.Id}.{advertaismet.Name} successfully deleted from db."
            };
        }

        public async Task<List<Advertaismet>> GetAll(Expression<Func<Advertaismet, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.AdvertaismetRepository.GetAll(predicate, includes); 
        }

        public async Task<Advertaismet> GetEntity(Expression<Func<Advertaismet, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.AdvertaismetRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Advertaismet, bool>> predicate = null)
        {
            return await _unitOfWork.AdvertaismetRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Advertaismet entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Advertaismet does not exist"
            };
            else if (await IsExist(e => e.Name == entity.Name && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Advertaismet {entity.Id}.{entity.Name} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.AdvertaismetRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Advertaismet {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

