using System.Web.Mvc;

namespace NawazEyeWebProject_NEW_.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(HandleErrorInfo model)
        {
            return View( model);
        }
    }
}