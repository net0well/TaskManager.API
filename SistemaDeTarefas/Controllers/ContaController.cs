using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ContaController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginModel login)
    {
        if (login.Login == "admin" && login.Senha == "admin")
        {
            var token = GerarTokenJWT();
            return Ok(new { token });
        }

        return BadRequest(new { menssagem = "Verifique seu nome de usuario e senha." });
    }

    private string GerarTokenJWT()
    {
        string chaveScreta = "6b2d34a0-04ae-4858-9b8d-5b19664a9745";
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveScreta));

        var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("login", "admin"),
            new Claim("nome", "Administrador")
        };

        var token = new JwtSecurityToken(
            issuer: "task",
            audience: "app",
            claims: null,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credencial
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
   
}