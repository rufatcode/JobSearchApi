using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.PhoneNumberHeadlingDtos
{
	public class GetPhoneNumberHeadlingDto:GetBaseDto
	{
        public string Headling { get; set; }
        public GetPhoneNumberHeadlingDto()
		{
		}
	}
}

