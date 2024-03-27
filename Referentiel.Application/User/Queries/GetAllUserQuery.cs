using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.User.Queries
{
    public class GetAllUserQuery : IRequest<List<UserEntity>>
    {

        public GetAllUserQuery()
        {
        }
    }
}
