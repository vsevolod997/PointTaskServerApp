using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ClientApp.Controllers
{

    [Route("api/auth")]
    public class AuthController : Controller
    {


        private BaseContext db;


        public AuthController(BaseContext context)
        {
            this.db = context;
        }
    
        [HttpPost("token")]
        public IActionResult  Token()
        {
            //string tokenString = "test";
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); 
                var usernameAndPass = usernameAndPassenc.Split(":");
                //проверка логина и пароля в DB

                var identy =  GetIdentity(usernameAndPass[0], usernameAndPass[1]);
                if(identy !=null)
                {
          
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ahbasshfbsahjfbshajbfhjasbfashjbfsajhfvashjfashfbsahfbsahfksdjf"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken(
                         issuer: "mysite.com",
                         audience: "PointTask",
                         expires: DateTime.Now.AddMinutes(1),
                         claims: identy.Claims,
                         signingCredentials: signInCred
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
                else
                {
                    return  NotFound();
                }
            }
            return BadRequest(); 

            // return View();
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User person = db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                 //   new Claim(ClaimsIdentity.DefaultNameClaimType, person.Name),
                    new Claim(ClaimTypes.Role , person.Role),
                    new Claim("Id", person.Id.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Basic", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }


    }
}