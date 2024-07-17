using System;
using FluentValidation;
using Service.Dtos.PhoneNumberDtos;

namespace Service.Validators.PhoneNumberValidators
{
	public class PutPhoneNumberValidator:AbstractValidator<PutPhoneNumberDto>
	{
		public PutPhoneNumberValidator()
		{
			RuleFor(e => e.Phone)
			   .MinimumLength(6).WithMessage("Length of Phone must be greater than 6");
        }
	}
}

