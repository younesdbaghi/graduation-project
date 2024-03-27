using MediatR;
using Referentiel.Application.User.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.User.Queries.Handlers
{
    internal class GetAllUserHandler : IRequestHandler<GetAllUserQuery, List<UserEntity>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserEntity>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return users.OrderByDescending(u => u.Id).ToList();
        }
    }
}
