using System;
using FluentValidation;
using Service.Dtos.CategoryDtos;
using Service.Helpers;
using Service.Helpers.Interfaces;

namespace Service.Validators.CategoryValidators
{
	public class PostCategoryValidator: AbstractValidator<PostCategoryDto>
    {
		public PostCategoryValidator(IFileService fileService)
		{
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Length of name must be greater than 3")
                .MaximumLength(100).WithMessage("Length of name must be smaller than 100");
            RuleFor(e => e.Image).Custom((l, ctx) =>
            {
                if (!fileService.IsImage(l))
                {
                    ctx.AddFailure("You can upload only image");
                }
                else if (!fileService.IsLengthSuit(l, 1000))
                {
                    ctx.AddFailure("Image length must be smaller than 1mb");
                }
            });
        }
	}
}

