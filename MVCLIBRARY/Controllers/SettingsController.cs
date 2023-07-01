using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{
    public class SettingsController : Controller
    {

        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index()
        {
            var kullanıcılar = db.TBL_ADMİN.ToList(); //admin tablosundaki kayıtları listele
            return View(kullanıcılar); //listeyi sayfaya döndür.
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(TBL_ADMİN p)
        {
            db.TBL_ADMİN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAdmin(int id)
        {
            var admin = db.TBL_ADMİN.Find(id);
            db.TBL_ADMİN.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //ADMİN BİLGİLERİNİ SAYFAYA ÇEKER
        [HttpGet]
        public ActionResult UpdateAdmin(int id)
        {
            var admin = db.TBL_ADMİN.Find(id);
            return View("UpdateAdmin",admin);
        }


        //FORMDA YAPILAN DEĞİŞİKLİKLERİ VT YE KAYDEDER. 
        [HttpPost]
        public ActionResult UpdateAdmin(TBL_ADMİN p)
        {
            var admin = db.TBL_ADMİN.Find(p.ID);
            admin.USERNAME  = p.USERNAME;
            admin.PASSWORD  = p.PASSWORD;
            admin.AUTHORITY = p.AUTHORITY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}