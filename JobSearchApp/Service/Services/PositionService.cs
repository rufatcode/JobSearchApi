using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class PositionService: IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PositionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Position entity)
        {
            if (await IsExist(e => e.Name == entity.Name)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Name} already has taken"
            };
            await _unitOfWork.PositionRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "Position Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Position does not exist."
            };
            Position position = await GetEntity(a => a.Id == id);
            position.IsActive = false;
            position.RemovedAt = DateTime.Now;
            await _unitOfWork.PositionRepository.Update(position);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Position {position.Id}.{position.Name} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Position does not exist."
            };
            Position position = await GetEntity(a => a.Id == id);
            await _unitOfWork.PositionRepository.Delete(position);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Position {position.Id}.{position.Name} successfully deleted from db."
            };
        }

        public async Task<List<Position>> GetAll(Expression<Func<Position, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.PositionRepository.GetAll(predicate, includes);
        }

        public async Task<Position> GetEntity(Expression<Func<Position, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.PositionRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Position, bool>> predicate = null)
        {
            return await _unitOfWork.PositionRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Position entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Position does not exist"
            };
            else if (await IsExist(e => e.Name == entity.Name && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Position {entity.Id}.{entity.Name} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.PositionRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Position {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

