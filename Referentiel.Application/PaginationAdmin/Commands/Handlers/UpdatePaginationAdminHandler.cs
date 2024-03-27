using MediatR;
using Referentiel.Application.PaginationAdmin.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.PaginationAdmin.Commands.Handlers
{
    public class UpdatePaginationAdminHandler : IRequestHandler<UpdatePaginationAdminCommand, int>
    {
        private readonly IPaginationAdminRepository _paginationadminRepository;

        public UpdatePaginationAdminHandler(IPaginationAdminRepository paginationadminRepository)
        {
            _paginationadminRepository = paginationadminRepository;
        }

        public async Task<int> Handle(UpdatePaginationAdminCommand command, CancellationToken cancellationToken)
        {
            var paginationadmin = new PaginationAdminEntity()
            {
                Id = command.Id,
                PagAdminUsers = command.PagAdminUsers,
                PagAdminPublications = command.PagAdminPublications,
                PagAdminProjects = command.PagAdminProjects,
                PagAdminQuotations = command.PagAdminQuotations,
                UserId = command.UserId,
            };

            return await _paginationadminRepository.UpdateAsync(paginationadmin);
        }
    }
}
