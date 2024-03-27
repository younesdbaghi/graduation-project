using MediatR;
using Referentiel.Application.ProjectQuotation.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectQuotation.Commands.Handlers
{
    public class UpdateProjectQuotationHandler : IRequestHandler<UpdateProjectQuotationCommand, int>
    {
        private readonly IProjectQuotationRepository _projectquotationRepository;

        public UpdateProjectQuotationHandler(IProjectQuotationRepository projectquotationRepository)
        {
            _projectquotationRepository = projectquotationRepository;
        }

        public async Task<int> Handle(UpdateProjectQuotationCommand command, CancellationToken cancellationToken)
        {
            var projectquotation = new ProjectQuotationEntity()
            {
                Id = command.Id,
                Quotation = command.Quotation,
                ProjectId = command.ProjectId,
            };

            return await _projectquotationRepository.UpdateAsync(projectquotation);
        }
    }
}
