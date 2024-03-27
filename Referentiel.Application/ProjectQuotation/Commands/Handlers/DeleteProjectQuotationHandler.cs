using MediatR;
using Referentiel.Application.ProjectQuotation.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectQuotation.Commands.Handlers
{
    public class DeleteProjectQuotationHandler : IRequestHandler<DeleteProjectQuotationCommand, int>
    {
        private readonly IProjectQuotationRepository _projectquotationRepository;

        public DeleteProjectQuotationHandler(IProjectQuotationRepository projectquotationRepository)
        {
            _projectquotationRepository = projectquotationRepository;
        }

        public async Task<int> Handle(DeleteProjectQuotationCommand command, CancellationToken cancellationToken)
        {
            var entity= await _projectquotationRepository.GetByIdAsync(command.Id);
            return await _projectquotationRepository.DeleteAsync(entity);
        }
    }
}
