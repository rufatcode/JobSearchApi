using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Service.Dtos.BaseDtos;
using Service.Dtos.CompanyDtos;
using Service.Dtos.PhoneNumberHeadlingDtos;

namespace Service.Dtos.CompanyContactDtos
{
	public class GetCompanyContactDto:GetBaseDto
	{
        public int CompanyId { get; set; }
        public GetCompanyDto Company { get; set; }
        public int PhoneNumberHeadlingId { get; set; }
        public GetPhoneNumberHeadlingDto PhoneNumberHeadling { get; set; }
        public GetCompanyContactDto()
		{
		}
	}
}

