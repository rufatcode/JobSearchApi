using System;
namespace Service.Dtos.UserDtos
{
	public class UpdateUserDto
	{
        public IList<string> Roles { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
        public string FullName { get; set; }
    }
}

