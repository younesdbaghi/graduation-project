using MediatR;
using Referentiel.Application.User.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.User.Queries.Handlers
{
    internal class GetUsersByUsernameHandler : IRequestHandler<GetUsersByUsernameQuery, List<UserEntity>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByUsernameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserEntity>> Handle(GetUsersByUsernameQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByUsernameAsync(query.Username);
            return users.OrderByDescending(u => u.Id).ToList();
        }

    }
}
