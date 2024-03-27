using MediatR;
using Referentiel.Application.Project.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Project.Queries.Handlers
{
    internal class GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, List<ProjectEntity>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectQuotationRepository _projectQuotationRepository;

        public GetAllProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectEntity>> Handle(GetAllProjectQuery query, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.OrderByDescending(p => p.Id).ToList();
        }
    }
}
