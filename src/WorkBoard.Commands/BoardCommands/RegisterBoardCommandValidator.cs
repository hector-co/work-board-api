using FluentValidation;

namespace WorkBoard.Commands.BoardCommands
{
    public class RegisterBoardCommandValidator : AbstractValidator<RegisterBoardCommand>
    {
        public RegisterBoardCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().MaximumLength(255);
        }
    }
}
