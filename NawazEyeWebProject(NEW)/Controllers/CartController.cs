using System;
using System.Collections.Generic;
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
        [HttpGet]
        public ActionResult Index(bool promoValidation = false)
        {
            try
            {
                if (promoValidation)
                {
                    ViewBag.Message = "This Promo Code is already used by you.";
                }
                Cart c = new Account(User.Identity.GetUserId()).Buyer.GetCurrentCart();
                ViewCartViewModel vcvm = new ViewCartViewModel();
                if (c == null)
                {
                    vcvm.IsCart = false;
                    return View(vcvm);
                }
                else
                {
                    vcvm.IsCart = true;
                    vcvm.Id = c.CartId.ToString();
                    vcvm.DeliveryCharges = decimal.Round(c.Buyer.City.DeliverCharges).ToString();
                    foreach (var item in c.PrescriptionGlasses)
                    {
                        if (vcvm.ItemsInCart == null)
                        {
                            vcvm.ItemsInCart = new List<ProductListViewModel>();
                        }
                        vcvm.ItemsInCart.Add(new ProductListViewModel()
                        {
                            ItemId = item.PrescriptionGlasses.ProductId,
                            Name = item.PrescriptionGlasses.Name,
                            Price = decimal.Round(item.PrescriptionGlasses.Price).ToString(),
                            Quantity = item.Quantity.ToString(),
                            Image = item.PrescriptionGlasses.PrimaryImage
                        });
                    }
                    foreach (var item in c.Sunglasses)
                    {
                        if (vcvm.ItemsInCart == null)
                        {
                            vcvm.ItemsInCart = new List<ProductListViewModel>();
                        }
                        vcvm.ItemsInCart.Add(new ProductListViewModel()
                        {
                            ItemId = item.Sunglasses.ProductId,
                            Name = item.Sunglasses.Name,
                            Price = decimal.Round(item.Sunglasses.Price).ToString(),
                            Quantity = item.Quantity.ToString(),
                            Image = item.Sunglasses.PrimaryImage
                        });
                    }
                    return View(vcvm);
                }
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "Index");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }

        [HttpGet]
        public ActionResult AddToCart(int id, string urlRedirect = null)
        {
            try
            {
                ViewBag.Message = urlRedirect;
                if (Product.IsSunglasses(id))
                {
                    Sunglasses s = new Sunglasses(id);
                    OrderSunglassesViewModel osvm = new OrderSunglassesViewModel()
                    {
                        DeliveryCharges = decimal.Round(new Account(User.Identity.GetUserId()).Buyer.City.DeliverCharges).ToString(),
                        DiscountedPrice = decimal.Round(s.GetDiscountedPrice()).ToString(),
                        Id = s.ProductId.ToString(),
                        Images = s.Images,
                        Name = s.Name,
                        Price = decimal.Round(s.Price).ToString(),
                        Quantity = s.Quantity,
                        Status = s.Quantity + " Item(s) available"
                    };
                    return View("OrderSunglasses", osvm);
                }
                else if (Product.IsPrescriptionGlasses(id))
                {
                    PrescriptionGlasses p = new PrescriptionGlasses(id);
                    OrderPrescriptionGlassesViewModel opvm = new OrderPrescriptionGlassesViewModel()
                    {
                        DeliveryCharges = decimal.Round(new Account(User.Identity.GetUserId()).Buyer.City.DeliverCharges).ToString(),
                        DiscountedPrice = decimal.Round(p.GetDiscountedPrice()).ToString(),
                        Id = p.ProductId.ToString(),
                        Images = p.Images,
                        Lens = p.Lens.LensName,
                        Name = p.Name,
                        Price = decimal.Round(p.Price).ToString(),
                        Quantity = p.Quantity,
                        Status = p.Quantity + " Item(s) available"
                    };
                    return View("OrderPrescriptionGlasses", opvm);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "AddToCart");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Order(int id)
        {
            try
            {
                if (Request.IsAuthenticated)
                {
                    string url = Request.UrlReferrer.AbsoluteUri;
                    return RedirectToAction("AddToCart", new { id = id, urlRedirect = url });
                }
                else
                {
                    ViewBag.Tag = id.ToString();
                    return View("OrderAnonymous");
                }
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "Order");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AnonymousOrderBuyerDetailsSubmission(BuyersInfoViewModel model, string id)
        {
            try
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
                        ViewBag.Message = null;
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
                        ViewBag.Message = null;
                        return View("OrderSunglasses", osvm);
                    }
                }
                else
                {
                    return View("OrderAnonymous", model);
                }
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "AnonymousOrderBuyerDetailsSubmission");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPrescriptionGlasses(OrderPrescriptionGlassesViewModel model, string urlRedirect = null)
        {
            try
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
                    string path = Server.MapPath(ConfigurationManager.AppSettings["PrescriptionsPath"] + fileName);
                    model.Prescription.SaveAs(path);
                    p.Quantity -= model.Quantity;
                    if (urlRedirect == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(urlRedirect);
                    }
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
                    if (urlRedirect != null && Request.IsAuthenticated)
                    {
                        return Redirect(urlRedirect);
                    }
                    else if (Request.IsAuthenticated)
                    {
                        return RedirectToAction("Index", "Home");

                    }
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
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "OrderPrescriptionGlasses");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult OrderSunglasses(OrderSunglassesViewModel model, string urlRedirect = null)
        {
            try
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
                    if (urlRedirect != null)
                    {
                        return Redirect(urlRedirect);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    Cart c = new Cart(buyer);
                    Sunglasses s = new Sunglasses(productId);
                    c.AddSunglasses(s, model.Quantity);
                    s.Quantity -= model.Quantity;
                    if (urlRedirect != null)
                    {
                        return Redirect(urlRedirect);
                    }
                    else if (Request.IsAuthenticated)
                    {
                        return RedirectToAction("Index", "Home");
                    }
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
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "OrderSunglasses");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderUser(string promo, int cartId)
        {
            try
            {
                Buyer b = new Account(User.Identity.GetUserId()).Buyer;
                Cart c = new Cart(cartId);
                Order o;
                if (promo == null || promo == "")
                {
                    o = new Order(c, DateTime.Now);
                    c.IsCurrent = false;
                }
                else
                {
                    PromoCode p = new PromoCode(promo);
                    if (p.IsUsed(b.GetAccount()))
                    {
                        return RedirectToAction("Index", new { promoValidation = true });
                    }
                    o = new Order(c, DateTime.Now, p);
                    c.IsCurrent = false;
                }
                OrderSuccessViewModel osvm = new OrderSuccessViewModel()
                {
                    BuyersName = b.Name,
                    DeliveryCharges = decimal.Round(b.City.DeliverCharges).ToString(),
                    DiscountAvailed = o.Promo.Discount + "%",
                    OrderId = o.OrderId.ToString(),
                    Status = o.Status,
                    TotalPrice = decimal.Round(o.TotalPrice).ToString()
                };
                return View("OrderSuccess", osvm);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "OrderUser");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        public ActionResult DeleteCartItem(int itemId, int cartId, int quantity)
        {
            try
            {
                Cart c = new Cart(cartId);
                c.DeleteItem(itemId);
                Product p = new Product(itemId);
                p.Quantity += quantity;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Cart", "DeleteCartItem");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
    }
}