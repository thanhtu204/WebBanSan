using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Models
{

    //Lớp biểu diễn 1 phần tử của giỏ hàng 
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
    //Lớp biểu diễn giỏ hàng 
    public class Cart
    {
        private List<CartItem> _items;
        public Cart()
        {
            _items = new List<CartItem>();
        }

        public List<CartItem> Items { get { return _items; } }
        //Các phương thức xử lý trên giỏ hàng
        //Phương thức thêm sp vào giỏ
        public void Add(Product p, int qty)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == p.Id);
            if (item == null)//Chưa có
            {
                _items.Add(new CartItem { Product = p, Quantity = qty });
            }
            else
            {
                item.Quantity += qty;
            }
        }
        //Phương thức cập nhật số lượng
        public void Update(int productId, int qty)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)//Tồn tại
            {
                if (qty > 0)
                {
                    item.Quantity = qty;
                }
                else
                {
                    _items.Remove(item);
                }
            }
        }
        //Phương thức xóa số lượng
        internal void Remove(int productId)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)//Tồn tại
            {
                _items.Remove(item);
            }
        }

        //Phương thức tính tổng thành tiền
        public double Total
        {
            get
            {
                double total = _items.Sum(x => x.Quantity * x.Product.Price);
                return total;
            }
        }
        //Phương thức tính tổng số lượng của giỏ
        public double Quantity
        {
            get
            {
                double total = _items.Sum(x => x.Quantity);
                return total;
            }
        }

    }
}

