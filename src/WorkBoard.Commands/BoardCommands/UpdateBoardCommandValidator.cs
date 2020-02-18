using FluentValidation;

namespace WorkBoard.Commands.BoardCommands
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().MaximumLength(255);
        }
    }
}
