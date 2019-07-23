using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Picture { get; set; }

        public List<User> Users { get; set; }
        public List<Document> Documents { get; set; }
        public Company()
        {
            Users = new List<User>();
            Documents = new List<Document>();
        }
    }
}
