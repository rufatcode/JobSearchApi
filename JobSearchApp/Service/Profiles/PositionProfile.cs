using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.PositionDtos;

namespace Service.Profiles
{
	public class PositionProfile:Profile
	{
		public PositionProfile()
		{
			CreateMap<Position, GetPositionDto>().ReverseMap();
			CreateMap<Position, PostPositionDto>().ReverseMap();
			CreateMap<Position, PutPositionDto>().ReverseMap();
        }
	}
}

