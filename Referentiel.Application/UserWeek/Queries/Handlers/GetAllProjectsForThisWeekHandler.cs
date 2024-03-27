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
    internal class GetAllProjectsForThisWeekHandler : IRequestHandler<GetAllProjectsForThisWeekQuery, List<UserWeekProjectEntity>>
    {
        private readonly IUserWeekRepository _userWeekRepository;

        public GetAllProjectsForThisWeekHandler(IUserWeekRepository userWeekRepository)
        {
            _userWeekRepository = userWeekRepository;
        }

        public async Task<List<UserWeekProjectEntity>> Handle(GetAllProjectsForThisWeekQuery query, CancellationToken cancellationToken)
        {
            var projects = await _userWeekRepository.GetAllProjectsForThisWeek(query.UserId, query.WeekId);
            return projects.OrderByDescending(p => p.Id).ToList();
        }
    }

}
