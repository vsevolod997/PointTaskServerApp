using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class TasksUser
    {
        public int Id { get; set; }
        public int PutId { get; set; }
        public int ResponId { get; set; }
        public string Text { get; set; }
        public string AllText { get; set; }
        public string Status { get; set; }
        public double Latitude { get; set;}
        public double Longitude { get; set;}
        public string URLimage { get; set; }
        public bool Main { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime? TimeEndFact { get; set; }

        public bool TimeFail { get; set; }


        public virtual User User { get; set; }

        public List<Comment> Comments { get; set; }

        public TasksUser()
        {
            Comments = new List<Comment>();
            TimeStart = DateTime.UtcNow;

            if (TimeEnd > TimeEndFact)
            {
                TimeFail = true;
            }
            else{
                TimeFail = false;
            }
        }



    }

    
}
