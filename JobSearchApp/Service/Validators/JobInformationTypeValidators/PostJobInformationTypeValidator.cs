using System;
using FluentValidation;
using Service.Dtos.JobInformationTypeDtos;

namespace Service.Validators.JobInformationTypeValidators
{
	public class PostJobInformationTypeValidator: AbstractValidator<PostJobInformationTypeDto>
    {
		public PostJobInformationTypeValidator()
		{
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Length of name must be greater than 3")
                .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
        }
	}
}

