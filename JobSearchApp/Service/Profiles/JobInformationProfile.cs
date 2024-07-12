using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.JobInformationDtos;

namespace Service.Profiles
{
	public class JobInformationProfile:Profile
	{
		public JobInformationProfile()
		{
			CreateMap<JobInformation, GetJobInformationDto>().ReverseMap();
			CreateMap<JobInformation, PostJobInformationDto>().ReverseMap();
			CreateMap<JobInformation, PutJobInformationDto>().ReverseMap();
        }
	}
}

