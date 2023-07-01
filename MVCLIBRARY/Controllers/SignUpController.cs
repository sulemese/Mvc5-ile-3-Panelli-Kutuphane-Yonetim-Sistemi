using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous] 
    public class SignUpController : Controller
    {
        
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        
        [HttpGet] //GET methodu veritabanindan data cekmek icin POST ise update etmek icin kullaniliyor.


        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(TBL_USER p)
        {
            if (!ModelState.IsValid)
            {
                return View("SignUp");
            }
            db.TBL_USER.Add(p);
            db.SaveChanges();
            return View();
        }


    }
}
