using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Helper;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //Lấy cart từ session
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (cart == null)
            {
                cart = new Cart();
            }
            //Gửi cart qua view thông qua viewbag
            ViewBag.CART = cart;
            return View();
        }
        public IActionResult ProcessOrder(Order order)
        {
            //B1.Lưu trữ đơn hàng

            //B2.Xóa giỏ hàng 
            HttpContext.Session.Remove("CART");
            //Trả về view hiển thị kết quả
            return View("Result");
        }

    }
}
