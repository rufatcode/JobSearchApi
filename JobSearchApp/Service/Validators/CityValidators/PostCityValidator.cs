using System;
using FluentValidation;
using Service.Dtos.CityDtos;

namespace Service.Validators.CityValidators
{
	public class PostCityValidator:AbstractValidator<PostCityDto>
	{
		public PostCityValidator()
		{
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Length of name must be greater than 3")
                .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
        }
	}
}

