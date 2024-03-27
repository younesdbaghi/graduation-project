using MediatR;
using Referentiel.Application.PaginationUser.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationUser.Commands.Handlers
{
    public class DeletePaginationUserHandler : IRequestHandler<DeletePaginationUserCommand, int>
    {
        private readonly IPaginationUserRepository _paginationuserRepository;

        public DeletePaginationUserHandler(IPaginationUserRepository paginationuserRepository)
        {
            _paginationuserRepository = paginationuserRepository;
        }

        public async Task<int> Handle(DeletePaginationUserCommand command, CancellationToken cancellationToken)
        {
            var entity= await _paginationuserRepository.GetByIdAsync(command.Id);
            return await _paginationuserRepository.DeleteAsync(entity);
        }
    }
}
