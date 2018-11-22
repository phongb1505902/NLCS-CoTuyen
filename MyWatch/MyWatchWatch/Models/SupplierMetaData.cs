using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWatchWatch.Models
{
    [MetadataType(typeof(Supplier.SupplierMetaData))]
    public partial class Supplier
    {
        internal sealed class SupplierMetaData
        {
            [Display(Name = "ID")]
            public int SupplierId { get; set; }

            [Display(Name = "Company")]
            [Required(ErrorMessage = "Record can not null")]
            public string CompanyName { get; set; }

            [Display(Name = "Representative")]
            [Required(ErrorMessage = "Record can not null")]
            public string ContactName { get; set; }

            [Display(Name = "Context")]
            [DataType(DataType.MultilineText)]
            [Required(ErrorMessage = "Record can not null")]
            public string ContactTitle { get; set; }

            [Display(Name = "Address")]
            [DataType(DataType.MultilineText)]
            [Required(ErrorMessage = "Record can not null")]
            public string Address { get; set; }

            [Display(Name = "Phone")]
            [Required(ErrorMessage = "Record can not null")]

            public string Phone { get; set; }

            [Display(Name = "Fax")]
            [Required(ErrorMessage = "Record can not null")]
            public string Fax { get; set; }

            [Display(Name = "Website")]
            [Required(ErrorMessage = "Record can not null")]
            public string HomePage { get; set; }

            [Display(Name = "Status")]
            public bool Status_Supplier { get; set; }
        }
    }

}