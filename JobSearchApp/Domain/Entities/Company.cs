using System;
using Domain.Commons;

namespace Domain.Entities
{
	public class Company:BaseEntity
	{
		public string Name { get; set; }
		public string LogoUrl { get; set; }
		public string About { get; set; }
		public string Location { get; set; }
		public string LocationIframe { get; set; }
		public string WebSiteUrl { get; set; }
		public List<CompanyContact> CompanyContacts { get; set; }
	}
}

