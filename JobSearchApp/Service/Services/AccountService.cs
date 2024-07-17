using System;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Service.Dtos.AccountDtos;
using Service.Helpers;
using Service.Helpers.Interfaces;
using System.Linq.Expressions;
using System.Web;
using Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
	public class AccountService: IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISendEmail _sendEmail;
        private readonly UrlHelperService _urlHelper;
        private readonly ITokenService _tokenService;
        private readonly string _privateUrl = "http://localhost:8080";
        private readonly IDistributedCache _distributedCache;
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISendEmail sendEmail, UrlHelperService urlHelper, ITokenService tokenService, IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sendEmail = sendEmail;
            _urlHelper = urlHelper;
            _tokenService = tokenService;
            _distributedCache = distributedCache;
        }

        public async Task<ResponseObj> ForgetPassword(string email, string requestScheme, string requestHost)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null || appUser.IsDeleted) return new ResponseObj
            {
                ResponseMessage = "User does not exist.",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            var urlHelper = _urlHelper.GetUrlHelper();
            //string link = urlHelper.Action(nameof(ResetPassword), "Account", new { email = appUser.Email, token }, requestScheme, requestHost);
            string link = $"{_privateUrl}/reset/{HttpUtility.UrlEncode(appUser.Email)}/{HttpUtility.UrlEncode(token)}";
            string resetPasswordBody = string.Empty;
            using (StreamReader stream = new StreamReader("wwwroot/Verification/ResetPassword.html"))
            {
                resetPasswordBody = await stream.ReadToEndAsync();
            };
            resetPasswordBody = resetPasswordBody.Replace("{{link}}", link);
            resetPasswordBody = resetPasswordBody.Replace("{{userName}}", appUser.FullName);
            Random random = new();
            int otp = random.Next(100000, 999999);
            resetPasswordBody = resetPasswordBody.Replace("{{OTP}}", otp.ToString());
            Dictionary<string, string> userCache = new();
            userCache.Add(appUser.Email, otp.ToString());
            await _distributedCache.SetStringAsync(appUser.Email, JsonConvert.SerializeObject(userCache), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(4)
            });
            _sendEmail.Send("mypathacademy.com@gmail.com", "AistGroup", appUser.Email, resetPasswordBody, "Reset Password");
            return new ResponseObj
            {
                ResponseMessage = $"reset password link sended to {appUser.UserName} ",
                StatusCode = (int)StatusCodes.Status200OK
            };
        }

        public async Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null)
        {
            return predicate == null ? false : await _userManager.Users.AnyAsync(predicate);
        }
        public async Task<bool> OtpIsExist(string email, string otp)
        {
            string userCache = await _distributedCache.GetStringAsync(email);
            if (userCache == null)
            {
                return false;
            }
            Dictionary<string, string> userCacheResoult = JsonConvert.DeserializeObject<Dictionary<string, string>>(userCache);

            string otpResoult = userCacheResoult[email];
            if (otpResoult != otp) return false;
            return true;
        }
        public async Task<ResponseObj> Login(UserLoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(loginDto.EmailOrUserName);
            if (appUser == null)
            {
                appUser = await _userManager.FindByNameAsync(loginDto.EmailOrUserName);
                if (appUser == null)
                {
                    return new ResponseObj
                    {
                        ResponseMessage = "User does not exist.",
                        StatusCode = (int)StatusCodes.Status400BadRequest
                    };
                }
            }
            else if (appUser.IsDeleted) return new ResponseObj
            {
                ResponseMessage = "user does not exist",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            else if (!appUser.IsActive) return new ResponseObj
            {
                ResponseMessage = "User has been blocked by admin.",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            var resoult = await _signInManager.PasswordSignInAsync(appUser, loginDto.Password, true, true);
            if (resoult.IsLockedOut) return new ResponseObj
            {
                ResponseMessage = "Too many wrong attempts, try in ",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            else if (!appUser.EmailConfirmed) return new ResponseObj
            {
                ResponseMessage = "Email is not confirmed yet.",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            else if (!resoult.Succeeded) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = resoult.ToString()
            };
            IList<string> roles = await _userManager.GetRolesAsync(appUser);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = _tokenService.CreateToken(appUser, roles)
            };
        }

        public async Task<ResponseObj> Register(AppUser appUser, string password)
        {
            IdentityResult resoult = await _userManager.CreateAsync(appUser, password);
            if (!resoult.Succeeded)
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description))
                };
            }
            await _userManager.AddToRoleAsync(appUser, "User");
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var urlHelper = _urlHelper.GetUrlHelper();
            //string link = urlHelper.Action(nameof(VerifyEmail), "Account", new { VerifyEmail = appUser.Email, token }, requestScheme, requestHost);
            string link = $"{_privateUrl}/verify/{HttpUtility.UrlEncode(appUser.Email)}/{HttpUtility.UrlEncode(token)}";
            string verificationMessageBody = string.Empty;
            using (StreamReader fileStream = new StreamReader("wwwroot/Verification/VerificationEmail.html"))
            {
                verificationMessageBody = await fileStream.ReadToEndAsync();
            }
            verificationMessageBody = verificationMessageBody.Replace("{{link}}", link);
            verificationMessageBody = verificationMessageBody.Replace("{{userName}}", appUser.FullName);
            Random random = new();
            int otp = random.Next(100000, 999999);
            verificationMessageBody = verificationMessageBody.Replace("{{OTP}}", otp.ToString());
            Dictionary<string, string> userCache = new();
            userCache.Add(appUser.Email, otp.ToString());
            await _distributedCache.SetStringAsync(appUser.Email, JsonConvert.SerializeObject(userCache), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(4)
            });
            _sendEmail.Send("mypathacademy.com@gmail.com", "AistGroup", appUser.Email, verificationMessageBody, "Confirm Account");

            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{appUser.UserName}  please verify email"
            };
        }

        public async Task<ResponseObj> ResetPasswordWithOTP(UserResetPasswordWithOTPDto userResetPasswordWithOTPDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(userResetPasswordWithOTPDto.Email);
            if (appUser == null || appUser.IsDeleted) return new ResponseObj
            {
                ResponseMessage = "User not found",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            string userCache = await _distributedCache.GetStringAsync(appUser.Email);
            if (userCache == null)
            {
                return new ResponseObj
                {
                    ResponseMessage = $"This code has expired.",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            Dictionary<string, string> userCacheResoult = JsonConvert.DeserializeObject<Dictionary<string, string>>(userCache);

            string otpResoult = userCacheResoult[appUser.Email];
            if (otpResoult != userResetPasswordWithOTPDto.OTP) return new ResponseObj
            {
                ResponseMessage = $"Confirmation code is wrong.",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            IdentityResult resoult = await _userManager.ResetPasswordAsync(appUser, await _userManager.GeneratePasswordResetTokenAsync(appUser), userResetPasswordWithOTPDto.Password);
            if (!resoult.Succeeded) return new ResponseObj
            {
                ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description)),
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            await _userManager.UpdateSecurityStampAsync(appUser);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Password successfully reseted"
            };
        }
        public async Task<ResponseObj> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(userResetPasswordDto.Email);
            if (appUser == null || appUser.IsDeleted) return new ResponseObj
            {
                ResponseMessage = "User not found",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            var isSucceeded = await _userManager.VerifyUserTokenAsync(appUser, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", userResetPasswordDto.Token);
            if (!isSucceeded) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "TokenIsNotValid"
            };
            IdentityResult resoult = await _userManager.ResetPasswordAsync(appUser, userResetPasswordDto.Token, userResetPasswordDto.Password);
            if (!resoult.Succeeded) return new ResponseObj
            {
                ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description)),
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            await _userManager.UpdateSecurityStampAsync(appUser);
            await _distributedCache.RemoveAsync(appUser.Email);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Password successfully reseted"
            };
        }
        public async Task<ResponseObj> VerifyEmailWithOTP(string VerifyEmail, string otp)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(VerifyEmail);
            if (appUser == null || appUser.IsDeleted)
            {
                return new ResponseObj
                {
                    ResponseMessage = "User does not exist",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            if (appUser.EmailConfirmed) return new ResponseObj
            {
                ResponseMessage = "This email has already been comfirmed.",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            string userCache = await _distributedCache.GetStringAsync(appUser.Email);
            if (userCache == null)
            {
                return new ResponseObj
                {
                    ResponseMessage = $"This code has expired.",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            Dictionary<string, string> userCacheResoult = JsonConvert.DeserializeObject<Dictionary<string, string>>(userCache);

            string otpResoult = userCacheResoult[appUser.Email];
            if (otpResoult != otp) return new ResponseObj
            {
                ResponseMessage = $"Confirmation code is wrong.",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };

            appUser.EmailConfirmed = true;
            IdentityResult resoult = await _userManager.UpdateAsync(appUser);

            if (!resoult.Succeeded)
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = string.Join(", ", resoult.Errors.Select(e => e.Description))
                };
            }
            await _userManager.UpdateSecurityStampAsync(appUser);
            IList<string> roles = await _userManager.GetRolesAsync(appUser);
            return new ResponseObj
            {
                ResponseMessage = _tokenService.CreateToken(appUser, roles),
                StatusCode = (int)StatusCodes.Status200OK
            };
        }
        public async Task<ResponseObj> VerifyEmail(string VerifyEmail, string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(VerifyEmail);
            if (appUser == null || appUser.IsDeleted)
            {
                return new ResponseObj
                {
                    ResponseMessage = "User does not exist.",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            IdentityResult resoult = await _userManager.ConfirmEmailAsync(appUser, token);
            if (!resoult.Succeeded)
            {
                return new ResponseObj
                {
                    ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description)),
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            await _userManager.UpdateSecurityStampAsync(appUser);
            IList<string> roles = await _userManager.GetRolesAsync(appUser);
            return new ResponseObj
            {
                ResponseMessage = _tokenService.CreateToken(appUser, roles),
                StatusCode = (int)StatusCodes.Status200OK
            };
        }
    }
}

