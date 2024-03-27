using Commons.Exceptions;
using MediatR;
using Referentiel.Application.Publication.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.Publication.Commands.Handlers
{
    public class AddPublicationHandler : IRequestHandler<AddPublicationCommand, PublicationEntity>
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUserRepository _userRepository;


        public AddPublicationHandler(IPublicationRepository publicationRepository, IUserRepository userRepository)
        {
            _publicationRepository = publicationRepository;
            _userRepository = userRepository;
        }

        public async Task<PublicationEntity> Handle(AddPublicationCommand command, CancellationToken cancellationToken)
        {
            var user = await this._userRepository.GetByIdAsync(command.UserId);
            if(user.Admin=="YES" || user.Admin == "CDP") 
            {
                command.Date = DateTime.Now;
                command.Heure = command.Date.ToString("HH:mm");
                var publication = new PublicationEntity()
                {
                    Id = command.Id,
                    Title = command.Title,
                    Description = command.Description,
                    Date = command.Date,
                    Heure = command.Heure,
                    UserId = command.UserId,
                };

                return await _publicationRepository.AddAsync(publication);
            }
            throw new ForbidenException();
        }
    }
}
