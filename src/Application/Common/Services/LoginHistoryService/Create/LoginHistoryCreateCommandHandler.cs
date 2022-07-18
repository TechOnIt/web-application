using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Common.Services.LoginHistoryService.Create
{
    public class LoginHistoryCreateCommandHandler : IRequestHandler<LoginHistoryCreateCommand>
    {

        public Task<Unit> Handle(LoginHistoryCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
