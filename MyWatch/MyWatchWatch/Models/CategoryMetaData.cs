using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWatchWatch.Models
{
    [MetadataType(typeof(Category.CategoryMetaData))]
    public partial class Category
    {
        internal sealed class CategoryMetaData
        {
            [Display(Name = "ID")]
            public int CategoryId { get; set; }

            [Display(Name = "Category")]
            [Required(ErrorMessage ="Record can not null")]
            public string CategoryName { get; set; }
            [Display(Name = "Description")]
            [DataType(DataType.MultilineText)]
            [Required(ErrorMessage = "Record can not null")]
            public string CategoryDetails { get; set; }
            [Display(Name = "Status")]
            public bool Status_Category { get; set; }
        }
    }
}