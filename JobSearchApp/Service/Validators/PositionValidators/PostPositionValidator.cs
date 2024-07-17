using System;
using FluentValidation;
using Service.Dtos.PositionDtos;

namespace Service.Validators.PositionValidators
{
	public class PostPositionValidator: AbstractValidator<PostPositionDto>
    {
		public PostPositionValidator()
		{
            RuleFor(e => e.Name)
               .MinimumLength(3).WithMessage("Length of name must be greater than 3")
               .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
        }
	}
}

