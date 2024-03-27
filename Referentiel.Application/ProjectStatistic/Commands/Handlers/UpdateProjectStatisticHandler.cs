using MediatR;
using Referentiel.Application.ProjectStatistic.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectStatistic.Commands.Handlers
{
    public class UpdateProjectStatisticHandler : IRequestHandler<UpdateProjectStatisticCommand, int>
    {
        private readonly IProjectStatisticRepository _projectstatisticRepository;

        public UpdateProjectStatisticHandler(IProjectStatisticRepository projectstatisticRepository)
        {
            _projectstatisticRepository = projectstatisticRepository;
        }

        public async Task<int> Handle(UpdateProjectStatisticCommand command, CancellationToken cancellationToken)
        {
            var projectstatistic = new ProjectStatisticEntity()
            {
                Id = command.Id,
                Progress = command.Progress,
                ProjectId = command.ProjectId,
            };

            return await _projectstatisticRepository.UpdateAsync(projectstatistic);
        }
    }
}
