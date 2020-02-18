using FluentValidation;

namespace WorkBoard.Commands.CardCommands
{
    public class EditCardCommandValidator : AbstractValidator<EditCardCommand>
    {
        public EditCardCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().MaximumLength(255);
            RuleFor(c => c.Priority).InclusiveBetween(0, 2);
            RuleFor(c => c.EstimatedPoints).GreaterThanOrEqualTo(0);
        }
    }
}
