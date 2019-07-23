using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogController : ControllerBase
    {


        private BaseContext db;


        public DialogController(BaseContext context)
        {
            this.db = context;
        }

        // GET: api/Message
        [HttpGet("{id}")]
        public ActionResult<Message> GetAllDialog(int page)
        {
            int pageSize = 10; 
            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null)
            {

                var countSourse = db.Dialogs.Count(x=>x.FirstUserId == int.Parse(idClaim.Value));
                var items = db.Dialogs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                PageViewModel pageViewModel = new PageViewModel(countSourse, page, pageSize);
                IndexViewModel viewModel = new IndexViewModel
                {
                    PageViewModel = pageViewModel,
                    Dialog = items
                };
                return new ObjectResult(viewModel);
            }

            return BadRequest();
        }

        // POST: api/Message
        [HttpPost]
        public ActionResult Post([FromBody] Dialog dlg)
        {
            
             if (db.Dialogs.Any(x => x.FirstUserId == dlg.FirstUserId && db.Dialogs.Any(y=>y.SecondUserId == dlg.SecondUserId))||

               (db.Dialogs.Any(x => x.SecondUserId == dlg.FirstUserId && db.Dialogs.Any(y => y.SecondUserId == dlg.FirstUserId)))){

                return Ok();

             }else{
                db.Dialogs.Add(dlg);
                db.SaveChanges();
                return Ok();

            }
            
        }

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Message msg)
        {


        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult>  Delete(int? id)
        {
            if(id != null)
            {
                Dialog dlg = await db.Dialogs.FirstOrDefaultAsync(x => x.Id == id);
                var userId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
                if(dlg != null && userId != null)
                {
                    if(dlg.FirstUserId == int.Parse(userId.Value))
                    {
                        dlg.FirstSee = false;
                        await db.SaveChangesAsync();
                        foreach (var mes in db.Messages)
                        {
                            if (mes.DialogId == id)
                            {
                                if (mes.InputId == int.Parse(userId.Value))
                                {
                                    mes.IsVisibleScond = false;
                                }
                                if (mes.OutputId == int.Parse(userId.Value))
                                {
                                    mes.IsVisibleScond = false;
                                }
                            }
                        }
                        await db.SaveChangesAsync();
                        return Ok();
                    }else{
                            if (dlg.SecondUserId == int.Parse(userId.Value))
                             {
                                dlg.SecondSee = false;
                                await db.SaveChangesAsync();
                                return Ok();
                            }else{
                                 return NotFound();
                                 }
                         }
                   
                }
                else
                {
                   return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            } 
        }
    }
}
