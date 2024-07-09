﻿namespace DotNet8WebApi.RoleBasedAccessControl.Shared.Services.AuthService;

public class JWTAuth
{
    private readonly IConfiguration _configuration;

    public JWTAuth(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetJWTToken(JwtResponseModel jwtResponseModel)
    {
        try
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(
                    JwtRegisteredClaimNames.Iat,
                    ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                ),
                new Claim("UserName", jwtResponseModel.UserName),
                new Claim("Email", jwtResponseModel.Email),
                new Claim("UserRole", jwtResponseModel.UserRole),
                new Claim(ClaimTypes.Role, jwtResponseModel.UserRole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
