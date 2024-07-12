using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.CityDtos
{
	public class GetCityDto:GetBaseDto
	{
        public string Name { get; set; }
        public GetCityDto()
		{
		}
	}
}

