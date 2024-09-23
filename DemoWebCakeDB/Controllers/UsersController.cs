using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWebCakeDB.Models;

namespace DemoWebCakeDB.Controllers
{
    public class UsersController : Controller
    {
        private QLHOAEntities database = new QLHOAEntities();
        // GET: Users
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.UserName))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên không được để trống");
                if (string.IsNullOrEmpty(cust.EmailCus))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(cust.PhoneCus))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");

                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa
                var khachhang = database.Customers.FirstOrDefault(k =>
                k.UserName == cust.UserName);
                var idkhachhang = database.Customers.FirstOrDefault(k =>
               k.IDCus == cust.IDCus);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên đăng nhập này");
                if (idkhachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí id này");
                if (ModelState.IsValid)
                {
                    database.Customers.Add(cust);
                    database.SaveChanges();
                    Session["UserID"] = cust.IDCus;
                    Session["UserName"] = cust.UserName;

                    return RedirectToAction("UserAccount", "Users");
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["UserID"] != null)
            {
                // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
                return RedirectToAction("UserAccount", "Users");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer cust)
        {
            var check = database.Customers.Where(s => s.UserName == cust.UserName && s.PassCus == cust.PassCus).FirstOrDefault();
            if (check == null)  //không có KH
            {
                ViewBag.ErrorInfo = "Không có KH này";
                return View("Login");
            }
            else
            {   // Có tồn tại KH -> chuẩn bị dữ liệu đưa về lại ShowCart.cshtml
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["IDCus"] = check.IDCus;
                Session["Passwod"] = check.PassCus;
                Session["NameCus"] = check.NameCus;
                Session["PhoneCus"] = check.PhoneCus;
                Session["UserID"] = check.UserName;
                // Quay lại trang giỏ hàng với thông tin cần thiết
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            /*if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    /Tìm khách hàng có tên đăng nhập và pass hợp lệ trong CSDL
                    var khachhang = database.Customers.FirstOrDefault(k => k.NameCus == cust.NameCus && k.PassCus == cust.PassCus);
                    if (khachhang != null)
                    {
                        da.Configuration.ValidateOnSaveEnabled = false;
                        Session["IDCus"] = khachhang.IDCus;
                        Session["Passwod"] = khachhang.PassCus;
                        Session["NameCus"] = khachhang.NameCus;
                        Session["PhoneCus"] = khachhang.PhoneCus;
                        // Quay lại trang giỏ hàng với thông tin cần thiết
                        return RedirectToAction("ShowCart", "ShoppingCart");

                        //Luw vào ssssion
                        //Session["TaiKhoan"] = khachhang;
                        //return RedirectToAction("Home", "CustomerProducts");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    // check là khách hàng cần tìm
                    var check = database.Customers.Where(s => s.NameCus == cust.NameCus && s.PassCus == cust.PassCus).FirstOrDefault();
                    if (check == null)  //không có KH
                    {
                        ViewBag.ErrorInfo = "Không có KH này";
                        return View("LoginCus");
                    }
                    else
                    {   // Có tồn tại KH -> chuẩn bị dữ liệu đưa về lại ShowCart.cshtml
                        database.Configuration.ValidateOnSaveEnabled = false;
                        Session["IDCus"] = check.IDCus;
                        Session["Passwod"] = check.PassCus;
                        Session["NameCus"] = check.NameCus;
                        Session["PhoneCus"] = check.PhoneCus;
                        // Quay lại trang giỏ hàng với thông tin cần thiết
                        return RedirectToAction("ShowCart", "ShoppingCart");
                    }
                }
            }*/
            //return View();
        }
        public ActionResult UserAccount()
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (Session["UserID"] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login", "Users");
            }

            // Nếu đã đăng nhập, hiển thị trang tài khoản của người dùng
            // Lấy thông tin cần thiết từ Session để hiển thị trên trang
            ViewBag.UserName = Session["UserName"];
            return View();
        }
        public ActionResult LogOut() //LOGOUT
        {
            Session.Abandon();
            return RedirectToAction("Login", "Users");
        }
    }
}