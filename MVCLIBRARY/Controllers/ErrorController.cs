using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Page400()
        {
            Response.StatusCode = 400; //bu sayfanın hata kodu 400
            Response.TrySkipIisCustomErrors = true; //hata sayfasının ekrana gelmesini sağlar.
            return View();
        }
        public ActionResult Page403()
        {
            Response.StatusCode = 403; //bu sayfanın hata kodu 400
            Response.TrySkipIisCustomErrors = true; //hata sayfasının ekrana gelmesini sağlar.
            return View();
        }
        public ActionResult Page404()
        {
            Response.StatusCode = 404; //bu sayfanın hata kodu 400
            Response.TrySkipIisCustomErrors = true; //hata sayfasının ekrana gelmesini sağlar.
            return View();
        }
    }
}