using MediatR;
using Referentiel.Application.Publication.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Publication.Queries.Handlers
{
    internal class GetAllPublicationHandler : IRequestHandler<GetAllPublicationQuery, List<PublicationEntity>>
    {
        private readonly IPublicationRepository _publicationRepository;

        public GetAllPublicationHandler(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<List<PublicationEntity>> Handle(GetAllPublicationQuery query, CancellationToken cancellationToken)
        {
            var publications = await _publicationRepository.GetAllAsync();
            return publications.OrderByDescending(p => p.Id).ToList();
        }
    }
}
