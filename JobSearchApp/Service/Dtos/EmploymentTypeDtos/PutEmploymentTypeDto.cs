using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.EmploymentTypeDtos
{
	public class PutEmploymentTypeDto:PutBaseDto
	{
        public string Name { get; set; }
        public PutEmploymentTypeDto()
		{
		}
	}
}

