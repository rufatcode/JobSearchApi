using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Dtos.JobInformationDtos
{
	public class PostJobInformationDto
	{
        public string Name { get; set; }
        public int JobInformationTypeId { get; set; }
        public int JobId { get; set; }
        public PostJobInformationDto()
		{
		}
	}
}

