using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyWatchWatch.Models
{
    public class LoginU
    {
        [Required(ErrorMessage = "Username can not null")]
        public string CustomerCode { get; set; }
        [Required(ErrorMessage = "Password can not null")]
        public string CustomerPass { get; set; }
    }
}