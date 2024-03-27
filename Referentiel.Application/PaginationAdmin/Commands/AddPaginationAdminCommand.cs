using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.PaginationAdmin.Commands
{
    public class AddPaginationAdminCommand : IRequest<PaginationAdminEntity>
    {
        public int Id { get; set; }
        public int PagAdminUsers { get; set; }
        public int PagAdminPublications { get; set; }
        public int PagAdminProjects { get; set; }
        public int PagAdminQuotations { get; set; }
        public int UserId { get; set; }

        public AddPaginationAdminCommand(int Id, int PagAdminUsers, int PagAdminPublications, int PagAdminProjects, int PagAdminQuotations, int UserId)
        {
            this.Id = Id;
            this.PagAdminUsers = PagAdminUsers;
            this.PagAdminPublications = PagAdminPublications;
            this.PagAdminProjects = PagAdminProjects;
            this.PagAdminQuotations = PagAdminQuotations;
            this.UserId = UserId;
        }
    }
}
