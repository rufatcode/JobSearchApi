using System;
namespace Service.Dtos.AccountDtos
{
	public class UserResetPasswordDto
	{
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
	}
}

