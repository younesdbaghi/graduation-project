using MediatR;
using Referentiel.Application.PaginationAdmin.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationAdmin.Commands.Handlers
{
    public class DeletePaginationAdminHandler : IRequestHandler<DeletePaginationAdminCommand, int>
    {
        private readonly IPaginationAdminRepository _paginationadminRepository;

        public DeletePaginationAdminHandler(IPaginationAdminRepository paginationadminRepository)
        {
            _paginationadminRepository = paginationadminRepository;
        }

        public async Task<int> Handle(DeletePaginationAdminCommand command, CancellationToken cancellationToken)
        {
            var entity= await _paginationadminRepository.GetByIdAsync(command.Id);
            return await _paginationadminRepository.DeleteAsync(entity);
        }
    }
}
