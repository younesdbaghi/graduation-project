using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.PaginationUser.Queries
{
    public class GetOnePaginationUserQuery : IRequest<PaginationUserEntity>
    {
        public int Id { get; set; }

        public GetOnePaginationUserQuery(int PaginationUserId)
        {
            this.Id = PaginationUserId;
        }
    }
}
