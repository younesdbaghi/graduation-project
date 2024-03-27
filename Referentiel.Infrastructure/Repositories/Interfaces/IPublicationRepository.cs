using System;
using System.Collections.Generic;
using Referentiel.Domain.Entities;

namespace Referentiel.Infrastructure.Repositories.Interfaces
{
    public interface IPublicationRepository : IAsyncRepository<PublicationEntity>
    {
    }
}
