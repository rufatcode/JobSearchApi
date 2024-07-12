using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Service.Dtos.BaseDtos;
using Service.Dtos.EmploymentTypeDtos;
using Service.Dtos.CityDtos;
using Service.Dtos.AdvertaismetDtos;
using Service.Dtos.CategoryDtos;
using Service.Dtos.CompanyDtos;
using Service.Dtos.PositionDtos;
using Service.Dtos.JobInformationTypeDtos;

namespace Service.Dtos.JobDtos
{
	public class GetJobDto:GetBaseDto
	{
        public string Name { get; set; }
        public string Information { get; set; }
        public DateTime Deadline { get; set; }
        public double Salary { get; set; }
        public int EmploymentTypeId { get; set; }
        public GetEmploymentTypeDto EmploymentType { get; set; }
        public int CityId { get; set; }
        public GetCityDto City { get; set; }
        public int PositionId { get; set; }
        public GetPositionDto Position { get; set; }
        public int AdvertaismetId { get; set; }
        public GetAdvertaismetDto Advertaismet { get; set; }
        public int CategoryId { get; set; }
        public GetCategoryDto Category { get; set; }
        public int CompanyId { get; set; }
        public GetCompanyDto Company { get; set; }
        public string InformationForApply { get; set; }
        public List<GetJobInformationTypeDto> JobInformationTypes { get; set; }
        public GetJobDto()
		{
		}
	}
}

