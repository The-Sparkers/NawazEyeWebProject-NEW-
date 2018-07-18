using NawazEyeWebProject_NEW_.Models;
using NawazEyeWebProject_NEW_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawazEyeWebProject_NEW_.Controllers
{
    public class FramesController : Controller
    {
        // GET: Frames
        public ActionResult Index()
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
    }
}