using System;
using Domain.Entities;
using Service.Dtos.BaseDtos;
using Service.Dtos.CompanyContactDtos;

namespace Service.Dtos.CompanyDtos
{
	public class GetCompanyDto:GetBaseDto
	{
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string About { get; set; }
        public string Location { get; set; }
        public string LocationIframe { get; set; }
        public string WebSiteUrl { get; set; }
        public List<GetCompanyContactDto> CompanyContacts { get; set; }
        public GetCompanyDto()
		{
		}
	}
}

