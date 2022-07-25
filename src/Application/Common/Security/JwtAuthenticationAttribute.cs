using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace iot.Application.Common.Security
{
    public class JwtAuthenticationAttribute : AuthorizeAttribute
    {
        public JwtAuthenticationAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
