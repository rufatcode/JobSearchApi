using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.JobDtos;

namespace Service.Profiles
{
	public class JobProfile:Profile
	{
		public JobProfile()
		{
			CreateMap<Job, GetJobDto>().ReverseMap();
			CreateMap<Job, PostJobDto>().ReverseMap();
			CreateMap<Job, PutJobDto>().ReverseMap();
        }
	}
}

