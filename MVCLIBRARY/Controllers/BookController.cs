using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{
    public class BookController : Controller
    {
        /*------------------------------------------------------------------------*/
        
        LIBRARYDBEntities db = new LIBRARYDBEntities();

        /*------------------------------------------------------------------------*/


        //KİTAP LİSTELEME FONKSİYONU
        /* 
         * ARAMA İŞLEMİ İÇİN DIŞARIDAN P isminde STRİNG PARAMETRESİ GÖNDERDİM.
         * ARAMAK İSTEDİĞİM ŞEY KİTABIN İSMİ.
         * KİTAPLAR İSMİNDE BİR DEĞİŞKEN OLUŞTURDUM.
         * KİTAPLAR DEĞİŞKENİMİN DEĞERİNİ TBL_BOOK İÇİNDEN ALIP SEÇİYORUM.
         * ARAMA KISMINA YAZIP ARATTIĞIM P NULL YADA BOŞ DEĞİLSE
         * P NİN İÇERDİĞİ DEĞERE GÖRE KİTABI BUL.
         */

        public ActionResult Index(string p,int sayfa=1) 
        {
            var kitaplar = from k in db.TBL_BOOK select k;
            if(!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.NAME.Contains(p));
            }
            return View(db.TBL_BOOK.ToList().ToPagedList(sayfa, 3));               
        }

/*------------------------------------------------------------------------*/
        [HttpGet]
        public ActionResult BookAdd()
        {

            List<SelectListItem> deger1 = (from i in db.TBL_CATEGORY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;//değer1 den gelen değeri view a taşı. 
            //controllerdan view e değer taşır.


            List<SelectListItem> deger2 = (from i in db.TBL_AUTHOR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME + ' ' + i.SURNAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from i in db.TBL_PUBLISHER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            List<SelectListItem> deger4 = (from i in db.TBL_LANGUAGE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr4 = deger4;
            List<SelectListItem> deger5 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr5 = deger5;
            return View();
        }
/*------------------------------------------------------------------------*/
        [HttpPost]
        public ActionResult BookAdd(TBL_BOOK p)
        {
            var category = db.TBL_CATEGORY.Where(y => y.ID == p.TBL_CATEGORY.ID).FirstOrDefault();  
            p.TBL_CATEGORY = category;

            var author = db.TBL_AUTHOR.Where(y => y.ID == p.TBL_AUTHOR.ID).FirstOrDefault();
            p.TBL_AUTHOR = author; 

            var language = db.TBL_LANGUAGE.Where(y => y.ID == p.TBL_LANGUAGE.ID).FirstOrDefault();
            p.TBL_LANGUAGE = language;

            var library = db.TBL_LIBRARY.Where(y => y.ID == p.TBL_LIBRARY.ID).FirstOrDefault();
            p.TBL_LIBRARY = library;


            var publisher = db.TBL_PUBLISHER.Where(y => y.ID == p.TBL_PUBLISHER.ID).FirstOrDefault();
            p.TBL_PUBLISHER = publisher;

            db.TBL_BOOK.Add(p); //p den alıp tbl book tablosuna ekler.
            db.SaveChanges();
            return RedirectToAction("Index"); // ındex sayfasına yönlendirir.
            
        }

/*------------------------------------------------------------------------*/
        public ActionResult BookDelete(int ID)
        {
            var book = db.TBL_BOOK.Find(ID);
            db.TBL_BOOK.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
/*------------------------------------------------------------------------*/
        public ActionResult BookGet(int ID)  // güncelleme yapacağımız sayfada eski bilgileri textboxların içinde göstersin diye.
        {
            var book = db.TBL_BOOK.Find(ID);

            List<SelectListItem> deger1 = (from i in db.TBL_CATEGORY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;//değer1 den gelen değeri view a taşı.
            List<SelectListItem> deger2 = (from i in db.TBL_AUTHOR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME + ' ' + i.SURNAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from i in db.TBL_PUBLISHER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            List<SelectListItem> deger4 = (from i in db.TBL_LANGUAGE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr4 = deger4;
            List<SelectListItem> deger5 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr5 = deger5;
            return View("BookGet", book);
        }
/*------------------------------------------------------------------------*/
        public ActionResult BookUpdate(TBL_BOOK p)
        {
            var book = db.TBL_BOOK.Find(p.ID);
            book.NAME = p.NAME;
            book.PRINTYEAR = p.PRINTYEAR;
            book.PAGE = p.PAGE;
            
            
            var category = db.TBL_CATEGORY.Where(k => k.ID == p.TBL_CATEGORY.ID).FirstOrDefault();                                       //(p çekeceğimiz değerler  firstordefault() il veri yada olan veriyi çek.
            book.CATEGORYID = category.ID; 
            var author = db.TBL_AUTHOR.Where(y => y.ID == p.TBL_AUTHOR.ID).FirstOrDefault();
            book.AUTHORID = author.ID; 
            var language = db.TBL_LANGUAGE.Where(y => y.ID == p.TBL_LANGUAGE.ID).FirstOrDefault();
            book.LANGUAGEID = language.ID;
            var library = db.TBL_LIBRARY.Where(y => y.ID == p.TBL_LIBRARY.ID).FirstOrDefault();
            book.LIBRARYID = library.ID;
            var publisher = db.TBL_PUBLISHER.Where(y => y.ID == p.TBL_PUBLISHER.ID).FirstOrDefault();
            book.PUBLISHERID = publisher.ID;
            
            db.SaveChanges();
            return RedirectToAction("Index"); // ındex sayfasına yönlendirir.
/*------------------------------------------------------------------------*/
        }
    }
}