using FluentValidation;
using BackEnd.Models.ViewModels;
using BackEnd.Utils;

namespace BackEnd.Validations
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto> 
    {
        public CreateCategoryValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name must not empty");

            RuleFor(c => c.DisplayName)
                .NotEmpty().WithMessage("Display Name must not empty");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description must not empty")
                .MinimumLength(ConstantVariable.MINIMUM_CATEGORY_DESCRIPTION_LENGTH).WithMessage($"Description must have greater than {ConstantVariable.MINIMUM_CATEGORY_DESCRIPTION_LENGTH} letters");
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<CategoryDetailDto> 
    {
        public UpdateCategoryValidator() 
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("Id must not null")
                .GreaterThan(ConstantVariable.MINIMUM_CATEGORY_ID).WithMessage("Invalid category id");
                
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name must not empty");

            RuleFor(c => c.DisplayName)
                .NotEmpty().WithMessage("Display Name must not empty");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description must not empty")
                .MinimumLength(ConstantVariable.MINIMUM_CATEGORY_DESCRIPTION_LENGTH).WithMessage("Description must have greater than 10 letters");
        }
    }
}