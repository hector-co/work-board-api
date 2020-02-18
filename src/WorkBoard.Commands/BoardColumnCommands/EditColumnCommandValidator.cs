using FluentValidation;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public class EditColumnCommandValidator : AbstractValidator<EditColumnCommand>
    {
        public EditColumnCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().MaximumLength(255);
        }
    }
}
