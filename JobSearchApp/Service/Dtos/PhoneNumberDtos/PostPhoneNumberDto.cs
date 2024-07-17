using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Dtos.PhoneNumberDtos
{
	public class PostPhoneNumberDto
	{
        public string Phone { get; set; }
        public int PhoneNumberHeadlingId { get; set; }
        public PostPhoneNumberDto()
		{
		}
	}
}

