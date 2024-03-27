using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Infrastructure.Repositories
{
    public class ProjectStatisticRepository : RepositoryBase<ProjectStatisticEntity>,IProjectStatisticRepository
    {
        public ProjectStatisticRepository(MyDbContext dbContext):base(dbContext)
        {
        }
    }
}
