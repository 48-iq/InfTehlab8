using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InfTehLab8.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> products {  get; set; }
        public DbSet<OrderPosition> orderPositions { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}