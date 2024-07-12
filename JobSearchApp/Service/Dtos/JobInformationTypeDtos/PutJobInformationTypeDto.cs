using System;
using Domain.Entities;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.JobInformationTypeDtos
{
	public class PutJobInformationTypeDto:PutBaseDto
	{
        public int JobId { get; set; }
        public string Name { get; set; }
        public PutJobInformationTypeDto()
		{
		}
	}
}

