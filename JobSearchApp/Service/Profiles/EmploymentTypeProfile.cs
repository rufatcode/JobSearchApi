using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.EmploymentTypeDtos;

namespace Service.Profiles
{
	public class EmploymentTypeProfile:Profile
	{
		public EmploymentTypeProfile()
		{
			CreateMap<EmploymentType, GetEmploymentTypeDto>().ReverseMap();
			CreateMap<EmploymentType, PutEmploymentTypeDto>().ReverseMap();
			CreateMap<EmploymentType, PostEmploymentTypeDto>().ReverseMap();
        }
	}
}

