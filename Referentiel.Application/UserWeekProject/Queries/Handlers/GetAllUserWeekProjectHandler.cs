using MediatR;
using Referentiel.Application.UserWeekProject.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeekProject.Queries.Handlers
{
    internal class GetAllUserWeekProjectHandler : IRequestHandler<GetAllUserWeekProjectQuery, List<UserWeekProjectEntity>>
    {
        private readonly IUserWeekProjectRepository _userweekprojectRepository;

        public GetAllUserWeekProjectHandler(IUserWeekProjectRepository userweekprojectRepository)
        {
            _userweekprojectRepository = userweekprojectRepository;
        }

        public async Task<List<UserWeekProjectEntity>> Handle(GetAllUserWeekProjectQuery query, CancellationToken cancellationToken)
        {
            var userWeekProjects = await _userweekprojectRepository.GetAllAsync();
            return userWeekProjects.OrderByDescending(uwp => uwp.Id).ToList();
        }
    }
}
