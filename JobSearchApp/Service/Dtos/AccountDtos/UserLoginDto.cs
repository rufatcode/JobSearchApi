using System;
namespace Service.Dtos.AccountDtos
{
	public class UserLoginDto
	{
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }
}

