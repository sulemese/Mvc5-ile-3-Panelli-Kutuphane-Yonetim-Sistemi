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
    public class EmployeeController : Controller
    {
        /*---------------------------------------------------------------------------------*/

        /***** VERİTABANI NESNESİ OLUŞTURDUK.********/
        LIBRARYDBEntities db = new LIBRARYDBEntities(); //veritabanını temsil etmek amacıyla  nesnesi oluşturduk.

        /*----------------------------------------------------------------------------------*/

        /***** ARAMA KUTUSUNA BİR P STRİNGİ YAZILDIYSA P İLE EŞLEŞEN KAYITLARI DÖNDÜR. NULL YADA EMPTY İSE TÜM KAYITLARI DÖNDÜR.********/
        public ActionResult Index(string p, int sayfa=1) 
        {
            var personeller = from k in db.TBL_EMPLOYEE select k;
            if (!string.IsNullOrEmpty(p))
            {
                personeller = personeller.Where(m => m.NAME.Contains(p));
            }
            return View(db.TBL_EMPLOYEE.ToList().ToPagedList(sayfa, 3));
        }

        /*----------------------------------------------------------------------------------*/

        [HttpGet]
        public ActionResult EmployeeAdd()
        {
            //var deger1 = db.TBL_LIBRARY.ToList();  eğer dropdownlist kullanmasaydık dbden bu şekilde değer çekecektik. 

            List<SelectListItem> deger1 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;//değer1 den gelen değeri view a taşı.

            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult EmployeeAdd(TBL_EMPLOYEE i)
        {
            if(!ModelState.IsValid)
            {
                return View("EmployeeAdd");
            }

            var library = db.TBL_LIBRARY.Where(y => y.ID == i.TBL_LIBRARY.ID).FirstOrDefault();
            i.TBL_LIBRARY = library;

            db.TBL_EMPLOYEE.Add(i);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EmployeeDelete(int id)
        {
            var personel = db.TBL_EMPLOYEE.Find(id);
            db.TBL_EMPLOYEE.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EmployeeGet(int id)
        {
            var prs = db.TBL_EMPLOYEE.Find(id);

            List<SelectListItem> deger1 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View("EmployeeGet", prs);
        }

        public ActionResult EmployeeUpdate(TBL_EMPLOYEE p)
        {
            var prs = db.TBL_EMPLOYEE.Find(p.ID);
            prs.NAME = p.NAME;
            prs.SURNAME = p.SURNAME;
            prs.MAIL = p.MAIL;
            prs.TELEPHONENUM = p.TELEPHONENUM;
            prs.ADDRESS = p.ADDRESS;

            var lbr = db.TBL_LIBRARY.Where(l => l.ID == p.TBL_LIBRARY.ID).FirstOrDefault();
            prs.LIBRARYID = lbr.ID;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}