using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.CityDtos;

namespace Service.Profiles
{
	public class CityProfile:Profile
	{
		public CityProfile()
		{
			CreateMap<City, GetCityDto>().ReverseMap();
		}
	}
}

