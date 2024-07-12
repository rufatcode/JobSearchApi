using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
	public class JobInformation:BaseEntity
	{
		public string Name { get; set; }
		[ForeignKey(nameof(JobInformationType))]
		public int JobInformationTypeId { get; set; }
	
		public JobInformation()
		{
		}
	}
}

