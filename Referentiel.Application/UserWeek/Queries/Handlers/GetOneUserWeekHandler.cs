using MediatR;
using Referentiel.Application.UserWeek.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeek.Queries.Handlers
{
    internal class GetOneUserWeekHandler : IRequestHandler<GetOneUserWeekQuery, UserWeekEntity>
    {
        private readonly IUserWeekRepository _userweekRepository;

        public GetOneUserWeekHandler(IUserWeekRepository userweekRepository)
        {
            _userweekRepository = userweekRepository;
        }

        public async Task<UserWeekEntity> Handle(GetOneUserWeekQuery query, CancellationToken cancellationToken)
        {
            return await _userweekRepository.GetSpecWeekForSpecUser(query.IdUser, query.IdWeek);
        }
    }
}
