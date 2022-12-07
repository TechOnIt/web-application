using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TechOnIt.Application.Common.Security.JwtBearer
{
    public class JwtAuthenticationAttribute : AuthorizeAttribute
    {
        public JwtAuthenticationAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
