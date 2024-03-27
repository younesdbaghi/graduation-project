using Referentiel.Domain.Entities;

namespace Referentiel.Application.User.Queries.Handlers
{
    internal interface IGetUsersByUsernameHandler
    {
        Task<List<UserEntity>> Handle(GetUsersByUsernameQuery query, CancellationToken cancellationToken);
    }
}