using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.AccountDtos;
using Service.Dtos.UserDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public AccountController(IMapper mapper, IAccountService accountService, IUserService userService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            AppUser appUser = _mapper.Map<AppUser>(userRegisterDto);
            appUser.UserName = userRegisterDto.FullName.Trim().Replace(" ", "_");
            while (await _accountService.IsExist(u => u.UserName == appUser.UserName))
            {
                appUser.UserName = appUser.UserName + Guid.NewGuid().ToString().Substring(0, 6);
            }
            appUser.CreatedAt = DateTime.Now;
            appUser.IsActive = true;
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.Value;

            ResponseObj responseObj = await _accountService.Register(appUser, userRegisterDto.Password);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest)
            {
                return BadRequest(responseObj.ResponseMessage);
            }


            return Ok(responseObj);
        }
        [HttpGet("OtpIsExist")]
        public async Task<IActionResult> OtpIsExist(string email, string otp)
        {
            if (!int.TryParse(otp, out int intOTP) || email == null || otp == null || otp.Length != 6) return BadRequest("Something went wrong");
            var resoult = await _accountService.OtpIsExist(email, otp);
            if (!resoult) return BadRequest("Something went wrong");

            return Ok(resoult);
        }
        [HttpPost("VerifyEmailWithOTP")]
        public async Task<IActionResult> VerifyEmailWithOTP(string verifyEmail, string otp)
        {
            if (!int.TryParse(otp, out int intOTP) || VerifyEmail == null || otp == null || otp.Length != 6) return BadRequest("Something went wrong");
            ResponseObj responseObj = await _accountService.VerifyEmailWithOTP(verifyEmail, otp);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj.ResponseMessage);
        }
        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string verifyEmail, string token)
        {
            if (VerifyEmail == null || token == null) return BadRequest("Something went wrong");
            ResponseObj responseObj = await _accountService.VerifyEmail(verifyEmail, token);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj.ResponseMessage);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseObj responseObj = await _accountService.Login(loginDto);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);

            return Ok(responseObj.ResponseMessage);
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (email == null) return BadRequest("Email not found. Make sure you typed correctly");
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.Value;
            ResponseObj responseObj = await _accountService.ForgetPassword(email, scheme, host);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj.ResponseMessage);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto userResetPasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _accountService.ResetPassword(userResetPasswordDto);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj.ResponseMessage);
        }
        [HttpPost("ResetPasswordWithOTP")]
        public async Task<IActionResult> ResetPasswordWithOTP([FromBody] UserResetPasswordWithOTPDto userResetPasswordWithOTPDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _accountService.ResetPasswordWithOTP(userResetPasswordWithOTPDto);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj.ResponseMessage);
        }

        [Authorize]
        [HttpGet("IsExist/{id}")]
        public async Task<IActionResult> IsExist(string id)
        {
            if (id == null) return BadRequest();

            return Ok(await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive));
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (id == null) return BadRequest();
            else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("User does not exist");
            GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id);

            return Ok(_mapper.Map<GetUserDto>(getUserDto));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<GetUserDto> getUsersDto = await _userService.GetAllUser();

            return Ok(_mapper.Map<List<GetUserDto>>(getUsersDto));
        }
    }
}

