using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWatchWatch.Models
{
    [MetadataType(typeof(Employee.EmployeeMetaData))]
    public partial class Employee
    {
        internal sealed class EmployeeMetaData
        {
            [Display(Name = "ID")]
            [Required(ErrorMessage = "Record can not null")]
            public string EmployeeCode { get; set; }

            [Display(Name = "Password")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.Password)]
            public string EmployeePass { get; set; }

            [Display(Name = "Last Name")]
            [Required(ErrorMessage = "Record can not null")]
            public string LastName { get; set; }

            [Display(Name = "First Name")]
            [Required(ErrorMessage = "Record can not null")]
            public string FirstName { get; set; }

            [Display(Name = "Gender")]
            public Nullable<bool> EmployeeGender { get; set; }

            [Display(Name = "Birthday")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.DateTime, ErrorMessage = "Input is data type")]
            [Required(ErrorMessage = "Record can not null")]
            public Nullable<System.DateTime> BirthDate { get; set; }

            [Display(Name = "IMG")]
            public string EmployeImg { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.EmailAddress,ErrorMessage ="Email is not valid")]
            public string EmployeeEmail { get; set; }

            [Display(Name = "Address")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.MultilineText)]
            public string EmployeeAddress { get; set; }
        }
    }
}