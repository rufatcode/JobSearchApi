using System;
using FluentValidation;
using Service.Dtos.PhoneNumberDtos;

namespace Service.Validators.PhoneNumberValidators
{
	public class PostPhoneNumberValidator: AbstractValidator<PostPhoneNumberDto>
    {
		public PostPhoneNumberValidator()
		{
            RuleFor(e => e.Phone)
               .MinimumLength(6).WithMessage("Length of Phone must be greater than 6");
        }
	}
}

