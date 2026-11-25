using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DSW_3P_PR03.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace DSW_3P_PR03.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginDto login)
    {
        // Obtener credenciales de demostración
        var usuario = _configuration["DemoUser:Usuario"]
            ?? throw new InvalidOperationException(
                "Falta configurar el usuario");

        var password = _configuration["DemoUser:Password"]
            ?? throw new InvalidOperationException(
                "Falta configurar la contraseña");

        // Validar credenciales
        if (login.Usuario != usuario ||
            login.Password != password)
        {
            return Unauthorized(
                "Credenciales incorrectas"
            );
        }

        // Obtener configuración JWT
        var jwt = _configuration.GetSection("Jwt");

        var key = Convert.FromBase64String(
            jwt["Key"] ?? throw new InvalidOperationException(
                "No se configuró Jwt:Key"
            )
        );

        // Definir los Claims
        var claims = new[]
        {
            new Claim(
                ClaimTypes.Name,
                login.Usuario
            ),

            new Claim(
                ClaimTypes.Role,
                "Administrador"
            )
        };

        // Crear las credenciales de firma
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        );

        // Obtener tiempo de expiración
        int minutos = int.Parse(
            jwt["ExpirationMinutes"] ?? "30"
        );

        // Generar el token
        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutos),
            signingCredentials: credentials
        );

        // Convertir el token a string
        var tokenString =
            new JwtSecurityTokenHandler().WriteToken(token);

        // Devolver la respuesta
        return Ok(new
        {
            access_token = tokenString,
            token_type = "Bearer",
            expires_in = minutos * 60
        });
    }
}