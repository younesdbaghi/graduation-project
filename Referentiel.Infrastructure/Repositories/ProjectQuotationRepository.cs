using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Infrastructure.Repositories
{
    public class ProjectQuotationRepository : RepositoryBase<ProjectQuotationEntity>,IProjectQuotationRepository
    {
        public ProjectQuotationRepository(MyDbContext dbContext):base(dbContext)
        {
        }
    }
}
