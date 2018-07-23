using NawazEyeWebProject_NEW_.Models;
using NawazEyeWebProject_NEW_.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NawazEyeWebProject_NEW_.Controllers
{
    public class SunglassesController : Controller
    {
        // GET: Sunglasses
        public ActionResult Index()
        {
            try
            {
                List<ViewSunglassesViewModel> model = new List<ViewSunglassesViewModel>();
                foreach (var item in Sunglasses.GetAllSunglasses())
                {
                    model.Add(new ViewSunglassesViewModel()
                    {
                        Id = item.ProductId.ToString(),
                        Name = item.Name,
                        Image = item.PrimaryImage,
                        Price = decimal.Round(item.Price).ToString(),
                        Discount = item.Discount.ToString()
                    });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Sunglasses", "Index");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
    }
}