using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
namespace MVCLIBRARY.Controllers
{
    public class DepositController : Controller
    {



        LIBRARYDBEntities db = new LIBRARYDBEntities();

       // [Authorize(Roles="1.Seviye")]   Rolü "1.seviye" olanlar bu işleme erişim sağlasın. onun dışındakiler giriş ekranına yönlendirilsin.
        public ActionResult Index()
        {
            var degerler = db.TBL_DEPOSIT.Where(x => x.TRANSACTIONSTATUS == false).ToList();
            return View(degerler);
        }





        [HttpGet]
        public ActionResult DepositGive()
        {
            //üye adı metin kutusundan yazılırsa sorun olabilir. dropdown list ile seçilmesi daha uygundur. 
            //Bir liste oluştur . adı değer1 olsun. bu değer1 üyeler tablosundan çekilip seçilen liste öğesi olsun. bu elemanın değeri id , text değeri name olsun. 
            //id int olduğu için tostring ile string e dönüşsün.
            //bu eleman tolist metodu ile liste haline dönüşsün. 

 
            //üye adı seçimi için                                  
            List<SelectListItem> deger1 = (from x in db.TBL_USER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.NAME + " " + x.SURNAME,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            //kitap adı seçimi için
            List<SelectListItem> deger2 = (from x in db.TBL_BOOK.Where(x=>x.STATUS == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.NAME,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2= deger2;


            //çalışan adı seçimi için
            List<SelectListItem> deger3 = (from x in db.TBL_EMPLOYEE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.NAME + " " + x.SURNAME,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            //kütüphane adı seçimi için 
            List<SelectListItem> deger4 = (from x in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.NAME,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.dgr4 = deger4;

            return View();
        }

        /*------------------------------------------------------------------------*/
        [HttpPost]
        public ActionResult DepositGive(TBL_DEPOSIT p)
        {
            //dropdownlist ile seçilen değerlerin controller tarafında sql tablolarına kaydedilmesi için gerekli kodlar...
            var d1 = db.TBL_USER.Where(x => x.ID == p.TBL_USER.ID).FirstOrDefault();
            var d2 = db.TBL_BOOK.Where(x => x.ID == p.TBL_BOOK.ID).FirstOrDefault();
            var d3 = db.TBL_EMPLOYEE.Where(x => x.ID == p.TBL_EMPLOYEE.ID).FirstOrDefault();
            var d4 = db.TBL_LIBRARY.Where(x => x.ID == p.TBL_LIBRARY.ID).FirstOrDefault();

            //navigation properities e atamalarını yaptım.
            p.TBL_USER = d1;
            p.TBL_BOOK = d2;
            p.TBL_EMPLOYEE = d3;
            p.TBL_LIBRARY = d4;
            db.TBL_DEPOSIT.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------------*/
        //ödünç geri al butonuna tıkladığında geç getirilen gün sayısını da hesaplaması gerekmekte.
        public ActionResult DepositBack(TBL_DEPOSIT p)
        {
            var deposit = db.TBL_DEPOSIT.Find(p.ID); //ilgili hareketin ID sinden kaydı çek deposit e ata.
            DateTime d1 = DateTime.Parse(deposit.ENDTIME.ToString()); //normal iade tarihi
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString()); //getirdiği tarih
            TimeSpan d3 = d2-d1; //ikisi arasındaki farkı al 
            ViewBag.dgr = d3.TotalDays; //bu farkı totaldays formatında göster. 

            return View("DepositBack", deposit);    
        }
        /*------------------------------------------------------------------------*/
        public ActionResult DepositUpdate(TBL_DEPOSIT p)
        {
            //bu işlem yetersiz kalır. bu nedenle güncelleme için bir trigger yazıldı.
            //hareket tablosunda güncelleme olduğu zaman kitap tablosunda da bir güncelleme olması düşünüldü.
            var deposit = db.TBL_DEPOSIT.Find(p.ID); //id yi bul
            deposit.USERGETTIME = p.USERGETTIME;     //üye getirme tarihini atama yap.
            deposit.TRANSACTIONSTATUS = true;        // durumu true ya ayarla.
            db.SaveChanges();                        //değişiklikleri kaydet.
            return RedirectToAction("Index");        //ındex action result ına  git.  
        }
        /*------------------------------------------------------------------------*/
    }
}
