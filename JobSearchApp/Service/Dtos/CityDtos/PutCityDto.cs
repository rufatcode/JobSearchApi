using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.CityDtos
{
	public class PutCityDto:PutBaseDto
	{
        public string Name { get; set; }
        public PutCityDto()
		{
		}
	}
}

