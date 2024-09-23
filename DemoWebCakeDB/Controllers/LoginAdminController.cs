using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWebCakeDB.Models;

namespace DemoWebCakeDB.Controllers
{
    public class LoginAdminController : Controller
    {
        QLHOAEntities db = new QLHOAEntities();
        // GET: LoginAdmin
        // Phương thức tạo view cho Login
        public ActionResult Index()
        {
            return View();
        }
        // Xử lý tìm kiếm ID, password trong AdminUser và thông báo
        [HttpPost]
        public ActionResult LoginAcount(AdminUser _user)
        {
            var check = db.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai Info";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = _user.NameUser;
                Session["PasswodUser"] = _user.PasswordUser;
                return RedirectToAction("Index", "Products");
            }
        }
        // Regíter
        [HttpGet]
        public ActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterAdmin(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var CheckID = db.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if (CheckID == null) //chua co id
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.AdminUsers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "ID này đã có rồi !!!";
                    return View();
                }
            }
            return View();
        }


    }
}