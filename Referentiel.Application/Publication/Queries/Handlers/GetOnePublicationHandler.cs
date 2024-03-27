using MediatR;
using Referentiel.Application.Publication.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Publication.Queries.Handlers
{
    internal class GetOnePublicationHandler : IRequestHandler<GetOnePublicationQuery, PublicationEntity>
    {
        private readonly IPublicationRepository _publicationRepository;

        public GetOnePublicationHandler(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<PublicationEntity> Handle(GetOnePublicationQuery query, CancellationToken cancellationToken)
        {
            return await _publicationRepository.GetByIdAsync(query.Id);
        }
    }
}
