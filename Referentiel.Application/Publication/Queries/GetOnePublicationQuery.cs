using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.Publication.Queries
{
    public class GetOnePublicationQuery : IRequest<PublicationEntity>
    {
        public int Id { get; set; }

        public GetOnePublicationQuery(int PublicationId)
        {
            this.Id = PublicationId;
        }
    }
}
