using FluentValidation;
using Studfolio.AuthenticationService.Models.Dto.Requests;
using Studfolio.AuthenticationService.Validation.Interfaces;

namespace Studfolio.AuthenticationService.Validation
{
    public class LoginValidator : AbstractValidator<LoginRequest>, ILoginValidator
    {
        public LoginValidator()
        {
            RuleFor(user => user.LoginData)
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotEmpty();
        }
    }
}