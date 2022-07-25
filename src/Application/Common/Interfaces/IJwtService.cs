using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateTokenWithClaims(List<Claim> claims, int expireDateTime = 20);
    }
}
