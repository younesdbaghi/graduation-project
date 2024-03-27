using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.ProjectQuotation.Queries
{
    public class GetAllProjectQuotationQuery : IRequest<List<ProjectQuotationEntity>>
    {

        public GetAllProjectQuotationQuery()
        {
        }
    }
}
