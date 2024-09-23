using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWebCakeDB.Models;
using System.Net;
using PagedList;
using System.Data.Entity;
namespace DemoWebCakeDB.Controllers
{
    public class CustomerProductsController : Controller
    {
        private QLHOAEntities db = new QLHOAEntities();
        // GET: CustomerProducts
        public ActionResult Index(int? page, string SearchString, double min = double.MinValue, double max = double.MaxValue)
        {
            var products = db.Products.Include(p => p.Category); //cũ là xóa .include và if
            //Tìm kiếm chuỗi truy vấn theo NamePro, nếu chuỗi truy vấn SearchString khác rỗng, null
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.NamePro.Contains(SearchString));
            }
            // Tìm kiếm chuỗi truy vấn theo đơn giá
            if (min >= 0 && max > 0)
            {
                // Cập nhật truy vấn hiện tại thay vì tạo một truy vấn mới.
                products = products.OrderByDescending(x => x.Price).Where(p => (double)p.Price >= min && (double)p.Price <= max);
            }
            // Khai báo mỗi trang 4 sản phẩm
            int pageSize = 4;
            // Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // Nếu page = null thì đặt lại page là 1.
            if (page == null) page = 1;

            // Trả về các product được phân trang theo kích thước và số trang.
            //return View(products.ToPagedList(pageNumber,pageSize));
            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
                HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var otherProducts = db.Products
            .Where(p => p.ProductID != id)
            .OrderBy(x => Guid.NewGuid()) // Đảm bảo lấy ngẫu nhiên
            .Take(4)
            .ToList();
            ViewBag.OtherProducts = otherProducts;

            return View(product);
        }

        public ActionResult GetProductsByCategory()
        {
            var categories = db.Categories.ToList();
            return PartialView("CategoriesPartialView", categories);
        }

        public ActionResult GetProductsByCateId(int id)
        {
            var products = db.Products.Where(p => p.Category.CategoryId ==id).ToList();
            return View("Index", products);
        }

        public ActionResult Home()
        {
            var products=db.Products.ToList();
            return View(products);
        }
    }

}