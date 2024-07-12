using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.AdvertaismetDtos;

namespace Service.Profiles
{
	public class AdvertaismetProfile:Profile
	{
		public AdvertaismetProfile()
		{
			CreateMap<Advertaismet, GetAdvertaismetDto>().ReverseMap();
			CreateMap<Advertaismet, PostAdvertaismetDto>().ReverseMap();
			CreateMap<Advertaismet, PutAdvertaismetDto>().ReverseMap();
        }
	}
}

