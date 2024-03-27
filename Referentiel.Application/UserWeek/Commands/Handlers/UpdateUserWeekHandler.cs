using MediatR;
using Referentiel.Application.UserWeek.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeek.Commands.Handlers
{
    public class UpdateUserWeekHandler : IRequestHandler<UpdateUserWeekCommand, int>
    {
        private readonly IUserWeekRepository _userweekRepository;

        public UpdateUserWeekHandler(IUserWeekRepository userweekRepository)
        {
            _userweekRepository = userweekRepository;
        }

        public async Task<int> Handle(UpdateUserWeekCommand command, CancellationToken cancellationToken)
        {
            var userweek = new UserWeekEntity()
            {
                Id = command.Id,
                WeekNumber = command.WeekNumber,
                StatusWeek = command.StatusWeek,
                UserId = command.UserId,
            };

            return await _userweekRepository.UpdateAsync(userweek);
        }
    }
}
