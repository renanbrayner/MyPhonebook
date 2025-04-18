using FluentValidation;
using Phonebook.API.DTO;
// Não é DRY, mas não quis fazer um helper para isso e acaba desacoplando o validator

namespace Phonebook.API.Validators
{
    public class EditContactDTOValidator : AbstractValidator<EditContactDTO>
    {
        public EditContactDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail não é válido.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
                    .WithMessage("O telefone deve seguir o formato (99) 9999-9999 ou (99) 99999-9999.");
        }
    }
}
