using System;
using FluentValidation;
using Service.Dtos.PositionDtos;

namespace Service.Validators.PositionValidators
{
	public class PutPositionValidator:AbstractValidator<PutPositionDto>
	{
		public PutPositionValidator()
		{
            RuleFor(e => e.Name)
               .MinimumLength(3).WithMessage("Length of name must be greater than 3")
               .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
        }
	}
}

