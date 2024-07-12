using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.EmploymentTypeDtos
{
	public class GetEmploymentTypeDto:GetBaseDto
	{
        public string Name { get; set; }
        public GetEmploymentTypeDto()
		{
		}
	}
}

