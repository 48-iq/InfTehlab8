using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfTehLab8.Models
{
    public class OrderPosition
    {
        public int? Id {  get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int? CountInOrder { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}