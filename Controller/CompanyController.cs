using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private BaseContext db;

        public CompanyController(BaseContext context)
        {
            db = context;
        }


        // GET: api/Company/id
        [HttpGet("{id}")]
        public ActionResult<string> InfoCompanyGet(int id)
        {
           Company company = db.Companies.FirstOrDefault(x => x.Id == id);

            if (company == null)
                return NotFound();

            return new ObjectResult(company);
        }

        // POST: api/Company
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
        [HttpGet("getresponsableuser")]
        public ActionResult GetRespondebleUser()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null)
            {
                var userNow = db.Users.FirstOrDefault(x => x.Id == int.Parse(idClaim.Value));
                var id = userNow.CompanyId;
                var user = db.Users.Where(t => t.CompanyId == id).OrderBy(t => t);
                return new ObjectResult(user);
             }else
            {
                return NotFound();
            }
        }

        // PUT: api/Company/5
        [Authorize(Roles = "CompanyAdmin")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Company company)
        {
        
                if (db.Companies.Any(x => x.Id == id))
                {
                    db.Update(company);
                    db.SaveChanges();
                    return Ok(company);
                }
                else
                {
                    return NotFound();
                }
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "MainAdmin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Company company = db.Companies.FirstOrDefault(x => x.Id == id);

            if (company == null)
            {
                return BadRequest();
            }

            db.Remove(company);
            db.SaveChanges();
            return Ok();
        }
    }
}
