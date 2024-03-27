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
    public class DeletePaginationAdminCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeletePaginationAdminCommand(int PaginationAdminId)
        {
            this.Id = PaginationAdminId;
        }
    }
}
