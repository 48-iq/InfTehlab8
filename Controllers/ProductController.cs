using InfTehLab8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfTehLab8.Controllers
{
    public class ProductController : Controller
    {
        private string login = "admin";
        private string password = "admin";

        private StoreContext db = new StoreContext();
        // GET: Product
        public ActionResult all()
        {
            Order order = (Order)Session["order"];
            if (order == null)
            {
                order = new Order();
            }
            ViewBag.Order = order;
            return View(db.products.ToList());
        }
        [HttpGet]
        public ActionResult adminLogin()
        {
            return View();
        }
        
        public ActionResult loginProcess(AdminDto adminDto) 
        {
            Session["login"] = adminDto.login;
            Session["password"] = adminDto.password;
            if (adminDto.login != login || adminDto.password != password) 
            {
                return Redirect("/Error/Index?message=Неправильный логин или пароль");
            }
            else
            {
                List<Order> orders = db.orders.ToList();
                foreach (Order order in orders)
                {
                    order.OrderPositions = db.orderPositions.Where(o => o.OrderId == order.Id).ToList();
                }
                ViewBag.Orders = orders;
                return View("adminPage", db.products.ToList());
            }
        }

        [HttpPost]
        public ActionResult save(Product newProduct)
        {   
            if ((string)Session["login"] != login || (string)Session["password"] != password)
            {
                return Redirect("/Error/Index?message=вы не имеете доступа к этому ресурсу");
            }
            if (newProduct.Id == null)
            {

                db.products.Add(newProduct);
                db.SaveChanges();
            }
            else
            {
                Product product = db.products.Find(newProduct.Id);
                product.Name = newProduct.Name;
                product.Description = newProduct.Description;
                product.Price = newProduct.Price;
                product.CountInStore = newProduct.CountInStore;
                db.SaveChanges();
            }
            List<Order> orders = db.orders.ToList();
            foreach (Order order in orders)
            {
                order.OrderPositions = db.orderPositions.Where(o => o.OrderId == order.Id).ToList();
            }
            ViewBag.Orders = orders;
            return View("adminPage", db.products.ToList());
        }

        public ActionResult delete(int id)
        {
            if ((string)Session["login"] != login || (string)Session["password"] != password)
            {
                return Redirect("/Error/Index?message=вы не имеете доступа к этому ресурсу");
            }
            db.products.Remove(db.products.Find(id));
            db.SaveChanges();
            List<Order> orders = db.orders.ToList();
            foreach (Order order in orders)
            {
                order.OrderPositions = db.orderPositions.Where(o => o.OrderId == order.Id).ToList();
            }
            ViewBag.Orders = orders;
            return View("adminPage", db.products.ToList());
        }
    }
}