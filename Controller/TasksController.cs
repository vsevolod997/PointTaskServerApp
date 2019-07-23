using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private BaseContext db;

        public TasksController(BaseContext context)
        {
            db = context;
        }
 
        // GET: api/Tasks выдача списка задач где пользователь ответственный
        [HttpGet("respond")]
        [Authorize]
        public ActionResult GetRespond(string sort)
        {
            // List<TasksUser> myTaskRespond = new List<TasksUser>();

            List<TasksUserLigth> myTaskRespond = new List<TasksUserLigth>();

            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));

            if (idClaim != null)
            {
                var res = db.TasksUsers.Join(db.Users,
                                e => e.PutId,
                                o => o.Id,
                                (e, o) => new
                                {
                                    Id = e.Id,
                                    PutName = o.Family + " " + o.NameOth,
                                    ResponId = e.ResponId,
                                    Text = e.Text,
                                    PutId = e.PutId,
                                    Main = e.Main,
                                    Status = e.Status,
                                    Latitude = e.Latitude,
                                    Longitude = e.Longitude,
                                    TimeEnd = e.TimeEnd,
                                }
                        );

                foreach (var a in res)
                {
                    if (a.ResponId  == int.Parse(idClaim.Value) && a.Status == sort)
                    {
                        myTaskRespond.Add(new TasksUserLigth
                        {
                            Id = a.Id,
                            Text = a.Text,
                            Latitude = a.Latitude,
                            Longitude = a.Longitude,
                            Main = a.Main,
                            PutId = a.PutId,
                            ResponId = a.ResponId,
                            TimeEnd = a.TimeEnd,
                            PutIdName = a.PutName
                        });

                    }
                }
            }
            return new ObjectResult(myTaskRespond);
        }
        //Выдача списка задач где пользователь постановщик
        [HttpGet("put")]
        public ActionResult<string> GetPut(string sort)
        {
            List<TasksUserLigth> myTaskPut = new List<TasksUserLigth>();

            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null)
            {
                var res = db.TasksUsers.Join(db.Users,
                                e => e.PutId,
                                o => o.Id,
                                (e, o) => new
                                {
                                    Id = e.Id,
                                    PutName = o.Family + " " + o.NameOth,
                                    ResponId = e.ResponId,
                                    Text = e.Text,
                                    PutId = e.PutId,
                                    Main = e.Main,
                                    Status = e.Status,
                                    Latitude = e.Latitude,
                                    Longitude = e.Longitude,
                                    TimeEnd = e.TimeEnd,
                                }
                        );

                foreach (var a in res)
                {

                    if (a.PutId == int.Parse(idClaim.Value) && a.Status == sort)
                    {
                        myTaskPut.Add(new TasksUserLigth
                        {
                            Id = a.Id,
                            Text = a.Text,
                            Latitude = a.Latitude,
                            Longitude = a.Longitude,
                            Main = a.Main,
                            PutId = a.PutId,
                            PutIdName  = a.PutName,
                            ResponId = a.ResponId,
                            TimeEnd = a.TimeEnd
                        });
                    }
                }
            }

            return new ObjectResult(myTaskPut);
        }

        // GET: api/Tasks/ информация по конкретной задаче
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {

            TasksUser task = db.TasksUsers.FirstOrDefault(x => x.Id == id);
            //User Put = db.Users.FirstOrDefault(x => x.Id == task.PutId);
            //User Respone = db.Users.FirstOrDefault(x => x.Id == task.ResponId);

            

            if (task != null)
            {

                var countComments = db.Comments.Count(s => s.TaskId == id);

                var res1 = db.TasksUsers.Join(db.Users,
               e => e.ResponId,
               o => o.Id,
               (e, o) => new
               {
                   Id = e.Id,
                   ResponName = o.Family + " " + o.NameOth,
                   ResponId = e.ResponId,
                   Text = e.Text,
                   PutId = e.PutId,
                   Main = e.Main,
                   Status = e.Status,
                   TimeStart = e.TimeStart,
                   TimeEnd = e.TimeEnd,
                   AllText = e.AllText,
                   ImageURL = e.URLimage
                 //  Comments = e.Comments
               }
               );

                var res2 = res1.Join(db.Users,
                    e => e.PutId,
                    o => o.Id,
                    (e, o) => new
                    {
                        Id = e.Id,
                        ResponName = e.ResponName,
                        ResponId = e.ResponId,
                        Text = e.Text,
                        PutName = o.Family + " " + o.NameOth,
                        PutId = e.PutId,
                        Main = e.Main,
                        Status = e.Status,
                        TimeStart = e.TimeStart,
                        TimeEnd = e.TimeEnd,
                        AllText = e.AllText,
                        Comments = countComments,
                        ImageURL = e.ImageURL
                    }
                    );

                var result = res2.FirstOrDefault(x => x.Id == id);
                return new ObjectResult(result);
            }

            return NotFound();

        }



        //public ActionResult Post(int PutId, int ResponeId, string Text, string AllText, string Status, double Latitude, double Longitude, bool Main, string URLImage,
        //  string TimeStart, string TimeEnd)

        // POST: api/Tasks
        [HttpPost]
        public ActionResult Post (int PutId, int ResponeId)
        {
            var d = Request.Form;
            var s = PutId + ResponeId;
            return Ok(s);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] TasksUser task)
        {

            if (db.TasksUsers.Any(x => x.Id == id))
            {
                db.Update(task);
                db.SaveChanges();
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null)
            {
                TasksUser task = db.TasksUsers.FirstOrDefault(x => x.Id == id);

                if (task == null)
                {
                    return NotFound();
                }
          
                  if (task.PutId == int.Parse(idClaim.Value))
                    {
                    db.TasksUsers.Remove(task);
                    db.SaveChanges();
                    return Ok();
                    }
                return BadRequest("No");
      
            }
            return BadRequest();
        }
    }
}
