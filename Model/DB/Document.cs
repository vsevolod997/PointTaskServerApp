using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URLstrng {get; set;}
        public string Type { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
