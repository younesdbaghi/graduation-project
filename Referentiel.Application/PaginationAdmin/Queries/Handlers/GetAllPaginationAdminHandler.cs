using MediatR;
using Referentiel.Application.PaginationAdmin.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationAdmin.Queries.Handlers
{
    internal class GetAllPaginationAdminHandler : IRequestHandler<GetAllPaginationAdminQuery, List<PaginationAdminEntity>>
    {
        private readonly IPaginationAdminRepository _paginationadminRepository;

        public GetAllPaginationAdminHandler(IPaginationAdminRepository paginationadminRepository)
        {
            _paginationadminRepository = paginationadminRepository;
        }

        public async Task<List<PaginationAdminEntity>> Handle(GetAllPaginationAdminQuery query, CancellationToken cancellationToken)
        {
            return await _paginationadminRepository.GetAllAsync();
        }
    }
}
