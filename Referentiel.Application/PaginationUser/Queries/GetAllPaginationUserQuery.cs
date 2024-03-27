using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.PaginationUser.Queries
{
    public class GetAllPaginationUserQuery : IRequest<List<PaginationUserEntity>>
    {

        public GetAllPaginationUserQuery()
        {
        }
    }
}
