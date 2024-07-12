using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.CategoryDtos;

namespace Service.Profiles
{
	public class CategoryProfile:Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, PostCategoryDto>().ReverseMap();
			CreateMap<Category, PutCategoryDto>().ReverseMap();
			CreateMap<Category, GetCategoryDto>().ReverseMap();
        }
	}
}

