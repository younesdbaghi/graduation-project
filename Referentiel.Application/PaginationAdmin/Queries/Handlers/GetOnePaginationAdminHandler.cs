using MediatR;
using Referentiel.Application.PaginationAdmin.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationAdmin.Queries.Handlers
{
    internal class GetOnePaginationAdminHandler : IRequestHandler<GetOnePaginationAdminQuery, PaginationAdminEntity>
    {
        private readonly IPaginationAdminRepository _paginationadminRepository;

        public GetOnePaginationAdminHandler(IPaginationAdminRepository paginationadminRepository)
        {
            _paginationadminRepository = paginationadminRepository;
        }

        public async Task<PaginationAdminEntity> Handle(GetOnePaginationAdminQuery query, CancellationToken cancellationToken)
        {
            return await _paginationadminRepository.GetByIdAsync(query.Id);
        }
    }
}
