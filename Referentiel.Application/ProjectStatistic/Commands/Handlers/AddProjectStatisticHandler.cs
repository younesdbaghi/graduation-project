using MediatR;
using Referentiel.Application.ProjectStatistic.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectStatistic.Commands.Handlers
{
    public class AddProjectStatisticHandler : IRequestHandler<AddProjectStatisticCommand, ProjectStatisticEntity>
    {
        private readonly IProjectStatisticRepository _projectstatisticRepository;

        public AddProjectStatisticHandler(IProjectStatisticRepository projectstatisticRepository)
        {
            _projectstatisticRepository = projectstatisticRepository;
        }

        public async Task<ProjectStatisticEntity> Handle(AddProjectStatisticCommand command, CancellationToken cancellationToken)
        {
            var projectstatistic = new ProjectStatisticEntity()
            {
                Id = command.Id,
                Progress = command.Progress,
                ProjectId = command.ProjectId,
            };

            return await _projectstatisticRepository.AddAsync(projectstatistic);
        }
    }
}
