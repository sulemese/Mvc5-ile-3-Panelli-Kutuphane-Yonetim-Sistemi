using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVCLIBRARY.Models.Entity; //ÖNCE KÜTÜPHANEYİ DAHİL ET!
namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous]
    public class AuthorController : Controller
    {

        LIBRARYDBEntities db = new LIBRARYDBEntities();

        //BU METOD YAZAR TABLOSUNDAKİ DEĞERLERİ ALIR LİSTE HALİNE GETİRİR VE VİEW'A GÖNDEREREK LİSTELER. 
        public ActionResult Index(string p,int sayfa=1)
        {
            // veritabanındaki yazarlar tablosundaki değerler yazarlar değişkenine  atanır. 
            var yazarlar = from k in db.TBL_AUTHOR select k;

            //p değeri null yada boş değilse if koşulu içine girilir. 
            if (!string.IsNullOrEmpty(p))
            {
                //yazarlar tablosunda NAME alanından p değerini içeren kayıtlar çekilir ve yazarlar adlı değişkene atanır.
                yazarlar = yazarlar.Where(m => m.NAME.Contains(p));
            }

            // yazarlar değişkeninin içerdiği değerlerden STATUS alanı true olanları alınır ve değerler adlı değişkene atanır. 
            var degerler = yazarlar.Where(m => m.STATUS == true).ToList();
            //"değerler" view katmanına taşınır 
            return View(degerler.ToPagedList(sayfa, 5));
        }







        [HttpGet]
        public ActionResult AuthorAdd()
        {
            return View();
        }






        //YENİ BİR YAZAR EKLENECEĞİ ZAMAN BU METOD ÇAĞIRILIR. VE BU METODDAKİ İŞLEMLER İLE YENİ YAZAR VERİTABANINA EKLENEREK VERİTABANI GÜNCELLENİR.
        [HttpPost]
        public ActionResult AuthorAdd(TBL_AUTHOR p)
        {
            // TBL_AUTHOR tablosuna p değerini ADD() metodu ile ekliyoruz. 
            db.TBL_AUTHOR.Add(p);
            // VERİTABANINI GÜNCELLİYORUZ.
            db.SaveChanges();
            //YAZARLARIN LİSTELENDİĞİ SAYFAYI ÇAĞIRIYORUZ. 
            return RedirectToAction("Index");
        }




        // yazar silineceği zaman viewda/sayfada sil butonuna tıklandığında bu metod çağırılır.
        // bu metod sil butonuna tıklandığında yapılacak işlemleri tanımlar. 
        public ActionResult AuthorDelete(int ID) //SİLİNECEK KAYDIN ID BİLGİSİ İÇİN BİR PARAMETRE ALIR. 
        {
            //VERİTABANINDAN ID PARAMETRESİ İLE EŞLEŞEN KAYIT BULUNUR. DEĞİŞKENE ATANIR.
            var author = db.TBL_AUTHOR.Find(ID);
            //BULUNAN KAYDIN "STATUS" ALANININ DEĞERİ FALSE OLARAK TANIMLANIR.
            author.STATUS = false;
            //VERİTABANI GÜNCELLENİR.
            db.SaveChanges();
            //Index SAYFASINA YÖNLENDİRİLİR.
            return RedirectToAction("Index");
        }




        //TABLODA BİR YAZAR GÜNCELLENECEĞİ ZAMAN ÖNCE GÜNCELLE BUTONUNA BASILAN SATIRIN ID BİLGİSİ METODA AKTARILIR.
        //BU METOD ID'Sİ BİLİNEN YAZARIN KAYDINI VERİTABANINDAN BULUR. 
        public ActionResult AuthorGet(int ID)
        {
            var author = db.TBL_AUTHOR.Find(ID);     //ID İLE EŞLEŞEN KAYIT VERİTABANINDAN BULUNUR.
            return View("AuthorGet", author);        // "author" değeri ile "AuthorGet" view/sayfasına git.
        }






        // Yazarın güncel bilgileri  metin kutularına yazıldıktan sonra bu bilgilerin veritabanına kaydedilmesini sağlayan metoddur.
        public ActionResult AuthorUpdate(TBL_AUTHOR p)
        {
            var author = db.TBL_AUTHOR.Find(p.ID); 
            author.NAME = p.NAME;
            author.SURNAME = p.SURNAME;
            author.DETAIL = p.DETAIL;
            db.SaveChanges();
            return RedirectToAction("Index");

        }









        //Yazarlar sayfasında "yazarın kitapları" butonuna tıklandığında bu metod çalışır
        //Bu metod yazarın kitaplarının sayfada listelenmesini sağlar. 
        public ActionResult AuthorBook(int id) //hangi yazara ait kitaplar listelenecekse o yazarın id si alınır. 
        {
            //veritabanında parametre olarak alınan yazar id ile eşleşen TBL_KİTAP tablosundaki kayıtlar alındı. 
            var degerler = db.TBL_BOOK.Where(m => m.AUTHORID == id).ToList();
            //veritabanında TBL_AUTHOR tablosundan id ile eşleşen kayıtların NAME ve SURNAME değerleri alındı. FirstorDefault ile birden fazla kayıt varsa ilkinin seçilmesi sağlandı.
            var yzrad = db.TBL_AUTHOR.Where(m => m.ID == id).Select(m => m.NAME +" "+ m.SURNAME).FirstOrDefault();
            //viewbag ile yazarın adını ve soyadını içeren değişken view katmanına taşındı. 
            ViewBag.yazarad = yzrad;
            // kitap kayıtlarını içeren değişken view katmanına aktarıldı. 
            return View(degerler); 
        }











        //BU METOD SİLİNEN YAZARLARI LİSTELER. SİLİNEN YAZARLARIN DURUMU FALSE OLUR. BUNLAR LİSTE HALİNE GETİRİLİR VE  VİEW KATMANINA TAŞINILIR.
        public ActionResult RemovedAuthorIndex()
        {
            //STATUS==FALSE olan kayıtları liste haline getir. değişkene ata. 
            var degerler = db.TBL_AUTHOR.Where(m => m.STATUS == false).ToList();
            //listeyi içeren değişkeni view katmanına gönder. 
            return View(degerler);
        }








        //BU METOD SİLİNEN YAZARLAR SAYFASINDA GERİ YÜKLE BUTONUNA TIKLANAN KAYDI BULUR VE DURUMUNU TRUE OLARAK GÜNCELLEYEREK YAZARLAR LİSTESİNE GERİ EKLER. 
        public ActionResult AuthorAddBack(int ID)
        {
            //İLGİLİ ID YE SAHİP KAYDI VERİTABANINDAN BULUR.
            var author = db.TBL_AUTHOR.Find(ID);
            //BU KAYDIN DURUMUNU TRUE OLARAK GÜNCELLER.
            author.STATUS = true;
            //DEĞİŞİKLİKLERİ KAYDEDER.
            db.SaveChanges();
            //EN SON INDEX ADLI VİEW'A (YAZARLAR SAYFASI) DÖNER. 
            return RedirectToAction("RemovedAuthorIndex");
        }

        
    }
}