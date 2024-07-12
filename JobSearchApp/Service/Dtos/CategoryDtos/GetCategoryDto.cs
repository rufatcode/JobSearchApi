using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.CategoryDtos
{
	public class GetCategoryDto:GetBaseDto
	{
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public GetCategoryDto()
		{
		}
	}
}

