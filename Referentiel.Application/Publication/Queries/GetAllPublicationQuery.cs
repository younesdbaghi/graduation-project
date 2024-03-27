using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.Publication.Queries
{
    public class GetAllPublicationQuery : IRequest<List<PublicationEntity>>
    {

        public GetAllPublicationQuery()
        {
        }
    }
}
