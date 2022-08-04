using iot.Application.Common.Interfaces.Context;
using iot.Application.Common.Models;
using iot.Domain.Entities.Identity;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Queries.Users.FindById
{
    public class FindByIdQueryHandler : IRequestHandler<FindUserByIdQuery, UserViewModel>
    {
        #region Constracture
        private readonly IIdentityContext _context;
        public FindByIdQueryHandler(IIdentityContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<UserViewModel> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if(user != null)
            {
                return user.Adapt<UserViewModel>();
            }

            return new UserViewModel();
        }
    }
}
