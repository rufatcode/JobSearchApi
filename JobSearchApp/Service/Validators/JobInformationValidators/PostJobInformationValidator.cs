using System;
using FluentValidation;
using Service.Dtos.JobInformationDtos;

namespace Service.Validators.JobInformationValidators
{
	public class PostJobInformationValidator: AbstractValidator<PostJobInformationDto>
    {
		public PostJobInformationValidator()
		{
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Length of name must be greater than 3")
                .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
        }
	}
}

