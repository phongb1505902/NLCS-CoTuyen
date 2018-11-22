using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWatchWatch.Models
{
    [MetadataType(typeof(Delivery.DelieveryMetaData))]
    public partial class Delivery
    {
        internal sealed class DelieveryMetaData
        {
            [Display(Name = "ID")]
            [Required(ErrorMessage = "Record can not null")]
            public int DeliveryId { get; set; }

            [Display(Name = "Title")]
            [Required(ErrorMessage = "Record can not null")]
            public string DeliveryTitle { get; set; }

            [Display(Name = "IMG")]
            public string ImgDelivery { get; set; }

            [Display(Name = "Context")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.MultilineText)]
            public string DeliveryDetails { get; set; }

            [Display(Name = "Question")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.MultilineText)]
            public string DeliveryQuestion { get; set; }
        }
    
    
    }
}