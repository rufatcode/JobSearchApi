using System;
using Domain.Entities;

namespace Service.Dtos.JobInformationTypeDtos
{
	public class PostJobInformationTypeDto
	{
        public int JobId { get; set; }
        public string Name { get; set; }
        public PostJobInformationTypeDto()
		{
		}
	}
}

