using MediatR;
using Referentiel.Application.Project.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Project.Commands.Handlers
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var entity= await _projectRepository.GetByIdAsync(command.Id);
            return await _projectRepository.DeleteAsync(entity);
        }
    }
}
