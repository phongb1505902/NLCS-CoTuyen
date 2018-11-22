using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWatchWatch.Models
{
    [MetadataType(typeof(News.NewsMetaData))]
    public partial class News
    {
        internal sealed class NewsMetaData
        {
            [Display(Name ="ID")]
            public int NewsId { get; set; }

            [Display(Name = "Titles")]
            [Required(ErrorMessage = "Record can not null")]
            public string NewTitles { get; set; }

            [Display(Name = "Context")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.MultilineText)]
            public string NewsDetails { get; set; }

            [Display(Name = "Date")]
            [Required(ErrorMessage = "Record can not null")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.DateTime, ErrorMessage = "Input is data type")]
            public Nullable<System.DateTime> NewsDate { get; set; }
        }
    }
}