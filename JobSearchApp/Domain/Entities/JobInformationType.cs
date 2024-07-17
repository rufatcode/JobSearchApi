using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
	public class JobInformationType:BaseEntity
	{
		public string Name { get; set; }
		public List<JobInformation> JobInformations { get; set; }

		public JobInformationType()
		{
		}
	}
}

