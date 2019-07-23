using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class TasksUserLigth
    {
        public int Id { get; set; }

        public int PutId { get; set; }
        public string PutIdName { get; set; }
        public int ResponId { get; set; }
        public string Text { get; set; }
        public bool Main { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime TimeEnd { get; set; }
    }
}
