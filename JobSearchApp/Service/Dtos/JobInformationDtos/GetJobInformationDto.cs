using System;
using Domain.Entities;
using Service.Dtos.BaseDtos;
using Service.Dtos.JobInformationTypeDtos;

namespace Service.Dtos.JobInformationDtos
{
	public class GetJobInformationDto:GetBaseDto
	{
        public string Name { get; set; }
        public int JobInformationTypeId { get; set; }
        public GetJobInformationTypeDto JobInformationType { get; set; }
        public int JobId { get; set; }
        public GetJobInformationDto()
		{
		}
	}
}

