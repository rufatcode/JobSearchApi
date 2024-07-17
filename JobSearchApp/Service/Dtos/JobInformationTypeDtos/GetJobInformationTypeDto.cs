using System;
using Domain.Entities;
using Service.Dtos.BaseDtos;
using Service.Dtos.JobInformationDtos;

namespace Service.Dtos.JobInformationTypeDtos
{
	public class GetJobInformationTypeDto:GetBaseDto
	{
        public string Name { get; set; }
        public List<GetJobInformationDto> JobInformations { get; set; }
        public GetJobInformationTypeDto()
		{
		}
	}
}

