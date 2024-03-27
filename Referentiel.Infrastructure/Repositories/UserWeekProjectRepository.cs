using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;
using Referentiel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserWeekProjectRepository : RepositoryBase<UserWeekProjectEntity>, IUserWeekProjectRepository
{
    public UserWeekProjectRepository(MyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<UserWeekProjectEntity>> GetAllWeekProjectsByIdAsync(int id)
    {
        try
        {
            // Utilisez OrderByDescending pour trier les entit�s par ID de mani�re d�croissante
            return await _dbContext.UserWeekProject
                .Where(uwp => uwp.WeekId == id)
                .OrderByDescending(uwp => uwp.Id) // Tri d�croissant par ID
                .ToListAsync();
        }
        catch (Exception ex)
        {
            // G�rez les exceptions appropri�es ici
            return default;
        }
    }






    public async Task<int> DeleteAllByWeekIdAsync(int weekId)
    {
        try
        {
            var userWeekProjectsToDelete = await _dbContext.UserWeekProject
                .Where(uwp => uwp.WeekId == weekId)
                .ToListAsync();

            if (userWeekProjectsToDelete != null && userWeekProjectsToDelete.Any())
            {
                _dbContext.UserWeekProject.RemoveRange(userWeekProjectsToDelete);
                return await _dbContext.SaveChangesAsync();
            }

            return 0; // Aucun objet � supprimer
        }
        catch (Exception ex)
        {
            // G�rez l'exception appropri�e (journalisation, etc.)
            return 0; // Ou renvoyez une valeur indiquant l'�chec de la suppression
        }
    }
}
