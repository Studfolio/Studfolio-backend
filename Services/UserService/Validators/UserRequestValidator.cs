using FluentValidation;
using Studfolio.UserService.Models;

namespace Studfolio.UserService.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .MaximumLength(32).WithMessage("First name is too long.")
                .Matches("^[A-Z][a-z]+$|^[А-ЯЁ][а-яё]+$").WithMessage("First name with error.");

            RuleFor(user => user.LastName)
                .NotEmpty()
                .MaximumLength(32).WithMessage("Last name is too long.")
                .Matches("^[A-Z][a-z]+$|^[А-ЯЁ][а-яё]+$").WithMessage("Last name with error.");

            RuleFor(user => user.MiddleName)
                .MaximumLength(32).WithMessage("Middle name is too long.")
                .Matches("^[A-Z][a-z]+$|^[А-ЯЁ][а-яё]+$").WithMessage("Middle name with error.");

            RuleFor(user => user.Email)
                .MaximumLength(254).WithMessage("Email is too long.")
                .EmailAddress().WithMessage("Email is invalid.");

            RuleFor(user => user.Status)
                .MaximumLength(300).WithMessage("Status is too long.");

            RuleFor(user => user.Password)
                .MinimumLength(8).WithMessage("Password is too short.");
        }
    }
}
