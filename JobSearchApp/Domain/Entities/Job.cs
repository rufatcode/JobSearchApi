using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
	public class Job:BaseEntity
	{
		public string Name { get; set; }
		public string Information { get; set; }
		public DateTime Deadline { get; set; }
		public double Salary { get; set; }
		[ForeignKey(nameof(EmploymentType))]
		public int EmploymentTypeId { get; set; }
		public EmploymentType EmploymentType { get; set; }
		[ForeignKey(nameof(City))]
		public int CityId { get; set; }
		public City City { get; set; }
		[ForeignKey(nameof(Position))]
		public int PositionId { get; set; }
		public Position Position { get; set; }
		[ForeignKey(nameof(Advertaismet))]
		public int AdvertaismetId { get; set; }
		public Advertaismet Advertaismet { get; set; }
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		[ForeignKey(nameof(Company))]
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public List<JobInformation> JobInformations { get; set; }
		public Job()
		{
		}
	}
}

