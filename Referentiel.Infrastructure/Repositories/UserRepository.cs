using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>,IUserRepository
    {
        public UserRepository(MyDbContext dbContext):base(dbContext)
        {
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            try
            {
                return await _dbContext.User
                    .Include(uw => uw.UserWeekProjects)
                    .OrderByDescending(u => u.Id) // Tri décroissant par ID
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Gérez les exceptions appropriées ici
                return default;
            }
        }

        public async Task<List<UserEntity>> GetUsersByUsernameAsync(string username)
        {
            try
            {
                return await _dbContext.User
                    .Where(u => u.Username.StartsWith(username))
                    .OrderByDescending(u => u.Id) // Tri décroissant par ID
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Gérez les exceptions appropriées ici
                return default;
            }
        }

    }
}
