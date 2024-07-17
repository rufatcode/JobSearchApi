using System;
namespace Service.Dtos.AccountDtos
{
	public class UserRegisterDto
	{
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

