using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.User.Queries
{
    public class GetOneUserQuery : IRequest<UserEntity>
    {
        public int Id { get; set; }

        public GetOneUserQuery(int UserId)
        {
            this.Id = UserId;
        }
    }
}
