﻿using System;
using FluentValidation;
using Service.Dtos.EmploymentTypeDtos;

namespace Service.Validators.EmploymentTypeValidators
{
	public class PostEmploymentTypeValidator: AbstractValidator<PostEmploymentTypeDto>
    {
		public PostEmploymentTypeValidator()
		{
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Length of name must be greater than 3")
                .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
        }
	}
}

