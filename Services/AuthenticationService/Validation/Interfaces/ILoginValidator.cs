using FluentValidation;
using Studfolio.AuthenticationService.Models.Dto.Requests;

namespace Studfolio.AuthenticationService.Validation.Interfaces
{
    public interface ILoginValidator : IValidator<LoginRequest>
    {
    }
}
