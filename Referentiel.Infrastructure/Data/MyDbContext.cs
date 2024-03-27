using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Data;

namespace Referentiel.Infrastructure.Data
{
    public class MyDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public MyDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connectionStringEnv = Environment.GetEnvironmentVariable("MyDbContext");
            var connectionString = connectionStringEnv == null ? Configuration.GetConnectionString("DefaultConnection") : connectionStringEnv;
            options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<UserWeekEntity> UserWeek { get; set; }
        public DbSet<UserWeekProjectEntity> UserWeekProject { get; set; }
        public DbSet<PaginationAdminEntity> PaginationAdmin { get; set; }
        public DbSet<PaginationUserEntity> PaginationUser { get; set; }
        public DbSet<ProjectEntity> Project { get; set; }
        public DbSet<ProjectQuotationEntity> ProjectQuotation { get; set; }
        public DbSet<ProjectStatisticEntity> ProjectStatistic { get; set; }
        public DbSet<PublicationEntity> Publication { get; set; }
    }
}
