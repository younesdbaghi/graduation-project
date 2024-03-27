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
    public class AddUserCommand : IRequest<UserEntity>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Activity { get; set; }
        public string Admin { get; set; }

        public AddUserCommand(int Id, string Name, string LastName, string Username, string Email, string Password, string Activity, string Admin)
        {
            this.Id = Id;
            this.Name = Name;
            this.LastName = LastName;
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.Activity = Activity;
            this.Admin = Admin;
        }
    }
}
