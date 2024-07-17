using System;
using System.Linq.Expressions;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class CategoryService: ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Category entity)
        {
            if (await IsExist(e => e.Name == entity.Name)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Name} already has taken"
            };
            await _unitOfWork.CategoryRepository.Create(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = StatusCodes.Status201Created,
                ResponseMessage = "Category Successfully Created"
            };
        }

        public async Task<ResponseObj> Delete(int id)
        {
            if (!await IsExist(e => e.Id == id && e.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Category does not exist."
            };
            Category category = await GetEntity(a => a.Id == id);
            category.IsActive = false;
            category.RemovedAt = DateTime.Now;
            await _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Category {category.Id}.{category.Name} successfully deleted."
            };
        }

        public async Task<ResponseObj> DeleteFromDB(int id)
        {
            if (!await IsExist(e => e.Id == id )) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Category does not exist."
            };
            Category category = await GetEntity(a => a.Id == id);
            await _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Category {category.Id}.{category.Name} successfully deleted from db."
            };
        }

        public async Task<List<Category>> GetAll(Expression<Func<Category, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CategoryRepository.GetAll(predicate, includes);
        }

        public async Task<Category> GetEntity(Expression<Func<Category, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CategoryRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Category, bool>> predicate = null)
        {
            return await _unitOfWork.CategoryRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Category entity)
        {
            if (!await IsExist(e => e.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Category does not exist"
            };
            else if (await IsExist(e => e.Name == entity.Name && e.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = StatusCodes.Status404NotFound,
                ResponseMessage = $"Category {entity.Id}.{entity.Name} already has taken"
            };
            entity.UpdatedAt = DateTime.Now;
            if (entity.IsActive)
            {
                entity.RemovedAt = null;
            }
            await _unitOfWork.CategoryRepository.Update(entity);

            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"Category {entity.Id}.{entity.Name}  successfully updated"
            };
        }
    }
}

