using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
	public class CompanyContact:BaseEntity
	{
		[ForeignKey(nameof(Company))]
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		[ForeignKey(nameof(PhoneNumber))]
		public int PhoneNumberId { get; set; }
		public PhoneNumber PhoneNumber { get; set; }
		public CompanyContact()
		{
		}
	}
}

