using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.JobInformationDtos
{
	public class GetJobInformationDto:GetBaseDto
	{
        public string Name { get; set; }
        public int JobInformationTypeId { get; set; }
        public GetJobInformationDto()
		{
		}
	}
}

