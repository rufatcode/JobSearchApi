using System;
namespace Service.Dtos.BaseDtos
{
	public class GetBaseDto
	{
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string RegUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> RemovedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetBaseDto()
		{
		}
	}
}

