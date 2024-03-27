using System;
using System.Collections.Generic;
using Referentiel.Domain.Entities;

namespace Referentiel.Infrastructure.Repositories.Interfaces
{
    public interface IUserWeekProjectRepository : IAsyncRepository<UserWeekProjectEntity>
    {
        Task<List<UserWeekProjectEntity>> GetAllWeekProjectsByIdAsync(int id);
        Task<int> DeleteAllByWeekIdAsync(int weekId);
    }
}
