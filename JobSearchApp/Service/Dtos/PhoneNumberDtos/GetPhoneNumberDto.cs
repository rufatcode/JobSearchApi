using System;
using Service.Dtos.BaseDtos;
using Service.Dtos.PhoneNumberHeadlingDtos;

namespace Service.Dtos.PhoneNumberDtos
{
	public class GetPhoneNumberDto:GetBaseDto
	{
        public string Phone { get; set; }
        public int PhoneNumberHeadlingId { get; set; }
        public GetPhoneNumberHeadlingDto PhoneNumberHeadling { get; set; }
        public GetPhoneNumberDto()
		{
		}
	}
}

