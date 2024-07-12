using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.PhoneNumberHeadlingDtos
{
	public class PutPhoneNumberHeadlingDto:PutBaseDto
	{
        public string Headling { get; set; }
        public PutPhoneNumberHeadlingDto()
		{
		}
	}
}

