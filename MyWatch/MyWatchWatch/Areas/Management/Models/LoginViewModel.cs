using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWatchWatch.Areas.Management.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username can not null")]
        public string EmployeeCode { get; set; }
        [Required(ErrorMessage = "Password can not null")]
        public string EmployeePass { get; set; }
    }
}