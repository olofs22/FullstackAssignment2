using FullstackAssignment2.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace FullstackAssignment2.Validators
{
    public class CreateCarValidator : AbstractValidator<CreateCarDTO>
    {
        public CreateCarValidator()
        {
            RuleFor(x => x.Make)
                .NotEmpty().WithMessage("Make is required.")
                .MaximumLength(20).WithMessage("Make cannot exceed 20 characters.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(30).WithMessage("Model cannot exceed 30 characters.");
        }
    }

    public class UpdateCarValidator : AbstractValidator<UpdateCarDTO>
    {
        public UpdateCarValidator()
        {
            {
                RuleFor(x => x.Make)
                    .NotEmpty().WithMessage("Make is required.")
                    .MaximumLength(20).WithMessage("Make cannot exceed 20 characters.");

                RuleFor(x => x.Model)
                    .NotEmpty().WithMessage("Model is required.")
                    .MaximumLength(30).WithMessage("Model cannot exceed 30 characters.");
            }
        }
    }
        
}
