using NawazEyeWebProject_NEW_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawazEyeWebProject_NEW_.Controllers
{
    public class ViewController : Controller
    {
        // GET: View
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewProduct(int id)
        {
            ViewProductViewModel model= new ViewProductViewModel();
            return View(model);
        }
    }
}