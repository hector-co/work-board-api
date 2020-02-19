using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkBoard.Commands.CardCommands;
using WorkBoard.Dtos;
using WorkBoard.DataAccess.Ef.BoardColumnDataAccess;
using WorkBoard.DataAccess.Ef.BoardDataAccess;
using WorkBoard.Commands.Exceptions;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Commands
{
    public class AddCardCommandHandler : IAddCardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public AddCardCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            var boardDto = await _context.Set<BoardDtoDataAccess>().FindAsync(request.BoardId);
            if (boardDto == null || boardDto.State == BoardState.Closed) throw new CommandException();

            var columnDto = request.ColumnId.HasValue
                ? _context.Set<BoardColumnDtoDataAccess>().First(c => c.Id == request.ColumnId.Value)
                : _context.Set<BoardColumnDtoDataAccess>().Where(c => c.BoardDataAccess.Id == request.BoardId)
                    .OrderBy(c => c.Order).FirstOrDefault();

            var maxOrder = 0;
            if (_context.Set<CardDtoDataAccess>().Any(c => c.ColumnDataAccess.Id == request.ColumnId))
            {
                maxOrder = _context.Set<CardDtoDataAccess>().Where(c => c.ColumnDataAccess.Id == request.ColumnId).Max(c => c.Order);
            }

            var cardDto = new CardDtoDataAccess
            {
                Title = request.Title,
                Description = request.Description,
                BoardDataAccess = _context.Set<BoardDtoDataAccess>().First(b => b.Id == request.BoardId),
                ColumnDataAccess = columnDto,
                Color = request.Color,
                EstimatedPoints = request.EstimatedPoints,
                Priority = (CardPriority)request.Priority,
                Order = maxOrder + 1,
                Version = 1,
                Guid = Guid.NewGuid()
            };
            _context.Set<CardDtoDataAccess>().Add(cardDto);

            await _context.SaveChangesAsync(cancellationToken);

            return cardDto.Id;
        }
    }
}
