using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{ 
    
    public class StatisticController : Controller
    {
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        // GET: Statistic
        public ActionResult Index()
        {
            var deger1 = db.TBL_USER.Count();  //bootstrap kartlarında toplam üye sayısını gösterecek.
            ViewBag.dgr1 = deger1;
            var deger2 = db.TBL_BOOK.Count();  //toplam kitap sayısını gösterecek.   
            ViewBag.dgr2 = deger2;
            var deger3 = db.TBL_BOOK.Where(x=>x.STATUS == false).Count(); //emanet kitap sayısını gösterecek.   
            ViewBag.dgr3 = deger3;
            var deger4 = db.TBL_PUNISHMENT.Where(x=>x.PRICE>0).Sum(x=>x.PRICE);  //kasadaki tutarı gösterecek.   
            ViewBag.dgr4 = deger4;
            return View();


        }




        public ActionResult Hava()
        {
            return View();
        }
        public ActionResult HavaKart()
        {
            return View();
        }

        public ActionResult Galeri()
        {
            return View();
        }

        public ActionResult LinqKart()
        {
            var deger1 = db.TBL_BOOK.Count(); //toplam kitap sayısı kartı için
            ViewBag.dgr1 = deger1;
            var deger2 = db.TBL_USER.Count();//üye sayısı
            ViewBag.dgr2 = deger2;
            var deger3 = db.TBL_PUNISHMENT.Sum(x=> x.PRICE);//kasadaki tutar
            ViewBag.dgr3 = deger3;
            var deger4 = db.TBL_BOOK.Where(x=>x.STATUS==false).Count(); //emanet kitaplar
            ViewBag.dgr4 = deger4;
            var deger5 = db.TBL_CATEGORY.Count(); // KATEGORİ SAYISINI
            ViewBag.dgr5 = deger5;
            var deger6 = db.ENAKTIFUYE().FirstOrDefault();
            ViewBag.dgr6 = deger6;
            var deger7 = db.ENCOKOKUNANKITAP().FirstOrDefault();
            ViewBag.dgr7 = deger7;
            var deger8 = db.ENFAZLAKITAPYAZAR().FirstOrDefault(); // EN FAZLA KİTABI OLAN YAZARIN ADI SOYADINI
            ViewBag.dgr8 = deger8;
            var deger9 = db.EN_IYI_YAYINEVI().FirstOrDefault();
            ViewBag.dgr9 = deger9;
            //var deger10 = db.ENBASARILIPERSONEL();
            //ViewBag.dgr10 = deger10;
            var deger11 = db.TBL_CONTACT.Count();
            ViewBag.dgr11 = deger11;
            var deger12 = db.BUGUNKU_EMANETLER().FirstOrDefault();
            ViewBag.dgr12 = deger12;






            return View();
        }
        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if(dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
                //resimlerin içine kaydetsin
            }
            return RedirectToAction("Galeri");
        }
    }

}