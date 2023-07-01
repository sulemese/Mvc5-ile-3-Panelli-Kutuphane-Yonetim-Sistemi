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
    public class CategoryController : Controller
    {
       
       LIBRARYDBEntities db = new LIBRARYDBEntities();







        //BU METOD KATEGORİ TABLOSUNDAKİ DEĞERLERİ ALIR LİSTE HALİNE GETİRİR VE VİEW'A GÖNDEREREK LİSTELER. 
        public ActionResult Index(string p, int sayfa = 1)
        {
            // veritabanındaki kategori tablosundaki değerler kategoriler değişkenine  atanır. 
            var kategoriler = from k in db.TBL_CATEGORY select k;  

            //p değeri null yada boş değilse if koşulu içine girilir. 
            if (!string.IsNullOrEmpty(p))
            {
                //kategoriler tablosunda NAME alanından p değerini içeren kayıtlar çekilir ve kategoriler adlı değişkene atanır. 
                kategoriler = kategoriler.Where(m => m.NAME.Contains(p));
            }
            // kategoriler değişkeninin içerdiği değerlerden STATUS alanı true olanları alınır ve değerler adlı değişkene atanır. 
            var degerler = kategoriler.Where(m=>m.STATUS==true).ToList();  
            //"değerler" view katmanına taşınır 
            return View(degerler.ToPagedList(sayfa,5));
        }








        //BU METOD SİLİNEN KATEGORİLERİ LİSTELER. SİLİNEN KATEGORİLER DURUMU FALSE OLAN KATEGORİLERDİR. BUNLARI LİSTE HALİNE GETİRİP VİEW KATMANINA TAŞIR.
        public ActionResult RemovedCategoryIndex()
        {
            //STATUS==FALSE olan kayıtları liste haline getir. değişkene ata. 
            var degerler = db.TBL_CATEGORY.Where(m=>m.STATUS==false).ToList();
            //listeyi içeren değişkeni view katmanına gönder. 
            return View(degerler);
        }










        //BU METOD SİLİNEN KATEGORİLER SAYFASINDA GERİ YÜKLE BUTONUNA TIKLANAN KAYDI BULUR VE DURUMUNU TRUE OLARAK GÜNCELLEYEREK KATEGORİ LİSTESİNE GERİ EKLER. 
        public ActionResult CategoryAddBack(int ID)
        {
            //İLGİLİ ID YE SAHİP KAYDI VERİTABANINDAN BULUR.
            var ktg = db.TBL_CATEGORY.Find(ID);
            //BU KAYDIN DURUMUNU TRUE OLARAK GÜNCELLER.
            ktg.STATUS = true;
            //DEĞİŞİKLİKLERİ KAYDEDER.
            db.SaveChanges();
            //EN SON INDEX ADLI VİEW'A (KATEGORİLER SAYFASI) DÖNER. 
            return RedirectToAction("Index"); 
        }






       [HttpGet] 
        public ActionResult CategoryAdd()
        {
            return View();
        }



        [HttpPost]
        public ActionResult CategoryAdd(TBL_CATEGORY p)
        {
            db.TBL_CATEGORY.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }






      //Kategori silineceği zaman viewda/sayfada sil butonuna tıklandığında bu action resultdaki işlemler sırayla yapılır. 
      //Yani bu metod sil butonuna tıklandığında yapılacak işlemleri tanımlar. 
       public ActionResult CategoryDelete(int ID) //SİLİNECEK KAYDIN ID BİLGİSİ İÇİN BİR PARAMETRE ALIR. 
        {
            //VERİTABANINDAN ID PARAMETRESİ İLE EŞLEŞEN KAYITLAR BULUNUR. DEĞİŞKENE ATANIR.
            var category = db.TBL_CATEGORY.Find(ID);
            //BULUNAN KAYITLARIN "STATUS" ALANININ DEĞERİ FALSE OLARAK TANIMLANIR.
            category.STATUS = false;
            //VERİTABANI GÜNCELLENİR.
            db.SaveChanges();
            //Index sayfasına yönlendirecek.
            return RedirectToAction("Index"); 
        }








        // CategoryGet action result ımız id ye göre parametre getirecek.
        //find metodu ile id ye göre kategori bulunur ve ktg değişkenine atanır.
        // Geriye kategoriGetir sayfasını ktg den gelen değer ile döndürecek. 
        [HttpGet]
        public ActionResult CategoryGet(int ID)
        {
            var category = db.TBL_CATEGORY.Find(ID);
            return View("CategoryGet", category);
        }




/*------------------------------------------------------------------------*/
        //GÜNCELLEME İŞLEMİNDE BİRDEN FAZLA GÜNCELLENECEK DEĞER OLDUĞU İÇİN 
        // ID PARAMETRESİ KULLANMADIM.
      [HttpPost]
        public ActionResult CategoryUpdate(TBL_CATEGORY p)
        {

            var category = db.TBL_CATEGORY.Find(p.ID);
            category.NAME = p.NAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
/*------------------------------------------------------------------------*/




    }
}