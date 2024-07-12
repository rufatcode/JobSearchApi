using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.JobInformationDtos
{
	public class PutJobInformationDto:PutBaseDto
	{
        public string Name { get; set; }
        public int JobInformationTypeId { get; set; }
        public PutJobInformationDto()
		{
		}
	}
}

