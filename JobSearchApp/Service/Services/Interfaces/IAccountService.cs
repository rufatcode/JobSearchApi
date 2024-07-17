using System;
using Domain.Entities;
using Service.Dtos.AccountDtos;
using Service.Helpers;
using System.Linq.Expressions;

namespace Service.Services.Interfaces
{
	public interface IAccountService
	{
        Task<ResponseObj> Register(AppUser appUser, string password);
        Task<ResponseObj> Login(UserLoginDto loginDto);
        Task<ResponseObj> ForgetPassword(string email, string requestScheme, string requestHost);
        Task<ResponseObj> ResetPassword(UserResetPasswordDto userResetPasswordDto);
        Task<ResponseObj> ResetPasswordWithOTP(UserResetPasswordWithOTPDto userResetPasswordWithOTPDto);
        Task<ResponseObj> VerifyEmail(string VerifyEmail, string token);
        Task<ResponseObj> VerifyEmailWithOTP(string VerifyEmail, string otp);
        Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null);
        Task<bool> OtpIsExist(string email, string otp);
    }
}

