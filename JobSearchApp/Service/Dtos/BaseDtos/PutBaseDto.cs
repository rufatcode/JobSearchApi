using System;
namespace Service.Dtos.BaseDtos
{
	public class PutBaseDto
	{
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public PutBaseDto()
		{
		}
	}
}

