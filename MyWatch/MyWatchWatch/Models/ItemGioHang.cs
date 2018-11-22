using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWatchWatch.Models;
namespace MyWatchWatch.Models
{
    public class ItemGioHang
    {
        public Product product { set; get; }
        public ImgProduct ImgProduct { set; get; }
        public int ProductQtyUser { set; get; }
        public int ProductId { set; get; }
        public decimal ProductSold { get; set; }
        public List<ImgProduct> ImgPro { get; set; }
        public String ProductName { get; set; }
        public Decimal ThanhTien { get; set; }
        public int PromotionId { get; set; }
        public int ProductQty { get; set; }

        public ItemGioHang(int ProductId)
        {
            using (MyWatchWatchEntities db = new MyWatchWatchEntities())
            {
                Product pro = db.Products.Include("ImgProducts").Single(x => x.ProductId == ProductId);
                
                this.ProductId = ProductId;
                this.ProductQtyUser = 1;
                this.ProductName = pro.ProductName;                
                this.ImgPro = pro.ImgProducts.ToList();
                this.ProductQty = pro.ProductQty.Value;
                if (pro.PromotionId == null)
                {
                    this.ProductSold = pro.ProductSold.Value;
                }
                else
                {
                    Promotion motion = db.Promotions.SingleOrDefault(x => x.PromotionId == pro.PromotionId);
                    if (motion.PromotionDiscount == 0 || motion.PromotionDiscount == null)
                    {
                        this.ProductSold = pro.ProductSold.Value;
                    }
                    else
                    {
                        this.ProductSold = pro.ProductSold.Value - ((pro.ProductSold.Value * pro.Promotion.PromotionDiscount.Value) / 100);
                    }
                }
                this.ThanhTien = this.ProductSold * this.ProductQtyUser;
            }
        }
    }
}