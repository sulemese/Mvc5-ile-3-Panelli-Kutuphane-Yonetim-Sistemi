using MVCLIBRARY.Models.Entity; //database işlemleri için gerekli bir kitaplık. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //FormsAuthantication işlemleri için gerekli bir kitaplık.

namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        // GET: AdminLogin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TBL_ADMİN p)
        {
            var değerler = db.TBL_ADMİN.FirstOrDefault(x => x.USERNAME == p.USERNAME && x.PASSWORD == p.PASSWORD);
            if(değerler!=null)
            {
                FormsAuthentication.SetAuthCookie(değerler.USERNAME, false);
                Session["USERNAME"] = değerler.USERNAME.ToString();
                return RedirectToAction("Index","Category");
            }
            else
            {
                return View();
            }
           
        }
    }
}