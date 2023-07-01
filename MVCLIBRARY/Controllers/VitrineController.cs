using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
using MVCLIBRARY.Models.Classes;
namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous]
    public class VitrineController : Controller
    {
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        
       [HttpGet]
        public ActionResult Index()
        { 
            Class1 cs = new Class1();
            cs.Deger1 = db.TBL_BOOK.ToList();
            cs.Deger2 = db.TBL_ABOUT.ToList();
            return View(cs);
        }

        [HttpPost]
        public ActionResult Index(TBL_CONTACT t)
        {
            db.TBL_CONTACT.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}