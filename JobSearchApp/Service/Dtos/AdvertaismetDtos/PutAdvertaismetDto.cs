using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.AdvertaismetDtos
{
	public class PutAdvertaismetDto:PutBaseDto
	{
        public string Name { get; set; }
        public PutAdvertaismetDto()
		{
		}
	}
}

