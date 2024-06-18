using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }
        [ForeignKey("MaHD")]
        public Order Order  { get; set; }
        [ForeignKey("MaSP")]
         
        public Product Product { get; set; }
    }
}
