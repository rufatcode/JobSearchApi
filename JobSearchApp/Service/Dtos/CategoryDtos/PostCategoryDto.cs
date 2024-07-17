using System;
using Microsoft.AspNetCore.Http;

namespace Service.Dtos.CategoryDtos
{
	public class PostCategoryDto
	{
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public PostCategoryDto()
		{
		}
	}
}

