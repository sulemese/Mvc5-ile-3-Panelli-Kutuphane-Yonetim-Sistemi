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
    public class UserController : Controller
    {

        LIBRARYDBEntities db = new LIBRARYDBEntities();





        //BU METOD KATEGORİ TABLOSUNDAKİ DEĞERLERİ ALIR LİSTE HALİNE GETİRİR VE VİEW'A GÖNDEREREK LİSTELER. 
        public ActionResult Index(string p, int sayfa=1)
        {
            // veritabanındaki üyeler tablosundaki değerler kategoriler değişkenine  atanır. 
            var üyeler = from k in db.TBL_USER select k;
            //p değeri null yada boş değilse if koşulu içine girilir. 
            if (!string.IsNullOrEmpty(p))
            {
                //üyeler tablosunda NAME alanından p değerini içeren kayıtlar çekilir ve üyeler adlı değişkene atanır. 
                üyeler = üyeler.Where(m => m.NAME.Contains(p));
            }
            // üyeler değişkeninin içerdiği değerlerden STATUS alanı true olanları alınır ve değerler adlı değişkene atanır. 
            var degerler = üyeler.Where(m => m.STATUS == true).ToList();
            //"değerler" view katmanına taşınır 
            return View(degerler.ToPagedList(sayfa, 5));
        }





        [HttpGet]
        public ActionResult UserAdd()
        {
            return View();
        }







        [HttpPost]
        public ActionResult UserAdd(TBL_USER p)
        {
            if (!ModelState.IsValid)
            {
                return View("UserAdd");
            }

            db.TBL_USER.Add(p); //p den alıp tbl book tablosuna ekler.
            db.SaveChanges();
            return RedirectToAction("Index"); // ındex sayfasına yönlendirir.

        }






        //Üye silineceği zaman viewda/sayfada sil butonuna tıklandığında bu action resultdaki işlemler sırayla yapılır. 
        //Yani bu metod sil butonuna tıklandığında yapılacak işlemleri tanımlar. 
        public ActionResult UserDelete(int ID) //SİLİNECEK KAYDIN ID BİLGİSİ İÇİN BİR PARAMETRE ALIR. 
        {
            //VERİTABANINDAN ID PARAMETRESİ İLE EŞLEŞEN KAYITLAR BULUNUR. DEĞİŞKENE ATANIR.
            var user = db.TBL_USER.Find(ID);
            //BULUNAN KAYITLARIN "STATUS" ALANININ DEĞERİ FALSE OLARAK TANIMLANIR.
            user.STATUS = false;
            //VERİTABANI GÜNCELLENİR.
            db.SaveChanges();
            //Index sayfasına yönlendirecek.
            return RedirectToAction("Index");
        }





        //BU METOD SİLİNEN ÜYELERİ LİSTELER. SİLİNEN KATEGORİLER DURUMU FALSE OLAN KATEGORİLERDİR. BUNLARI LİSTE HALİNE GETİRİP VİEW KATMANINA TAŞIR.
        public ActionResult RemovedUserIndex()
        {
            //STATUS==FALSE olan kayıtları liste haline getir. değişkene ata. 
            var degerler = db.TBL_USER.Where(m => m.STATUS == false).ToList();
            //listeyi içeren değişkeni view katmanına gönder. 
            return View(degerler);
        }










        //BU METOD SİLİNEN ÜYELER SAYFASINDA GERİ YÜKLE BUTONUNA TIKLANAN KAYDI BULUR VE DURUMUNU TRUE OLARAK GÜNCELLEYEREK ÜYELER LİSTESİNE GERİ EKLER. 
        public ActionResult UserAddBack(int ID)
        {
            //İLGİLİ ID YE SAHİP KAYDI VERİTABANINDAN BULUR.
            var user = db.TBL_USER.Find(ID);
            //BU KAYDIN DURUMUNU TRUE OLARAK GÜNCELLER.
            user.STATUS = true;
            //DEĞİŞİKLİKLERİ KAYDEDER.
            db.SaveChanges();
            //EN SON INDEX ADLI VİEW'A (ÜYELER SAYFASI) DÖNER. 
            return RedirectToAction("Index");
        }









        // UserGet action result ımız id ye göre parametre getirecek.
        //find metodu ile id ye göre kategori bulunur ve ıser değişkenine atanır.
        // Geriye ÜyeGetir sayfasını ktg den gelen değer ile döndürecek. 
        public ActionResult UserGet(int ID)
        {
            var user = db.TBL_USER.Find(ID);
            return View("UserGet", user);
        }








        //GÜNCELLEME İŞLEMİNDE BİRDEN FAZLA GÜNCELLENECEK DEĞER OLDUĞU İÇİN 
        // ID PARAMETRESİ KULLANMADIM.
        public ActionResult UserUpdate(TBL_USER p)
        {

            var user = db.TBL_USER.Find(p.ID);
            user.NAME = p.NAME;
            user.SURNAME = p.SURNAME;
            user.MAIL = p.MAIL;
            user.TELEPHONENUM = p.TELEPHONENUM;
            user.SCHOOL = p.SCHOOL;
            user.PHOTOGRAPH = p.PHOTOGRAPH;
            user.USERNAME = p.USERNAME;
            user.PASSWORD = p.PASSWORD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }







        //Üyeler sayfasında "Kitap Geçmişi" butonuna tıklandığında yapılacak işlemleri içeren metoddur.
        public ActionResult UserBookDetail(int id) //ilgili üyenin id bilgisi alınır.
        {
            //üye id ile eşleşen kayıtlar hareket tablosundan çekilerek liste haline getirilir.
            var userbookdetail = db.TBL_DEPOSIT.Where(m => m.USERID == id).ToList();
            //kitap geçmişi sayfasına aktarılmak üzere id ile eşleşen üyenin ad ve soyad alanı değerleri veritabanından alınır. eğer birden fazla kayıt bulunursa kayıtların 1.si seçilir. 
            var usrname = db.TBL_USER.Where(m => m.ID == id).Select(m => m.NAME + " " + m.SURNAME).FirstOrDefault();
            //bu ad soyad değerleri view katmanına aktarılır.
            ViewBag.username = usrname;
            //kitap geçmişi kayıtlarından oluşan liste değişken ile view katmanına aktarılır.
            return View(userbookdetail);
        }

    }

}