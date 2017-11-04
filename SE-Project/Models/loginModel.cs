using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SE_Project.Models
{
    public class loginModel
    {
        [Required()]
        public string userName { get; }
        [Required()]
        public string password { get; }
    }
}