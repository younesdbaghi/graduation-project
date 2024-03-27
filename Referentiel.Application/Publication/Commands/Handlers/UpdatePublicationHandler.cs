using Commons.Exceptions;
using MediatR;
using Referentiel.Application.Publication.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Publication.Commands.Handlers
{
    public class UpdatePublicationHandler : IRequestHandler<UpdatePublicationCommand, int>
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUserRepository _userRepository;

        public UpdatePublicationHandler(IPublicationRepository publicationRepository, IUserRepository userRepository)
        {
            _publicationRepository = publicationRepository;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(UpdatePublicationCommand command, CancellationToken cancellationToken)
        {
            var user = await this._userRepository.GetByIdAsync(command.UserId);
            if(user.Admin == "YES" || user.Admin == "CDP")
            {
                var publication = new PublicationEntity()
                {
                    Id = command.Id,
                    Title = command.Title,
                    Description = command.Description,
                    Date = command.Date,
                    Heure = command.Heure,
                    UserId = command.UserId,
                };
                return await _publicationRepository.UpdateAsync(publication);
            }
            throw new ForbidenException();
        }
    }
}
