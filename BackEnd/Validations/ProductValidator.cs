using BackEnd.Models.ViewModels;
using BackEnd.Utils;
using FluentValidation;

namespace BackEnd.Validations
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name must not empty");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description must not empty")
                .MinimumLength(ConstantVariable.MINIMUM_PRODUCT_DESCRIPTION_LENGTH).WithMessage($"Description must have greater than {ConstantVariable.MINIMUM_PRODUCT_DESCRIPTION_LENGTH} letters");

            RuleFor(p => p.Prices)
                .NotEmpty().WithMessage("Prices must not empty")
                .GreaterThan(0).WithMessage("Prices is not valid");

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("Category Id must not empty");

            RuleFor(p => p.IsFeatured)
                .NotEmpty().WithMessage("IsFeatured must not empty");

            RuleFor(p => p.Images)
                .NotEmpty().WithMessage("Images must not empty");
        }
    }

    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Id must not empty");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name must not empty");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description must not empty")
                .MinimumLength(ConstantVariable.MINIMUM_PRODUCT_DESCRIPTION_LENGTH).WithMessage($"Description must have greater than {ConstantVariable.MINIMUM_PRODUCT_DESCRIPTION_LENGTH} letters");

            RuleFor(p => p.Prices)
                .NotEmpty().WithMessage("Prices must not empty")
                .GreaterThan(0).WithMessage("Prices is not valid");

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("Category Id must not empty");

            RuleFor(p => p.IsFeatured)
                .NotEmpty().WithMessage("IsFeatured must not empty");
        }
    }
}