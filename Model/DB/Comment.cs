using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime TimeComment { get; set; }

        [ForeignKey("TasksUsers")]
        public int TaskId {get; set;}
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }

        public virtual TasksUser TasksUsers { get; set; }
        public virtual User User { get; set; }

    }
}
