using System;
namespace Service.Dtos.UserDtos
{
	public class GetUserDto
	{
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public IList<string> Roles { get; set; }
        public string Email { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> RemovedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public GetUserDto()
		{
		}
	}
}

