using System;
using FluentValidation;
using Service.Dtos.AdvertaismetDtos;

namespace Service.Validators.AdvertaismetValidators
{
	public class PostAdvertaismetValidator: AbstractValidator<PostAdvertaismetDto>
    {
		public PostAdvertaismetValidator()
		{
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Length of name must be greater than 3")
                .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
        }
	}
}

