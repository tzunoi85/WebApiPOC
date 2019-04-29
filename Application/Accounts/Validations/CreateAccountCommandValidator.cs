using Application.Accounts.Messages.Commands;
using FluentValidation;


namespace Application.Accounts.Validations
{
    public class CreateAccountCommandValidator
        : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            this.RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("Email should not be null!");

            this.RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Password should not be null!");
        }
    }
}
