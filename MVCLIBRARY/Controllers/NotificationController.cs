using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{
    public class NotificationController : Controller
    {
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index()
        {
            var degerler = db.TBL_NOTIFICATION.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TBL_NOTIFICATION duyuru)
        {
            db.TBL_NOTIFICATION.Add(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBL_NOTIFICATION.Find(id);
            db.TBL_NOTIFICATION.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DuyuruDetay(TBL_NOTIFICATION p)
        {
            var duyuru = db.TBL_NOTIFICATION.Find(p.ID);
            return View("DuyuruDetay", duyuru);
        }
        [HttpPost]
        public ActionResult DuyuruGüncelle(TBL_NOTIFICATION p)
        {
            var duyuru = db.TBL_NOTIFICATION.Find(p.ID);
            duyuru.CATEGORY = p.CATEGORY;
            duyuru.CONTENT = p.CONTENT;
            duyuru.DATE = p.DATE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}