using MediatR;
using Referentiel.Application.UserWeek.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeek.Queries.Handlers
{
    internal class GetAllUserWeekHandler : IRequestHandler<GetAllUserWeekQuery, List<UserWeekEntity>>
    {
        private readonly IUserWeekRepository _userweekRepository;

        public GetAllUserWeekHandler(IUserWeekRepository userweekRepository)
        {
            _userweekRepository = userweekRepository;
        }

        public async Task<List<UserWeekEntity>> Handle(GetAllUserWeekQuery query, CancellationToken cancellationToken)
        {
            var weeks = await _userweekRepository.GetAllAsync();
            return weeks.OrderByDescending(week => week.Id).ToList();
        }
    }
}
