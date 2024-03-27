using MediatR;
using Referentiel.Application.Project.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Project.Queries.Handlers
{
    internal class GetOneProjectHandler : IRequestHandler<GetOneProjectQuery, ProjectEntity>
    {
        private readonly IProjectRepository _projectRepository;

        public GetOneProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectEntity> Handle(GetOneProjectQuery query, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetByIdAsync(query.Id);
        }
    }
}
