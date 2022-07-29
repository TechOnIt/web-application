using iot.Application.Common.Interfaces.Context;
using iot.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Users
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Guid>
    {
        #region constracture
        public IIdentityContext _context { get; set; }
        public UserCreateCommandHandler(IIdentityContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Email,request.PhoneNumber,request.Password,request.Surname,request.Name);
            var result= await _context.Users.AddAsync(user);

            return result.Entity.Id;
        }
    }
}
