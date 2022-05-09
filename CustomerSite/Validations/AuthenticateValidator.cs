using FluentValidation;
using Shared.Clients;

namespace CustomerSite.Validations
{
    public class ClientRegisterValidator : AbstractValidator<ClientRegisterDto>
    {
        // public ClientRegisterValidator()
        // {
        //     RuleFor(r => r.UserName)
        //         .NotEmpty().WithMessage("Username must not empty");

        //     RuleFor(r => r.Email)
        //         .NotEmpty().WithMessage("Email must not empty")
        //         .EmailAddress().WithMessage("Invalid email address");

        //     RuleFor(r => r.Password)
        //         .NotEmpty().WithMessage("Password must not empty");
        // }
    }

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Tên tài khoản không được để trống")
                .MinimumLength(4).WithMessage("Tên tài khoản phải có tối thiểu 4 ký tự");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(8).WithMessage("Mật khẩu phải có tối thiểu 8 ký tự");
        }
    }
}