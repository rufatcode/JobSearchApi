using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.PhoneNumberDtos;

namespace Service.Profiles
{
	public class PhoneNumberProfile:Profile
	{
		public PhoneNumberProfile()
		{
			CreateMap<PhoneNumber, GetPhoneNumberDto>().ReverseMap();
			CreateMap<PhoneNumber, PostPhoneNumberDto>().ReverseMap();
			CreateMap<PhoneNumber, PutPhoneNumberDto>().ReverseMap();
        }
	}
}

