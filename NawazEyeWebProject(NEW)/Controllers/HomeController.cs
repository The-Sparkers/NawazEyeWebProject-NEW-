﻿using NawazEyeWebProject_NEW_.Models;
using NawazEyeWebProject_NEW_.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NawazEyeWebProject_NEW_.Controllers
{
    public class HomeController : Controller
    { 
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                HomeViewModel model = new HomeViewModel();
                List<HotItemsViewModel> lst = new List<HotItemsViewModel>();
                foreach (var item in Product.GetHotItems())
                {
                    HotItemsViewModel hvm = new HotItemsViewModel()
                    {
                        Id = item.ProductId.ToString(),
                        Image = item.PrimaryImage,
                        Name = item.Name

                    };
                    lst.Add(hvm);
                }
                model.HotItems = lst;
                List<FeaturedSunglassesViewModel> lstSunglasses = new List<FeaturedSunglassesViewModel>();
                foreach (var item in Sunglasses.FeaturedSunglasses())
                {
                    FeaturedSunglassesViewModel fsvm = new FeaturedSunglassesViewModel()
                    {
                        Id = item.ProductId.ToString(),
                        Name = item.Name,
                        Image = item.PrimaryImage,
                        Price = decimal.Round(item.Price).ToString()
                    };
                    lstSunglasses.Add(fsvm);
                }
                model.PopSunglasses = lstSunglasses;
                List<FeaturedPrescriptionGlassesViewModel> lstPrescription = new List<FeaturedPrescriptionGlassesViewModel>();
                foreach (var item in PrescriptionGlasses.FeaturedPrescriptionGlasses())
                {
                    FeaturedPrescriptionGlassesViewModel fpvm = new FeaturedPrescriptionGlassesViewModel()
                    {
                        Id = item.ProductId.ToString(),
                        Name = item.Name,
                        Image = item.PrimaryImage,
                        Price = decimal.Round(item.Price).ToString()
                    };
                    lstPrescription.Add(fpvm);
                }
                model.PopPrescriptionGlasses = lstPrescription;
                return View(model);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Home", "Index");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpGet]
        public ActionResult ViewProduct(int id, string urlRedirect)
        {
            try
            {
                ViewBag.Message = urlRedirect;
                ViewProductViewModel model = new ViewProductViewModel();
                ViewBag.Message = urlRedirect;
                if (Product.IsPrescriptionGlasses(id))
                {
                    PrescriptionGlasses p = new PrescriptionGlasses(id);
                    model.Id = p.ProductId.ToString();
                    model.Name = p.Name;
                    model.Price = decimal.Round(p.Price).ToString();
                    model.PrimaryImage = p.PrimaryImage;
                    model.Lens = p.Lens.LensName;
                    model.Description = p.ProductDescription;
                    model.DiscountedPrice = decimal.Round(p.GetDiscountedPrice()).ToString();
                    if (p.Quantity == 0)
                    {
                        model.Status = "Out of Stock";
                    }
                    else
                    {
                        model.Status = p.Quantity + " Item(s) available";
                    }
                    model.Frame = p.Frame.FrameName;
                    model.FrameColor = p.FrameColor;
                    model.Images = p.Images;
                    model.IsSunglasses = false;
                }
                else if (Product.IsSunglasses(id))
                {
                    Sunglasses s = new Sunglasses(id);
                    model.Id = s.ProductId.ToString();
                    model.Name = s.Name;
                    model.Price = decimal.Round(s.Price).ToString();
                    model.PrimaryImage = s.PrimaryImage;
                    model.LensColor = s.LensColor;
                    model.Description = s.ProductDescription;
                    model.DiscountedPrice = decimal.Round(s.GetDiscountedPrice()).ToString();
                    if (s.Quantity == 0)
                    {
                        model.Status = "Out of Stock";
                    }
                    else
                    {
                        model.Status = s.Quantity + " Item(s) remaining";
                    }
                    model.FrameColor = s.FrameColor;
                    model.Images = s.Images;
                    model.IsSunglasses = true;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Home", "ViewProduct");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        public ActionResult ViewStore()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Home", "ViewStore");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
    }
}