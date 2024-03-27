using MediatR;
using Referentiel.Application.PaginationUser.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationUser.Queries.Handlers
{
    internal class GetAllPaginationUserHandler : IRequestHandler<GetAllPaginationUserQuery, List<PaginationUserEntity>>
    {
        private readonly IPaginationUserRepository _paginationuserRepository;

        public GetAllPaginationUserHandler(IPaginationUserRepository paginationuserRepository)
        {
            _paginationuserRepository = paginationuserRepository;
        }

        public async Task<List<PaginationUserEntity>> Handle(GetAllPaginationUserQuery query, CancellationToken cancellationToken)
        {
            return await _paginationuserRepository.GetAllAsync();
        }
    }
}
