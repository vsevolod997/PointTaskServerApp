using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Dialog
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int FirstUserId { get; set; }// createrDiolog

        [ForeignKey("User")]
        public int SecondUserId { get; set; }
        public bool Main { get; set; }
        public bool FirstSee { get; set; }
        public bool SecondSee { get; set; }

        public virtual User Users { get; set; }
        public List<Message> Messages { get; set; }

        public Dialog()
        {
            Messages = new List<Message>();
            FirstSee = true;
            SecondSee = true;
        }
    }
}
