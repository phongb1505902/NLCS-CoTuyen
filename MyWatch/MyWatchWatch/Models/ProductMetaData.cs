using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWatchWatch.Models
{
    [MetadataType(typeof(Product.ProductMetaData))]
    public partial class Product
    {
        internal sealed class ProductMetaData
        {
            [Display(Name = "ID")]
            public int ProductId { get; set; }

            [Display(Name = "Name")]
            [Required(ErrorMessage = "Record can not null")]
            public string ProductName { get; set; }

            [Display(Name = "Details")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.MultilineText)]
            public string ProductDetails { get; set; }

            [Display(Name = "Status")]
            public Nullable<bool> ProductStatus { get; set; }

            [Display(Name = "Data")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.DateTime, ErrorMessage = "Input is data type")]
            [Required(ErrorMessage = "Record can not null")]
            public Nullable<System.DateTime> ProductUpdate { get; set; }

            [Display(Name = "Quantity")]
            [Required(ErrorMessage = "Record can not null")]
            public Nullable<int> ProductQty { get; set; }

            [Display(Name = "Price")]
            [Required(ErrorMessage = "Record can not null")]
            [DataType(DataType.Currency,ErrorMessage ="Input is currency")]
            public Nullable<decimal> ProductSold { get; set; }
        }
    }
  
}