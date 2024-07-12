using System;
using Microsoft.AspNetCore.Http;

namespace Service.Dtos.CategoryDtos
{
	public class PostCategoryDto
	{
        public string Name { get; set; }
        public IFormFile ImageUrl { get; set; }
        public PostCategoryDto()
		{
		}
	}
}

