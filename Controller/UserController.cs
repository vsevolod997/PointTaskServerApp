using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClientApp.Models;
using System.IO;

namespace ClientApp.Controllers
{


    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        BaseContext db;

        public UserController (BaseContext context)
        {
            this.db = context;
        }


        // GET api/values получить свой ID
        [Authorize]
        [HttpGet("getmy")]
        public ActionResult<string> GetMy()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null)
            {
                return Ok(idClaim.Value);
            }
            return NotFound("No Info");
        }

        // GET api/values/получить данные о своем ID
       // [Authorize]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                UserLight upload = new UserLight( user.Photo, user.NameOth, 
                    user.Family, user.DateTime,
                    user.CompanyId, user.Phone,
                    user.Post);
                return new ObjectResult(upload);
            }
            
        }

       

        // POST api/values создание нового пользователя???
        [Authorize(Roles = "CompanyAdmin")]
        [Authorize(Roles = "MainAdmin")]
        [HttpPost]
        public ActionResult AddUserPost([FromBody] User user)
        {
            if (user == null)
            {
               return BadRequest();
            }
            db.Users.Add(user);
            db.SaveChanges();
            return Ok();
        }

        // PUT api/values/5обновление своего профиля
        [HttpPut]
        public ActionResult Put([FromBody] User user)
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null & user!= null)
            {
                if (db.Users.Any(x => x.Id == int.Parse(idClaim.Value)))
                {
                    db.Update(user);
                    db.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        // DELETE api/values/5 удалить
        [Authorize(Roles ="CompanyAdmin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return BadRequest();
            }

            db.Remove(user);
            db.SaveChanges();
            return Ok();
        }
    }
}
