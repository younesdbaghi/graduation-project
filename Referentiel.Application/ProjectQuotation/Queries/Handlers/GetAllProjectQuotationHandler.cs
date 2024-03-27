using MediatR;
using Referentiel.Application.ProjectQuotation.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.ProjectQuotation.Queries.Handlers
{
    internal class GetAllProjectQuotationHandler : IRequestHandler<GetAllProjectQuotationQuery, List<ProjectQuotationEntity>>
    {
        private readonly IProjectQuotationRepository _projectquotationRepository;

        public GetAllProjectQuotationHandler(IProjectQuotationRepository projectquotationRepository)
        {
            _projectquotationRepository = projectquotationRepository;
        }

        public async Task<List<ProjectQuotationEntity>> Handle(GetAllProjectQuotationQuery query, CancellationToken cancellationToken)
        {
            var projectQuotations = await _projectquotationRepository.GetAllAsync();
            return projectQuotations.OrderByDescending(pq => pq.Id).ToList();
        }
    }
}
