using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Dtos.UserDtos;
using Service.Helpers;
using System.Linq.Expressions;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class UserService:IUserService
	{
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<ResponseObj> Update(string id, UpdateUserDto updateUserDto)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var role in updateUserDto.Roles)
            {
                if (!roles.Any(r => r.ToString() == role)) return new ResponseObj
                {
                    ResponseMessage = "Role is not exist",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            if (await _userManager.IsInRoleAsync(appUser, "SupperAdmin")) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "Supper admin can not update"
            };
            else if (updateUserDto.Roles.Any(r => r == "SupperAdmin")) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "SupperAdmin role can be belong to only 1 user"
            };
            UpdateUserDto existUserUpdateDto = _mapper.Map<UpdateUserDto>(appUser);
            existUserUpdateDto.Roles = await _userManager.GetRolesAsync(appUser);
            if (updateUserDto.Roles == null || updateUserDto.Roles.Count == 0) return new ResponseObj
            {
                ResponseMessage = "Roles Must not be Null",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            if (!appUser.IsDeleted && updateUserDto.IsDeleted)
            {
                appUser.RemovedAt = DateTime.Now;
            }
            _mapper.Map(updateUserDto, appUser);
            if (updateUserDto.IsDeleted)
            {

                if (await _userManager.IsInRoleAsync(appUser, "Admin") || await _userManager.IsInRoleAsync(appUser, "SupperAdmin")) return new ResponseObj
                {
                    ResponseMessage = "Something went wrong",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            else
            {
                appUser.RemovedAt = null;
            }

            await _userManager.RemoveFromRolesAsync(appUser, existUserUpdateDto.Roles);

            appUser.UpdatedAt = DateTime.Now;
            await _userManager.AddToRolesAsync(appUser, updateUserDto.Roles);
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded) return new ResponseObj
            {
                ResponseMessage = string.Join(", ", result.Errors.Select(e => e.Description)),
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            return new ResponseObj
            {
                ResponseMessage = $"{appUser.UserName} successfully updated",
                StatusCode = (int)StatusCodes.Status200OK
            };
        }
        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(u => u.Id == id && !u.IsDeleted)) return new ResponseObj
            {
                ResponseMessage = "User is not exist",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (await _userManager.IsInRoleAsync(appUser, "Admin") || await _userManager.IsInRoleAsync(appUser, "SupperAdmin")) return new ResponseObj
            {
                ResponseMessage = "Something went wrong",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            appUser.IsDeleted = true;
            appUser.RemovedAt = DateTime.Now;
            await _userManager.UpdateAsync(appUser);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{appUser.UserName} succesfully deleted"
            };
        }
        public async Task<ResponseObj> DeleteFromDb(string id)
        {
            if (!await IsExist(u => u.Id == id && !u.IsDeleted)) return new ResponseObj
            {
                ResponseMessage = "User is not exist",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (await _userManager.IsInRoleAsync(appUser, "Admin") || await _userManager.IsInRoleAsync(appUser, "SupperAdmin")) return new ResponseObj
            {
                ResponseMessage = "Something went wrong",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            await _userManager.DeleteAsync(appUser);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{appUser.UserName} succesfully deleted"
            };
        }

        public async Task<List<GetUserDto>> GetAllUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes)
        {
            IQueryable<AppUser> query = _userManager.Users;
            if (includes.Length > 0)
            {
                query = GetAllIncludes(includes);
            }
            List<AppUser> users = predicate == null ? await query.ToListAsync() : await query.Where(predicate).ToListAsync();
            List<GetUserDto> getUserDtos = _mapper.Map<List<GetUserDto>>(users);
            for (int i = 0; i < users.Count; i++)
            {
                getUserDtos[i].Roles = await _userManager.GetRolesAsync(users[i]);
            }
            return getUserDtos;
        }

        public async Task<GetUserDto> GetUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes)
        {
            IQueryable<AppUser> query = _userManager.Users;
            if (includes.Length > 0)
            {
                query = GetAllIncludes(includes);
            }
            if (predicate == null) return null;
            AppUser user = await query.FirstOrDefaultAsync(predicate);
            GetUserDto getUserDto = _mapper.Map<GetUserDto>(user);
            getUserDto.Roles = await _userManager.GetRolesAsync(user);
            return getUserDto;
        }

        public async Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null)
        {
            return predicate == null ? false : await _userManager.Users.AnyAsync(predicate);
        }

        public IQueryable<AppUser> GetAllIncludes(params string[] includes)
        {
            IQueryable<AppUser> query = _userManager.Users;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}

