using Commons.Exceptions;
using MediatR;
using Referentiel.Application.Publication.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Publication.Commands.Handlers
{
    public class DeletePublicationHandler : IRequestHandler<DeletePublicationCommand, int>
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUserRepository _userRepository;

        public DeletePublicationHandler(IPublicationRepository publicationRepository, IUserRepository userRepository)
        {
            _publicationRepository = publicationRepository;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(DeletePublicationCommand command, CancellationToken cancellationToken)
        {
            var entity= await _publicationRepository.GetByIdAsync(command.Id);
            var user = await _userRepository.GetByIdAsync(command.IdUser);
            if (user.Admin == "YES" || user.Admin == "CDP")
            {
                return await _publicationRepository.DeleteAsync(entity);
            }
            throw new ForbidenException();
        }
    }
}
