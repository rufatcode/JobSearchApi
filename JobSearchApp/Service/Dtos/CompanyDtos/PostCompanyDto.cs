using System;
using Microsoft.AspNetCore.Http;

namespace Service.Dtos.CompanyDtos
{
	public class PostCompanyDto
	{
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
        public string About { get; set; }
        public string Location { get; set; }
        public string LocationIframe { get; set; }
        public string WebSiteUrl { get; set; }
        public PostCompanyDto()
		{
		}
	}
}

