using FluentValidation;
using Shared.Clients;

namespace BackEnd.Validations
{
    public class ClientRegisterValidator : AbstractValidator<ClientRegisterDto>
    {
        public ClientRegisterValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Username must not empty");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email must not empty")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password must not empty");
        }
    }

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Username must not empty");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password must not empty");
        }
    }
}