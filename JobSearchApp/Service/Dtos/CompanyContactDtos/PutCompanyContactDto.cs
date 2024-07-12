using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.CompanyContactDtos
{
	public class PutCompanyContactDto:PutBaseDto
	{
        public int CompanyId { get; set; }
        public int PhoneNumberHeadlingId { get; set; }
        public PutCompanyContactDto()
		{
		}
	}
}

