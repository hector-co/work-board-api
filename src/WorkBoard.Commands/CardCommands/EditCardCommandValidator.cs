using FluentValidation;

namespace WorkBoard.Commands.CardCommands
{
    public class AddCardCommandValidator : AbstractValidator<AddCardCommand>
    {
        public AddCardCommandValidator()
        {
            RuleFor(c => c.BoardId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty().MaximumLength(255);
            RuleFor(c => c.Priority).InclusiveBetween(0, 2);
            RuleFor(c => c.EstimatedPoints).GreaterThan(0);
        }
    }
}
