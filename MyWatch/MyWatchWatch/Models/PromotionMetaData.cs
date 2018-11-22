using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWatchWatch.Models
{
    [MetadataType(typeof(Promotion.PromotionMetaData))]
    public partial class Promotion
    {
        internal sealed class PromotionMetaData
        {
            [Display(Name = "ID")]
            [Required(ErrorMessage = "Record can not null")]
            public int PromotionId { get; set; }

            [Display(Name = "Promotion")]
            [Required(ErrorMessage = "Record can not null")]
            public string PromotionName { get; set; }

            [Display(Name = "Details")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.MultilineText)]
            public string PromotionDetails { get; set; }

            [Display(Name = "Discount")]
            [Required(ErrorMessage = "Discount can not null")]

            [Range(1, 99, ErrorMessage = "Limit 1-99")]
            public Nullable<int> PromotionDiscount { get; set; }

            [Display(Name = "Status")]

            public Nullable<bool> PromotionStatus { get; set; }

            [Display(Name = "Start")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date, ErrorMessage = "Input Data is not valid")]
            [Required(ErrorMessage = "Can not null")]
            public Nullable<System.DateTime> PromotionOpen { get; set; }

            [Display(Name = "End")]

            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date, ErrorMessage = "Input Data is not valid")]
            [Required(ErrorMessage = "Can not null")]
            public Nullable<System.DateTime> PromotionClose { get; set; }
        }
    }
}