using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }

        public int OutputId { get; set; } // получатель
 
        public int InputId { get; set; } // ОТПРАВИТЕЛЬ

        [ForeignKey("Dialog")]
        public int DialogId { get; set; }
        public DateTime MessageDate { get; set;}
        public bool IsRead { get; set; }
        public bool IsVisibleScond { get; set; } //отображать у перовго пользователя 
        public bool IsVisibleSecond { get; set; } //отображать у второго 
        public string Photo { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }

        public virtual User User { get; set; }
        public virtual Dialog Dialog { get; set; }
    }
}
