using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.PaginationAdmin.Queries
{
    public class GetOnePaginationAdminQuery : IRequest<PaginationAdminEntity>
    {
        public int Id { get; set; }

        public GetOnePaginationAdminQuery(int PaginationAdminId)
        {
            this.Id = PaginationAdminId;
        }
    }
}
