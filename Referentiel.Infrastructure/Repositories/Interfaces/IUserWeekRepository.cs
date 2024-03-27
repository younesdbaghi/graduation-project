using System;
using System.Collections.Generic;
using Referentiel.Domain.Entities;

namespace Referentiel.Infrastructure.Repositories.Interfaces
{
    public interface IUserWeekRepository : IAsyncRepository<UserWeekEntity>
    {
        Task<List<UserWeekEntity>> GetWeeksByUserIdAsync(int userId);
        Task<List<UserWeekEntity>> GetWeeksForSpecUserAsync(int id_user);
        Task<UserWeekEntity> GetSpecWeekForSpecUser(int id_user, int id_week);
        Task<List<UserWeekProjectEntity>> GetAllProjectsForThisWeek(int id_user, int id_week);
    }
}
