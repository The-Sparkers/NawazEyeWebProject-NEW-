using NawazEyeWebProject_NEW_.Models;
using NawazEyeWebProject_NEW_.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NawazEyeWebProject_NEW_.Controllers
{
    public class FramesController : Controller
    {
        // GET: Frames
        public ActionResult Index()
        {
            try
            {
                List<ViewFramesViewModel> model = new List<ViewFramesViewModel>();
                foreach (var item in PrescriptionGlasses.GetAllPrescriptionGlasses())
                {
                    model.Add(new ViewFramesViewModel()
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
                HandleErrorInfo error = new HandleErrorInfo(ex, "Frames", "Index");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
    }
}