using MediatR;
using Referentiel.Application.Project.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Project.Commands.Handlers
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var project = new ProjectEntity()
            {
                Id = command.Id,
                ProjectName = command.ProjectName,
                HeuresTotal = command.HeuresTotal,
            };

            return await _projectRepository.UpdateAsync(project);
        }
    }
}
