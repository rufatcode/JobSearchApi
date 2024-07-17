using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Dtos.CompanyContactDtos
{
	public class PostCompanyContactDto
	{
        public int CompanyId { get; set; }
        public int PhoneNumberId { get; set; }
        public PostCompanyContactDto()
		{
		}
	}
}

