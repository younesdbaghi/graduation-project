using MediatR;
using Referentiel.Application.UserWeekProject.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeekProject.Queries.Handlers
{
    internal class GetOneUserWeekProjectHandler : IRequestHandler<GetOneUserWeekProjectQuery, UserWeekProjectEntity>
    {
        private readonly IUserWeekProjectRepository _userweekprojectRepository;

        public GetOneUserWeekProjectHandler(IUserWeekProjectRepository userweekprojectRepository)
        {
            _userweekprojectRepository = userweekprojectRepository;
        }

        public async Task<UserWeekProjectEntity> Handle(GetOneUserWeekProjectQuery query, CancellationToken cancellationToken)
        {
            return await _userweekprojectRepository.GetByIdAsync(query.Id);
        }
    }
}
