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
		[ForeignKey(nameof(PhoneNumberHeadling))]
		public int PhoneNumberHeadlingId { get; set; }
		public PhoneNumberHeadling  PhoneNumberHeadling { get; set; }
		public CompanyContact()
		{
		}
	}
}

