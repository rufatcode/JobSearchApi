using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.PhoneNumberHeadlingDtos;

namespace Service.Profiles
{
	public class PhoneNumberHeadlingProfile:Profile
	{
		public PhoneNumberHeadlingProfile()
		{
			CreateMap<PhoneNumberHeadling, GetPhoneNumberHeadlingDto>().ReverseMap();
			CreateMap<PhoneNumberHeadling, PostPhoneNumberHeadlingDto>().ReverseMap();
			CreateMap<PhoneNumberHeadling, PutPhoneNumberHeadlingDto>().ReverseMap();
        }
	}
}

