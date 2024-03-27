using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeek.Commands
{
    public class UpdateUserWeekCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string WeekNumber { get; set; }
        public string StatusWeek { get; set; }
        public int UserId { get; set; }

        public UpdateUserWeekCommand(int Id, string WeekNumber, string StatusWeek, int UserId)
        {
            this.Id = Id;
            this.WeekNumber = WeekNumber;
            this.StatusWeek = StatusWeek;
            this.UserId = UserId;
        }
    }
}
