using MediatR;
using Referentiel.Application.ProjectQuotation.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectQuotation.Queries.Handlers
{
    internal class GetOneProjectQuotationHandler : IRequestHandler<GetOneProjectQuotationQuery, ProjectQuotationEntity>
    {
        private readonly IProjectQuotationRepository _projectquotationRepository;

        public GetOneProjectQuotationHandler(IProjectQuotationRepository projectquotationRepository)
        {
            _projectquotationRepository = projectquotationRepository;
        }

        public async Task<ProjectQuotationEntity> Handle(GetOneProjectQuotationQuery query, CancellationToken cancellationToken)
        {
            return await _projectquotationRepository.GetByIdAsync(query.Id);
        }
    }
}
