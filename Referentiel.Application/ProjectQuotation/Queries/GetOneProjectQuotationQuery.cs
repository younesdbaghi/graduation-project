using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.ProjectQuotation.Queries
{
    public class GetOneProjectQuotationQuery : IRequest<ProjectQuotationEntity>
    {
        public int Id { get; set; }

        public GetOneProjectQuotationQuery(int ProjectQuotationId)
        {
            this.Id = ProjectQuotationId;
        }
    }
}
