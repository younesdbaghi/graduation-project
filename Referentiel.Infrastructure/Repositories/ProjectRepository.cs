using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Infrastructure.Repositories
{
    public class ProjectRepository : RepositoryBase<ProjectEntity>,IProjectRepository
    {
        public ProjectRepository(MyDbContext dbContext):base(dbContext)
        {
        }
        public override async Task<ProjectEntity> GetByIdAsync(int id)
        {
            try
            {
                var project = await _dbContext.Project
                    .Where(p => p.Id == id)
                    .Include(p => p.ProjectQuotations)
                    .Include(p => p.ProjectStatistics)
                    .FirstOrDefaultAsync();

                return project;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions ici
                return default;
            }
        }

        public async Task<List<ProjectEntity>> GetAllAsync()
        {
            try
            {
                var projects = await _dbContext.Project
                    .Include(uwp => uwp.ProjectQuotations)
                    .Include(p => p.ProjectStatistics)
                    .OrderByDescending(p => p.Id) // Ordonner par Id en ordre décroissant
                    .ToListAsync();

                return projects;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions ici
                return default;
            }
        }
    }
}
