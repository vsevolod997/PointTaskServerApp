using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string NameOth { get; set; }
        public string Family { get; set; }
        public DateTime DateTime { get; set; }


        public int CompanyId { get; set; }
        public string Phone { get; set; }
        public string Post { get; set; }
        public string Role { get; set; }

        public virtual Company Company { get; set; }

        public List<TasksUser> TasksUsers { get; set; }
        public List<Dialog> Dialogs { get; set; }
        public List<Message> Messages { get; set; }
        public List<Comment> Comments { get; set; }

        public User()
        {
            TasksUsers = new List<TasksUser>();
            Dialogs = new List<Dialog>();
            Messages = new List<Message>();
            Comments = new List<Comment>();
        }
            
    }

}
