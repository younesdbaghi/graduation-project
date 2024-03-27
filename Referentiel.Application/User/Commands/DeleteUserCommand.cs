using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.User.Commands
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int UserId)
        {
            this.Id = UserId;
        }
    }
}
