using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.PaginationAdmin.Queries
{
    public class GetAllPaginationAdminQuery : IRequest<List<PaginationAdminEntity>>
    {

        public GetAllPaginationAdminQuery()
        {
        }
    }
}
