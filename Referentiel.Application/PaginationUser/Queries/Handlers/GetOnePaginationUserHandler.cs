using MediatR;
using Referentiel.Application.PaginationUser.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationUser.Queries.Handlers
{
    internal class GetOnePaginationUserHandler : IRequestHandler<GetOnePaginationUserQuery, PaginationUserEntity>
    {
        private readonly IPaginationUserRepository _paginationuserRepository;

        public GetOnePaginationUserHandler(IPaginationUserRepository paginationuserRepository)
        {
            _paginationuserRepository = paginationuserRepository;
        }

        public async Task<PaginationUserEntity> Handle(GetOnePaginationUserQuery query, CancellationToken cancellationToken)
        {
            return await _paginationuserRepository.GetByIdAsync(query.Id);
        }
    }
}
