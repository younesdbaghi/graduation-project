using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Infrastructure.Repositories
{
    public class PaginationUserRepository : RepositoryBase<PaginationUserEntity>,IPaginationUserRepository
    {
        public PaginationUserRepository(MyDbContext dbContext):base(dbContext)
        {
        }
        public override async Task<PaginationUserEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.PaginationUser
                    .Where(PU => PU.UserId == id)
                    .OrderByDescending(PU => PU.Id)  // Remplacez DateProperty par la propriété que vous utilisez pour trier par date
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Gérez les exceptions appropriées ici
                return default;
            }
        }

    }
}
