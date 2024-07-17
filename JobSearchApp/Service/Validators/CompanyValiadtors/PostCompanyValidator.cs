using System;
using FluentValidation;
using Service.Dtos.CompanyDtos;
using Service.Helpers.Interfaces;

namespace Service.Validators.CompanyValiadtors
{
	public class PostCompanyValidator: AbstractValidator<PostCompanyDto>
    {
		public PostCompanyValidator(IFileService fileService)
		{
            RuleFor(e => e.Name)
               .MinimumLength(3).WithMessage("Length of name must be greater than 3")
               .MaximumLength(100).WithMessage("Length of name must be smaller than 100");

            RuleFor(e => e.About)
                .MinimumLength(10).WithMessage("Length of about must be greater than 10");


            RuleFor(e => e.Location)
                .MinimumLength(3).WithMessage("Length of location must be greater than 3");
            RuleFor(e => e.LocationIframe)
               .MinimumLength(10).WithMessage("Length of location iframe must be greater than 10");
            RuleFor(e => e.WebSiteUrl)
               .MinimumLength(5).WithMessage("Length of web site url must be greater than 5");
            RuleFor(e => e.Logo).Custom((l, ctx) =>
            {
                if (!fileService.IsImage(l))
                {
                    ctx.AddFailure("You can upload only image");
                }
                else if (!fileService.IsLengthSuit(l,1000))
                {
                    ctx.AddFailure("Image length must be smaller than 1mb");
                }
            });
        }
	}
}

