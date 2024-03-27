using MediatR;
using Referentiel.Application.ProjectStatistic.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectStatistic.Queries.Handlers
{
    internal class GetOneProjectStatisticHandler : IRequestHandler<GetOneProjectStatisticQuery, ProjectStatisticEntity>
    {
        private readonly IProjectStatisticRepository _projectstatisticRepository;

        public GetOneProjectStatisticHandler(IProjectStatisticRepository projectstatisticRepository)
        {
            _projectstatisticRepository = projectstatisticRepository;
        }

        public async Task<ProjectStatisticEntity> Handle(GetOneProjectStatisticQuery query, CancellationToken cancellationToken)
        {
            return await _projectstatisticRepository.GetByIdAsync(query.Id);
        }
    }
}
