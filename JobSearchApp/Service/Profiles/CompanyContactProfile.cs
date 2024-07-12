using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.CompanyContactDtos;

namespace Service.Profiles
{
	public class CompanyContactProfile:Profile
	{
		public CompanyContactProfile()
		{
			CreateMap<CompanyContact, GetCompanyContactDto>();
			CreateMap<CompanyContact, PutCompanyContactDto>();
			CreateMap<CompanyContact, PostCompanyContactDto>();
        }
	}
}

