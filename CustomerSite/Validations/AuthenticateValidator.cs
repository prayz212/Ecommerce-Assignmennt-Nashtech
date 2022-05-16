using System.Linq;
using FluentValidation;
using Shared.Clients;

namespace CustomerSite.Validations
{
    public class ClientRegisterValidator : AbstractValidator<ClientRegisterDto>
    {
        public ClientRegisterValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Tên tài khoản không được để trống");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 8 ký tự");

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("Mật khẩu xác nhận không được để trống")
                .Equal(r => r.Password).WithMessage("Mật khẩu xác nhận không trùng khớp");
        }
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