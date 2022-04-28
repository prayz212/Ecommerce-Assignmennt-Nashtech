using FluentValidation;
using BackEnd.Models.ViewModels;

namespace BackEnd.Validations
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto> 
    {
        public CreateCategoryValidator() 
        {
            RuleFor(c => c.name)
                .NotEmpty().WithMessage("Name must not empty");

            RuleFor(c => c.displayName)
                .NotEmpty().WithMessage("Display Name must not empty");

            RuleFor(c => c.description)
                .NotEmpty().WithMessage("Description must not empty")
                .MinimumLength(10).WithMessage("Description must have greater than 10 letters");
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<CategoryDetailDto> 
    {
        public UpdateCategoryValidator() 
        {
            RuleFor(c => c.id)
                .NotNull().WithMessage("Id must not null")
                .GreaterThan(0).WithMessage("Invalid category id");
                
            RuleFor(c => c.name)
                .NotEmpty().WithMessage("Name must not empty");

            RuleFor(c => c.displayName)
                .NotEmpty().WithMessage("Display Name must not empty");

            RuleFor(c => c.description)
                .NotEmpty().WithMessage("Description must not empty")
                .MinimumLength(10).WithMessage("Description must have greater than 10 letters");
        }
    }
}