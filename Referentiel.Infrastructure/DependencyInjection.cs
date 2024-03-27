using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Referentiel.Infrastructure.Repositories.Interfaces;
using Referentiel.Infrastructure.Repositories;
using Referentiel.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Referentiel.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserWeekRepository, UserWeekRepository>();
            services.AddScoped<IUserWeekProjectRepository, UserWeekProjectRepository>();
            services.AddScoped<IPaginationAdminRepository, PaginationAdminRepository>();
            services.AddScoped<IPaginationUserRepository, PaginationUserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectQuotationRepository, ProjectQuotationRepository>();
            services.AddScoped<IProjectStatisticRepository, ProjectStatisticRepository>();
            services.AddScoped<IPublicationRepository, PublicationRepository>();
            services.AddDbContext<MyDbContext>();
        }
    }
}
