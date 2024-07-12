using System;
using Microsoft.AspNetCore.Http;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.CategoryDtos
{
	public class PutCategoryDto:PutBaseDto
	{
        public string Name { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public PutCategoryDto()
		{
		}
	}
}

