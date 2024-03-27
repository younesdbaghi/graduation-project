using MediatR;
using Referentiel.Application.ProjectQuotation.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectQuotation.Commands.Handlers
{
    public class AddProjectQuotationHandler : IRequestHandler<AddProjectQuotationCommand, ProjectQuotationEntity>
    {
        private readonly IProjectQuotationRepository _projectquotationRepository;

        public AddProjectQuotationHandler(IProjectQuotationRepository projectquotationRepository)
        {
            _projectquotationRepository = projectquotationRepository;
        }

        public async Task<ProjectQuotationEntity> Handle(AddProjectQuotationCommand command, CancellationToken cancellationToken)
        {
            var projectquotation = new ProjectQuotationEntity()
            {
                Id = command.Id,
                Quotation = command.Quotation,
                ProjectId = command.ProjectId,
            };

            return await _projectquotationRepository.AddAsync(projectquotation);
        }
    }
}
