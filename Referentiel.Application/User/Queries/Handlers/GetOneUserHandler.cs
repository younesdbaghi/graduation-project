using MediatR;
using Referentiel.Application.User.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.User.Queries.Handlers
{
    internal class GetOneUserHandler : IRequestHandler<GetOneUserQuery, UserEntity>
    {
        private readonly IUserRepository _userRepository;

        public GetOneUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Handle(GetOneUserQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(query.Id);
        }
    }
}
