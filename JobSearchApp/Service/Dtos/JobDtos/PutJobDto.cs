using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.JobDtos
{
	public class PutJobDto:PutBaseDto
	{
        public string Name { get; set; }
        public string Information { get; set; }
        public DateTime Deadline { get; set; }
        public double Salary { get; set; }
        public int EmploymentTypeId { get; set; }
        public int CityId { get; set; }
        public int PositionId { get; set; }
        public int AdvertaismetId { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public string InformationForApply { get; set; }
        public PutJobDto()
		{
		}
	}
}

