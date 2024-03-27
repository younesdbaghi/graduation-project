using MediatR;
using Referentiel.Application.Project.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Project.Commands.Handlers
{
    public class AddProjectHandler : IRequestHandler<AddProjectCommand, ProjectEntity>
    {
        private readonly IProjectRepository _projectRepository;

        public AddProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectEntity> Handle(AddProjectCommand command, CancellationToken cancellationToken)
        {
            var project = new ProjectEntity()
            {
                Id = command.Id,
                ProjectName = command.ProjectName,
                HeuresTotal = command.HeuresTotal,
            };

            return await _projectRepository.AddAsync(project);
        }
    }
}
