using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.User.Queries
{
    public class GetUsersByUsernameQuery : IRequest<List<UserEntity>>
    {
        public string Username { get; set; }

        public GetUsersByUsernameQuery(string UserName)
        {
            this.Username = UserName;
        }
    }
}
