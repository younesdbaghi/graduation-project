using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.PaginationUser.Commands
{
    public class DeletePaginationUserCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeletePaginationUserCommand(int PaginationUserId)
        {
            this.Id = PaginationUserId;
        }
    }
}
