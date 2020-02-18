using FluentValidation;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public class AddColumnCommandValidator : AbstractValidator<AddColumnCommand>
    {
        public AddColumnCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().MaximumLength(255);
        }
    }
}
