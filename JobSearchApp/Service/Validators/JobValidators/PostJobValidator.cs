using System;
using FluentValidation;
using Service.Dtos.JobDtos;

namespace Service.Validators.JobValidators
{
	public class PostJobValidator:AbstractValidator<PostJobDto>
	{
		public PostJobValidator()
		{
            RuleFor(e => e.Name)
               .MinimumLength(3).WithMessage("Length of name must be greater than 3")
               .MaximumLength(200).WithMessage("Length of name must be smaller than 200");
            RuleFor(e => e.Information)
               .MinimumLength(10).WithMessage("Length of information must be greater than 10");
            RuleFor(e => e.InformationForApply)
                .MinimumLength(10).WithMessage("Length of information for apply must be greater than 10");
            RuleFor(e => e.Salary).Custom((s, ctx) =>
            {
                if (s<0)
                {
                    ctx.AddFailure("Salary must be greater than 0");
                }
            });
            RuleFor(e => e.Deadline).Custom((s, ctx) =>
            {
                if (s < DateTime.Now)
                {
                    ctx.AddFailure($"Deadline must be greater than {DateTime.Now}");
                }
            });
        }
	}
}

