using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Helper;

using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        //Hiển thị giỏ hàng
        public IActionResult Index()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (cart == null)
            {
                cart = new Cart();
            }
            return View(cart);
        }
        //Xử lý thêm sản phẩm vào giỏ
        public IActionResult AddToCart(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("CART");
                if (cart == null)
                {
                    cart = new Cart();
                }
                cart.Add(product, 1);
                HttpContext.Session.SetJson("CART", cart);
                // return Json(new { msg="success", qty = cart.Quantity});
                TempData["success"] = "Product added to cart";
                return RedirectToAction("Index", "Home");
            }
            return Json(new { msg = "error" });
        }
        //Xử lý Cập nhật sản phẩm trong giỏ hàng
        public IActionResult Update(int productId, int qty)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("CART");
                if (cart != null)
                {
                    cart.Update(productId, qty);//Cập nhật lại số lượng sp
                    HttpContext.Session.SetJson("CART", cart);//Lưu Cart vào session
                    return RedirectToAction("Index");
                }
            }
            return Json(new { msg = "error" });
        }
        //Xử lý Xóa sản phẩm trong giỏ hàng
        public IActionResult Remove(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                //Lấy cart từ session
                Cart cart = HttpContext.Session.GetJson<Cart>("CART");
                if (cart != null)
                {
                    cart.Remove(productId);//Xóa sp khỏi giỏ hàng
                    HttpContext.Session.SetJson("CART", cart);//Lưu Cart vào session
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        #region API
        public IActionResult AddToCartAPI(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("CART");
                if (cart == null)
                {
                    cart = new Cart();
                }
                cart.Add(product, 1);
                HttpContext.Session.SetJson("CART", cart);
                return Json(new { msg = "Product added to cart", qty = cart.Quantity });
            }
            return Json(new { msg = "error" });
        }

        public IActionResult GetQuantityOfCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (cart != null)
            {
                return Json(new { qty = cart.Quantity });
            }
            return Json(new { qty = 0 });
        }

        #endregion
    }
}
