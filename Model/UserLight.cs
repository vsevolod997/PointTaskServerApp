using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class UserLight
    {
        public string Photo { get; set; }
        public string NameOth { get; set; }
        public string Family { get; set; }
        public DateTime DateTime { get; set; }

        public int CompanyId { get; set; }
        public string Phone { get; set; }
        public string Post { get; set; }


        public UserLight(string photo, string nameOth, string family, DateTime dateTime, int companyId, string phone, string post)
        {
            Photo = photo;
            NameOth = nameOth;
            Family = family;
            DateTime = dateTime;
            CompanyId = companyId;
            Phone = phone;
            Post = post;
        }
    }
}
