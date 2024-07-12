using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.CompanyDtos;

namespace Service.Profiles
{
	public class CompanyProfile:Profile
	{
		public CompanyProfile()
		{
			CreateMap<Company, GetCompanyDto>().ReverseMap();
			CreateMap<Company, PostCompanyDto>().ReverseMap();
			CreateMap<Company, PutCompanyDto>().ReverseMap();
        }
	}
}

