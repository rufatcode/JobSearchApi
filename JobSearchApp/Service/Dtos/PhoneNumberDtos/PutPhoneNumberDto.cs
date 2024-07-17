using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Service.Dtos.BaseDtos;
using Service.Dtos.PhoneNumberHeadlingDtos;

namespace Service.Dtos.PhoneNumberDtos
{
	public class PutPhoneNumberDto:PutBaseDto
	{
        public string Phone { get; set; }
        public int PhoneNumberHeadlingId { get; set; }
        public PutPhoneNumberDto()
		{
		}
	}
}

