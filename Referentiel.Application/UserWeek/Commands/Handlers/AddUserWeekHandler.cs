using MediatR;
using Referentiel.Application.UserWeek.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeek.Commands.Handlers
{
    public class AddUserWeekHandler : IRequestHandler<AddUserWeekCommand, UserWeekEntity>
    {
        private readonly IUserWeekRepository _userweekRepository;

        public AddUserWeekHandler(IUserWeekRepository userweekRepository)
        {
            _userweekRepository = userweekRepository;
        }

        public async Task<UserWeekEntity> Handle(AddUserWeekCommand command, CancellationToken cancellationToken)
        {
            var userweek = new UserWeekEntity()
            {
                Id = command.Id,
                WeekNumber = command.WeekNumber,
                StatusWeek = command.StatusWeek,
                UserId = command.UserId,
            };

            return await _userweekRepository.AddAsync(userweek);
        }
    }
}
