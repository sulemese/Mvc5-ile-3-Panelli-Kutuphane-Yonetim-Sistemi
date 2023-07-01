using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
namespace MVCLIBRARY.Controllers

{
    
    public class PunishmentController : Controller
    {
     LIBRARYDBEntities db = new LIBRARYDBEntities(); 
        public ActionResult Index()
        {
            var degerler = db.TBL_PUNISHMENT.Where(x => x.PRICE>0).ToList();
            return View(degerler);
        }
        public ActionResult CezaOde(int ID)
        {
            var category = db.TBL_PUNISHMENT.Find(ID);
            return View("CezaOde", category);
        }
        public ActionResult PunishmentUpdate(TBL_PUNISHMENT p)
        {
            var category = db.TBL_PUNISHMENT.Find(p.ID);
            category.PRICE = 0;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }


}