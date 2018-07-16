using NawazEyeWebProject_NEW_.Models;
using NawazEyeWebProject_NEW_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawazEyeWebProject_NEW_.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
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
                //try
                //{
                lst.Add(hvm);
                //}
                //catch (NullReferenceException)
                //{

                //}
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
                //try
                //{
                lstSunglasses.Add(fsvm);
                //}
                //catch (NullReferenceException)
                //{
                //}
            }
            model.PopSunglasses = lstSunglasses;
            List<FeaturedPrescriptionGlassesViewModel> lstPrescription = new List<FeaturedPrescriptionGlassesViewModel>();
            foreach (var item in PrescriptionGlasses.FeaturedPrescriptionGlasses())
            {
                FeaturedPrescriptionGlassesViewModel fpvm = new FeaturedPrescriptionGlassesViewModel()
                {

                    Id = item.ProductId.ToString(),
                    Name = item.Name,
                    Image = item.Name,
                    Price = decimal.Round(item.Price).ToString()
                };
                lstPrescription.Add(fpvm);
                //try
                //{
                lstPrescription.Add(fpvm);
                //}
                //catch (NullReferenceException)
                //{
                //}
            }
            model.PopPrescriptionGlasses = lstPrescription;
            return View(model);
        }
    }
}