using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWebCakeDB.Models;
using System.Data.SqlClient;
namespace DemoWebCakeDB.Controllers
{
    public class ShoppingCartController : Controller
    {
        QLHOAEntities db = new    QLHOAEntities();
        // GET: ShoppingCart
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("ShowCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        // Tạo mới giỏ hàng, nguồn được lấy từ Session
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        // Thêm sản phẩm vào giỏ hàng
        public ActionResult AddToCart(int id)
        {
            // lấy sản phẩm theo id
            var _pro = db.Products.SingleOrDefault(s => s.ProductID == id);
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        // Cập nhật số lượng và tính lại tổng tiền
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(Request.Form["idPro"]);
            int _quantity = int.Parse(Request.Form["carQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        // Xóa dòng sản phẩm trong giỏ hàng
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        // Các phương thức cho thanh toán
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                OrderPro _order = new OrderPro();
                Random random = new Random();
                int a=random.Next(10,100);
                while (db.OrderPros.Any(op => op.ID == a)==true)
                {
                    a = random.Next();
                }
                _order.ID = a;
                _order.DateOrder = DateTime.Now;
                _order.AddressDelivery = form["AddressDeliverry"];
                _order.IDCus = int.Parse(form["CodeCustomer"]);
                
                db.OrderPros.Add(_order);
                db.SaveChanges(); // Lưu để có ID hợp lệ cho OrderPro

                foreach (var item in cart.Items)
                {
                    int b = random.Next(1, 20);
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.ID = b;
                    _order_detail.IDOrder = _order.ID; // Sử dụng ID mới tạo từ OrderPro
                    _order_detail.IDProduct = item._product.ProductID;
                    _order_detail.UnitPrice = (double)item._product.Price * item._quantity;
                    _order_detail.Quantity = item._quantity;

                    db.OrderDetails.Add(_order_detail);
                }

                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Có sai sót! Xin kiểm tra lại thông tin");
            }
        }
        // Thông báo thanh toán thành công
        public ActionResult CheckOut_Success()
        {
            return View();
        }   
    }
}