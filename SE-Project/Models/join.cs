using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SE_Project.Models
{
    public class join
    {
        [Required()]
        public string firstName { set; get; }
        [Required()]
        public string middleName { get; set; }
        [Required()]
        public string lastName { get; set; }
        [Required()]
        public string email { get; set; }
        [Required()]
        public string phone { get; set; }
        [Required()]
        public string userName { get; set; }
        [Required()]
        public string password { get; set; }
    }
}