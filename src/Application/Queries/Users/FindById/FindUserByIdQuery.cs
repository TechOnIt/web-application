using iot.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Queries.Users.FindById
{
    public class FindUserByIdQuery : IRequest<UserViewModel>
    {
        // input parameter
        public Guid Id { get; set; }
    }
}
