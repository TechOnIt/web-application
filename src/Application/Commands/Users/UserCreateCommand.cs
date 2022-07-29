using iot.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Users
{
    public class UserCreateCommand : IRequest<Guid>
    {
        // why did i add ? to string type properties while strings are already null-able ?
        // the answer is here for compiler warnings when we define a property as null-able
        // https://stackoverflow.com/questions/67505347/non-nullable-property-must-contain-a-non-null-value-when-exiting-constructor-co

        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public PasswordHash? Password { get; set; }
    }
}
