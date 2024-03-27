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
    public class AddPaginationUserCommand : IRequest<PaginationUserEntity>
    {
        public int Id { get; set; }
        public int PagUserWeeks { get; set; }
        public int PagUserPublications { get; set; }
        public int UserId { get; set; }

        public AddPaginationUserCommand(int Id, int PagUserWeeks, int PagUserPublications, int UserId)
        {
            this.Id = Id;
            this.PagUserWeeks = PagUserWeeks;
            this.PagUserPublications = PagUserPublications;
            this.UserId = UserId;
        }
    }
}
