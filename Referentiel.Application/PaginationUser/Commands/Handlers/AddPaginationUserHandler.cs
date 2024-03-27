using MediatR;
using Referentiel.Application.PaginationUser.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationUser.Commands.Handlers
{
    public class AddPaginationUserHandler : IRequestHandler<AddPaginationUserCommand, PaginationUserEntity>
    {
        private readonly IPaginationUserRepository _paginationuserRepository;

        public AddPaginationUserHandler(IPaginationUserRepository paginationuserRepository)
        {
            _paginationuserRepository = paginationuserRepository;
        }

        public async Task<PaginationUserEntity> Handle(AddPaginationUserCommand command, CancellationToken cancellationToken)
        {
            var paginationuser = new PaginationUserEntity()
            {
                Id = command.Id,
                PagUserWeeks = command.PagUserWeeks,
                PagUserPublications = command.PagUserPublications,
                UserId = command.UserId,
            };

            return await _paginationuserRepository.AddAsync(paginationuser);
        }
    }
}
