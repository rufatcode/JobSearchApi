using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.PositionDtos
{
	public class PutPositionDto:PutBaseDto
	{
        public string Name { get; set; }
        public PutPositionDto()
		{
		}
	}
}

