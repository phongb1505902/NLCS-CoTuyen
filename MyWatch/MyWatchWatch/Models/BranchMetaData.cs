using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWatchWatch.Models
{
    [MetadataType(typeof(Branch.BranchMetaData))]
    public partial class Branch
    {
        internal sealed class BranchMetaData
        {
            [Display(Name = "ID")]
            public int BranchId { get; set; }

            [Display(Name = "Name")]
            [Required(ErrorMessage = "Record can not null")]
            public string BranchName { get; set; }

            [Display(Name = "Details")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.MultilineText)]
            public string BranchDetails { get; set; }
        }
    }

}