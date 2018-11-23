using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWatchWatch.Models;

namespace MyWatchWatch.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        MyWatchWatchEntities db = new MyWatchWatchEntities();
        //Lấy giỏ hàng
        public List<ItemGioHang> LayGioHang()
        {
            // Giỏ hàng đã tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        //Thêm giỏ hàng thông thường load lại trang

        public ActionResult ThemGioHang(int ProductId, String strUrl)
        {
            // Kiểm tra sản phẩm có tồn tại trong CSLD hay không?
            Product sp = db.Products.SingleOrDefault(n => n.ProductId == ProductId);
            if (sp == null)
            {
                // Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
            }
            // Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();

            //TH1 sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.ProductId == ProductId);
            
            if (spCheck != null)
            {
                if (sp.ProductStatus == false)
                {
                    return View("ThongBao");
                }
                spCheck.ProductQtyUser++;
                spCheck.ThanhTien = spCheck.ProductQtyUser * spCheck.ProductSold;
                return Redirect(strUrl);

            }
            if (sp.ProductStatus == false)
            {
                return View("ThongBao");
            }
            ItemGioHang itemGH = new ItemGioHang(ProductId);
            lstGioHang.Add(itemGH);
            return Redirect(strUrl);

        }

        public double TinhTongSoLuong()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ProductQtyUser);
        }
        public decimal TinhTongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);

        }

        // GET: ShoppingCart
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> giohang = Session["GioHang"] as List<ItemGioHang>;
            return View(giohang);
        }
        public ActionResult ShoppingPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();

        }
        public ActionResult SuaSoLuong(int ProductId, int soluongmoi)
        {
            // tìm carditem muon sua
            List<ItemGioHang> giohang = Session["GioHang"] as List<ItemGioHang>;
            ItemGioHang itemSua = giohang.FirstOrDefault(m => m.ProductId == ProductId);
            if (itemSua != null)
            {
                itemSua.ProductQtyUser = soluongmoi;
                itemSua.ThanhTien = itemSua.ProductQtyUser * itemSua.ProductSold;
            }
            return RedirectToAction("XemGioHang");
        }
        public RedirectToRouteResult XoaKhoiGio(int ProductId)
        {
            List<ItemGioHang> giohang = Session["GioHang"] as List<ItemGioHang>;
            ItemGioHang itemXoa = giohang.FirstOrDefault(m => m.ProductId == ProductId);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("XemGioHang");
        }

        public ActionResult ConfirmCheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmCheckOut([Bind(Include = "OrderAddress,OrderPhone")] Order order)
        {
            if (ModelState.IsValid)
            {
                var userId = Session["username"];
                order.OrderDate = DateTime.Now;
                order.CustomerCode = Convert.ToString(userId);
                //order.UserId = null;
                order.Order_Status = false;
                db.Orders.Add(order);
                db.SaveChanges();
                // Them chi tiet
                List<ItemGioHang> lstGH = LayGioHang();
                foreach (var item in lstGH)
                {
                    var details = new OrderDetail();
                    details.OrderId = order.OrderId;
                    details.ProductId = item.ProductId;
                    details.Quantity = item.ProductQtyUser;
                    details.SoldPrice = item.ProductSold;
                    db.OrderDetails.Add(details);

                }
                db.SaveChanges();
                //foreach (var item in lstGH)
                //{
                //    var lst = db.Products.SingleOrDefault(u => u.ProductId == item.ProductId);
                //    lst.ProductQty = lst.ProductQty - item.ProductQtyUser;
                //}
                //db.SaveChanges();
                Session["GioHang"] = null;
                return RedirectToAction("Susses", "Cart");
            }
            return View();
        }
        public ActionResult Susses()
        {
            return View();
        }
    }

}

