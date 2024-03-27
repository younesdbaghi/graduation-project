using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Referentiel.Infrastructure.Repositories
{
    public class UserWeekRepository : RepositoryBase<UserWeekEntity>,IUserWeekRepository
    {
        public UserWeekRepository(MyDbContext dbContext):base(dbContext)
        {
        }


        public async Task<List<UserWeekEntity>> GetAllAsync()
        {
            try
            {
                return await _dbContext.UserWeek
                .Include(uwp => uwp.UserWeekProjects)
                .OrderByDescending(uwp => uwp.Id)  // Remplacez "VotreProprieteDeTri" par le nom de la propriété par laquelle vous souhaitez trier
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return default;
            }
        }


        public async Task<List<UserWeekEntity>> GetWeeksForSpecUserAsync(int id_user)
        {
            try
            {
                return await _dbContext.UserWeek
                .Where(uw => uw.UserId == id_user)
                .Include(uwp => uwp.UserWeekProjects)
                .OrderByDescending(uwp => uwp.Id)  // Remplacez "VotreProprieteDeTri" par le nom de la propriété par laquelle vous souhaitez trier
                .ToListAsync();

            }
            catch (Exception ex)
            {
                return default;
            }
        }


        public async Task<List<UserWeekEntity>> GetWeeksByUserIdAsync(int userId)
        {
            return await this._dbContext.UserWeek
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id) // Tri décroissant par ID
                .ToListAsync();
        }


        public override async Task<UserWeekEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.UserWeek
                    .Where(uw => uw.Id == id)
                    .Include(uwp => uwp.UserWeekProjects)
                    .OrderByDescending(uw => uw.Id) // Tri décroissant par ID
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return default;
            }
        }


        public async Task<UserWeekEntity> GetSpecWeekForSpecUser(int id_user, int id_week)
        {
            try
            {
                return await _dbContext.UserWeek
                    .Where(uw => uw.UserId == id_user && uw.Id == id_week)
                    .Include(uwp => uwp.UserWeekProjects)
                    .OrderByDescending(uw => uw.Id) // Tri décroissant par ID
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return default;
            }
        }



        public async Task<List<UserWeekProjectEntity>> GetAllProjectsForThisWeek(int id_user, int id_week)
        {
            try
            {
                return await _dbContext.UserWeekProject
                    .Where(uw => uw.UserId == id_user && uw.WeekId == id_week)
                    .OrderByDescending(uw => uw.Id) // Tri décroissant par ID
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
