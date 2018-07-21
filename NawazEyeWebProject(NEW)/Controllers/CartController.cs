using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NawazEyeWebProject_NEW_.ViewModels;
using NawazEyeWebProject_NEW_.Models;
using System.IO;
using System.Configuration;

namespace NawazEyeWebProject_NEW_.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Order(int id)
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                ViewBag.Tag = id.ToString();
                return View("OrderAnonymous");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AnonymousOrderBuyerDetailsSubmission(BuyersInfoViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                City c = new City(Convert.ToInt32(model.CityName));
                Buyer buyer = new Buyer(model.FullName, model.PhoneNumber, model.Address, model.Email, c);
                HttpCookie anonymusBuyer = new HttpCookie("anonBuyer");
                anonymusBuyer.Value = buyer.BuyerId.ToString();
                anonymusBuyer.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(anonymusBuyer);
                int productId = Convert.ToInt32(id);
                if (Product.IsPrescriptionGlasses(productId))
                {
                    PrescriptionGlasses p = new PrescriptionGlasses(productId);
                    OrderPrescriptionGlassesViewModel opvm = new OrderPrescriptionGlassesViewModel()
                    {
                        DiscountedPrice = decimal.Round(p.GetDiscountedPrice()).ToString(),
                        Id = p.ProductId.ToString(),
                        Images = p.Images,
                        Lens = p.Lens.LensName,
                        Name = p.Name,
                        Price = decimal.Round(p.Price).ToString(),
                        DeliveryCharges = decimal.Round(buyer.City.DeliverCharges).ToString(),
                        Status = p.Quantity + " Item(s) available",
                        Quantity = p.Quantity
                    };
                    return View("OrderPrescriptionGlasses", opvm);
                }
                else
                {
                    Sunglasses s = new Sunglasses(productId);
                    OrderSunglassesViewModel osvm = new OrderSunglassesViewModel()
                    {
                        DiscountedPrice = decimal.Round(s.GetDiscountedPrice()).ToString(),
                        Id = s.ProductId.ToString(),
                        Images = s.Images,
                        Name = s.Name,
                        Price = decimal.Round(s.Price).ToString(),
                        Status = s.Quantity + " Item(s) available",
                        DeliveryCharges = decimal.Round(buyer.City.DeliverCharges).ToString(),
                        Quantity = s.Quantity

                    };
                    return View("OrderSunglasses", osvm);
                }
            }
            else
            {
                return View("OrderAnonymous", model);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPrescriptionGlasses(OrderPrescriptionGlassesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Buyer buyer;
            if (Request.IsAuthenticated)
            {
                buyer = new Account(User.Identity.GetUserId()).Buyer;
            }
            else if (Request.Cookies["anonBuyer"] != null)
            {
                buyer = new Buyer(Convert.ToInt32(Request.Cookies["anonBuyer"].Value));
                Response.Cookies.Remove("anonBuyer");
            }
            else
            {
                //return View which says that buyer's Session is timedout
                return View("TimedOut");
            }
            string fileName = Guid.NewGuid() + Path.GetFileName(model.Prescription.FileName);
            int productId = Convert.ToInt32(model.Id);
            if (buyer.GetCurrentCart() != null)
            {
                Cart c = buyer.GetCurrentCart();
                PrescriptionGlasses p = new PrescriptionGlasses(productId);
                c.AddPrescriptionglasses(p, model.Quantity, fileName, model.Lens);
                string path = ConfigurationManager.AppSettings["PrescriptionsPath"] + fileName;
                model.Prescription.SaveAs(path);
                p.Quantity -= model.Quantity;
                return RedirectToAction("Index");
            }
            else
            {
                Cart c = new Cart(buyer);
                PrescriptionGlasses p = new PrescriptionGlasses(productId);
                c.AddPrescriptionglasses(p, model.Quantity, fileName, model.Lens);
                var url = Server.MapPath(ConfigurationManager.AppSettings["PrescriptionsPath"]);
                string path = url + fileName;
                model.Prescription.SaveAs(path);
                p.Quantity -= model.Quantity;
                Order order = new Order(c, DateTime.Now);
                OrderSuccessViewModel osvm = new OrderSuccessViewModel()
                {
                    BuyersName = buyer.Name,
                    DeliveryCharges = decimal.Round(buyer.City.DeliverCharges).ToString(),
                    DiscountAvailed = order.Promo.Discount + "%",
                    OrderId = order.OrderId.ToString(),
                    Status = order.Status,
                    TotalPrice = decimal.Round(order.TotalPrice).ToString()
                };
                return View("OrderSuccess", osvm);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult OrderSunglasses(OrderSunglassesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Buyer buyer;
            if (Request.IsAuthenticated)
            {
                buyer = new Account(User.Identity.GetUserId()).Buyer;
            }
            else if (Request.Cookies["anonBuyer"] != null)
            {
                buyer = new Buyer(Convert.ToInt32(Request.Cookies["anonBuyer"].Value));
                Response.Cookies.Remove("anonBuyer");
            }
            else
            {
                //return View which says that buyer's Session is timedout
                return View("TimedOut");
            }
            int productId = Convert.ToInt32(model.Id);
            if (buyer.GetCurrentCart() != null)
            {
                Cart c = buyer.GetCurrentCart();
                Sunglasses s = new Sunglasses(productId);
                c.AddSunglasses(s, model.Quantity);
                s.Quantity -= model.Quantity;
                return RedirectToAction("Index");
            }
            else
            {
                Cart c = new Cart(buyer);
                Sunglasses s = new Sunglasses(productId);
                c.AddSunglasses(s, model.Quantity);
                s.Quantity -= model.Quantity;
                Order order = new Order(c, DateTime.Now);
                OrderSuccessViewModel osvm = new OrderSuccessViewModel()
                {
                    BuyersName = buyer.Name,
                    DeliveryCharges = decimal.Round(buyer.City.DeliverCharges).ToString(),
                    DiscountAvailed = order.Promo.Discount + "%",
                    OrderId = order.OrderId.ToString(),
                    Status = order.Status,
                    TotalPrice = decimal.Round(order.TotalPrice).ToString()
                };
                return View("OrderSuccess", osvm);
            }
        }
    }
}