using InfTehLab8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfTehLab8.Controllers
{
    public class OrderController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add(OrderPositionDto orderPositionDto)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Product/All");
            }
            if (Session["order"] == null)
            {
                Session["order"] = new Order();
            }
            Order order = (Order)Session["order"];
            OrderPosition orderPosition = order.OrderPositions
                .Find(o => o.Product.Id == orderPositionDto.ProductId);
            if (orderPosition == null)
            {
                orderPosition = new OrderPosition();
                orderPosition.Product = db.products.Find(orderPositionDto.ProductId);
                order.OrderPositions.Add(orderPosition);
                Debug.WriteLine(orderPosition.Product);
            }
            orderPosition.CountInOrder = orderPositionDto.ProductCount;
            Session["order"] = order;
            return Redirect("/Product/all");
        }
        [HttpGet]
        public ActionResult submit()
        {
            if (Session["order"] == null)
            {
                return Redirect("/Error/Index?message=Пустой заказ");
            }
            return View((Order)Session["order"]);
        }

        [HttpPost]
        public ActionResult submit(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
     
                return Redirect("/Order/submit");
            }
            if (Session["order"] == null)
            {
                return Redirect("/Error/Index?message=Пустой заказ");
            }

            Order order = (Order)Session["order"];
            order.ClientEmail = orderDto.ClientEmail;
            order.ClientPhone = orderDto.ClientPhone;
            order.ClientName = orderDto.ClientName;
            order.ClientSurname = orderDto.ClientSurname;

            foreach (OrderPosition orderPosition in  order.OrderPositions)
            {
                Product product = db.products.Find(orderPosition.Product.Id);
                if (product.CountInStore < orderPosition.CountInOrder)
                {
                    return Redirect("/Error/Index?message=На складе нет необходимого количества " + product.Name);
                }
            }

            foreach(OrderPosition orderPosition in order.OrderPositions)
            {
                Product product = db.products.Find(orderPosition.Product.Id);
                product.CountInStore -= orderPosition.CountInOrder;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            List<OrderPosition> orderPositions = order.OrderPositions;
            order.OrderPositions = null;
            order = db.orders.Add(order);
            db.SaveChanges();
            foreach (OrderPosition position in orderPositions)
            {
                position.Order = order;
                position.OrderId = order.Id;
                position.ProductId = position.Product.Id;
                db.orderPositions.Add(position);
            }
            db.SaveChanges();
            Session["order"] = null;

            return View("successSubmit");
        }
    }

}