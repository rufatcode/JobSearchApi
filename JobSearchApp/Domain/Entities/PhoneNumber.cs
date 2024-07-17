using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
	public class PhoneNumber:BaseEntity
	{
		public string Phone { get; set; }
		[ForeignKey(nameof(PhoneNumberHeadling))]
		public int PhoneNumberHeadlingId { get; set; }
		public PhoneNumberHeadling PhoneNumberHeadling { get; set; }
		public PhoneNumber()
		{
		}
	}
}

