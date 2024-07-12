using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.AdvertaismetDtos
{
	public class GetAdvertaismetDto:GetBaseDto
	{
        public string Name { get; set; }
        public GetAdvertaismetDto()
		{
		}
	}
}

