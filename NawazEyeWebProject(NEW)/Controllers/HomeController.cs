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
            return View();
        }
        public ActionResult CartIcon()
        {
            int i = 1;
            return PartialView("_CartIcon", i);
        }
    }
}