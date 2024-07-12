using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.PositionDtos
{
	public class GetPositionDto:GetBaseDto
	{
        public string Name { get; set; }
        public GetPositionDto()
		{
		}
	}
}

