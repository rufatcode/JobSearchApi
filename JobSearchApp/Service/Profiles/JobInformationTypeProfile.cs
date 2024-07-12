using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.JobInformationTypeDtos;

namespace Service.Profiles
{
	public class JobInformationTypeProfile:Profile
	{
		public JobInformationTypeProfile()
		{
			CreateMap<JobInformationType, GetJobInformationTypeDto>().ReverseMap();
			CreateMap<JobInformationType, PostJobInformationTypeDto>().ReverseMap();
			CreateMap<JobInformationType, PutJobInformationTypeDto>().ReverseMap();
        }
	}
}

