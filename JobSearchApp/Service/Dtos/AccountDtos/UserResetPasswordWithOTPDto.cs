using System;
namespace Service.Dtos.AccountDtos
{
	public class UserResetPasswordWithOTPDto
	{
        public string Email { get; set; }
        public string OTP { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

