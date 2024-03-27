using MediatR;
using Referentiel.Application.PaginationUser.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationUser.Commands.Handlers
{
    public class UpdatePaginationUserHandler : IRequestHandler<UpdatePaginationUserCommand, int>
    {
        private readonly IPaginationUserRepository _paginationuserRepository;

        public UpdatePaginationUserHandler(IPaginationUserRepository paginationuserRepository)
        {
            _paginationuserRepository = paginationuserRepository;
        }

        public async Task<int> Handle(UpdatePaginationUserCommand command, CancellationToken cancellationToken)
        {
            var paginationuser = new PaginationUserEntity()
            {
                Id = command.Id,
                PagUserWeeks = command.PagUserWeeks,
                PagUserPublications = command.PagUserPublications,
                UserId = command.UserId,
            };

            return await _paginationuserRepository.UpdateAsync(paginationuser);
        }
    }
}
