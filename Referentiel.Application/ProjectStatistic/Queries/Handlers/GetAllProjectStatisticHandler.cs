using MediatR;
using Referentiel.Application.ProjectStatistic.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectStatistic.Queries.Handlers
{
    internal class GetAllProjectStatisticHandler : IRequestHandler<GetAllProjectStatisticQuery, List<ProjectStatisticEntity>>
    {
        private readonly IProjectStatisticRepository _projectstatisticRepository;

        public GetAllProjectStatisticHandler(IProjectStatisticRepository projectstatisticRepository)
        {
            _projectstatisticRepository = projectstatisticRepository;
        }

        public async Task<List<ProjectStatisticEntity>> Handle(GetAllProjectStatisticQuery query, CancellationToken cancellationToken)
        {
            return await _projectstatisticRepository.GetAllAsync();
        }
    }
}
