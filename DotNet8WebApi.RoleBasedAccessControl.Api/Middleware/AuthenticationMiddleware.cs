using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet8WebApi.RoleBasedAccessControl.Api.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? authHeader = context.Request.Headers["Authorization"];
            string requestPath = context.Request.Path;

            if (requestPath == "/api/account/register" || requestPath == "/api/account/login")
            {
                await _next(context);
                return;
            }

            if (!string.IsNullOrEmpty(authHeader) && authHeader is not null && authHeader.StartsWith("Bearer"))
            {
                string[] header_and_token = authHeader.Split(' ');
                string header = header_and_token[0];
                string token = header_and_token[1];

                JwtSecurityTokenHandler tokenHandler = new();
                byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

                TokenValidationParameters parameters = new()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);

                if (principal is not null)
                {
                    await _next(context);
                    return;
                }
                else
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}
