using System;
using Domain.Entities;
using Service.Helpers;
using System.Linq.Expressions;
using Service.Dtos.UserDtos;

namespace Service.Services.Interfaces
{
	public interface IUserService
	{
        Task<List<GetUserDto>> GetAllUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes);
        Task<ResponseObj> Delete(string id);
        Task<ResponseObj> DeleteFromDb(string id);
        Task<GetUserDto> GetUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes);
        Task<ResponseObj> Update(string id, UpdateUserDto updateUserDto);
        Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null);
    }
}

