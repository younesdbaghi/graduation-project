using MediatR;
using Referentiel.Application.User.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.User.Commands.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, UserEntity>
    {
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var user = new UserEntity()
            {
                Id = command.Id,
                Name = command.Name,
                LastName = command.LastName,
                Username = command.Username,
                Email = command.Email,
                Password = command.Password,
                Activity = command.Activity,
                Admin = command.Admin,
            };

            return await _userRepository.AddAsync(user);
        }
    }
}
