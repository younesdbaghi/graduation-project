using MediatR;
using Referentiel.Application.User.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories;
using Referentiel.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Referentiel.Application.UserWeek.Queries.Handlers
{
    internal class GetAllUsersByIdHandler : IRequestHandler<GetAllUserWeeksByUserIdQuery, List<UserWeekEntity>>
    {
        private readonly IUserWeekRepository _userWeekRepository;

        public GetAllUsersByIdHandler(IUserWeekRepository userWeekRepository)
        {
            _userWeekRepository = userWeekRepository;
        }

        public async Task<List<UserWeekEntity>> Handle(GetAllUserWeeksByUserIdQuery query, CancellationToken cancellationToken)
        {
            var weeks = await _userWeekRepository.GetWeeksByUserIdAsync(query.UserId);
            return weeks.OrderByDescending(week => week.Id).ToList();
        }

    }

}
