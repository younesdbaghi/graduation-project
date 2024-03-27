using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Infrastructure.Repositories
{
    public class PaginationAdminRepository : RepositoryBase<PaginationAdminEntity>,IPaginationAdminRepository
    {
        public PaginationAdminRepository(MyDbContext dbContext):base(dbContext)
        {
        }
        public override async Task<PaginationAdminEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.PaginationAdmin
                    .Where(PA=>PA.UserId==id)
                    .OrderByDescending(PA => PA.Id)  // Remplacez DateProperty par la propriété que vous utilisez pour trier par date
                    .FirstOrDefaultAsync(); ;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
