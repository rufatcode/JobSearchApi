using System;
using Microsoft.AspNetCore.Http;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.CompanyDtos
{
	public class PutCompanyDto:PutBaseDto
	{
        public string Name { get; set; }
        public IFormFile? Logo { get; set; }
        public string About { get; set; }
        public string Location { get; set; }
        public string LocationIframe { get; set; }
        public string WebSiteUrl { get; set; }
        public PutCompanyDto()
		{
		}
	}
}

