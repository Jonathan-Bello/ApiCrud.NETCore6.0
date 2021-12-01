using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventarioWebApiReact.Models;
using InventarioWebApiReact.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InventarioWebApiReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly iOperacionesBD db;
        private readonly IConfiguration _config;
        public LoginController(iOperacionesBD db, IConfiguration config)
        {
            this.db = db;
            this._config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuarios u)
        {
            Usuarios user = await db.Loguear(u.Usuario!, u.Pwd!);
            if (user == null)
            {
                return BadRequest("Usuario o contraseña incorrecta");
            }
            else
            {
                string token = ObtenerToken(user);
                return Ok(new
                {
                    estatus = "200",
                    token = token,
                    user = user
                });
            }
        }

        private string ObtenerToken(Usuarios u)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Autenticacion:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var headers = new JwtHeader(creds);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name , u.Usuario!),
                new Claim(ClaimTypes.UserData,u.Usuario!)
            };

            JwtPayload payload = new JwtPayload(
                issuer: _config["Autenticacion:Issuer"],
                audience: _config["Autenticacion:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30)
            );

            var token = new JwtSecurityToken(headers, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
