using MediatR;
using Referentiel.Application.ProjectStatistic.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectStatistic.Commands.Handlers
{
    public class DeleteProjectStatisticHandler : IRequestHandler<DeleteProjectStatisticCommand, int>
    {
        private readonly IProjectStatisticRepository _projectstatisticRepository;

        public DeleteProjectStatisticHandler(IProjectStatisticRepository projectstatisticRepository)
        {
            _projectstatisticRepository = projectstatisticRepository;
        }

        public async Task<int> Handle(DeleteProjectStatisticCommand command, CancellationToken cancellationToken)
        {
            var entity= await _projectstatisticRepository.GetByIdAsync(command.Id);
            return await _projectstatisticRepository.DeleteAsync(entity);
        }
    }
}
