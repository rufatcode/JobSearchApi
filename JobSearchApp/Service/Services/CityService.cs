using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class CityService: ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(City entity)
        {
            if (await IsExist(e => e.Name == entity.Name)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Name} already has taken"
            };
            await _unitOfWork.CityRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "City Successfully Created"
            }; 
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "City does not exist."
            };
            City city = await GetEntity(a => a.Id == id);
            city.IsActive = false;
            city.RemovedAt = DateTime.Now;
            await _unitOfWork.CityRepository.Update(city);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"City {city.Id}.{city.Name} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id )) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "City does not exist."
            };
            City city = await GetEntity(a => a.Id == id);
            await _unitOfWork.CityRepository.Delete(city);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"City {city.Id}.{city.Name} successfully deleted from db."
            };
        }

        public async Task<List<City>> GetAll(Expression<Func<City, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CityRepository.GetAll(predicate, includes);
        }

        public async Task<City> GetEntity(Expression<Func<City, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CityRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<City, bool>> predicate = null)
        {
            return await _unitOfWork.CityRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(City entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "City does not exist"
            };
            else if (await IsExist(e => e.Name == entity.Name && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"City {entity.Id}.{entity.Name} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.CityRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"City {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

