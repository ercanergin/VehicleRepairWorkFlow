using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TamirhaneMVC.Controllers
{
    public class HomeController : Controller
    {
        TamirhaneDBEntities _context = new TamirhaneDBEntities();
        public ActionResult Index()
        {
            var listofData = _context.MusteriAracs.ToList();
            return View(listofData);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MusteriArac model)
        {
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[MusteriArac] ON");

            _context.MusteriAracs.Add(model);
            _context.SaveChanges();

            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[MusteriArac] OFF");

            using (var transaction = _context.Database.BeginTransaction())
                transaction.Commit();

            ViewBag.Message = "Kayıt Başarılı Bir Şekilde Eklendi!";

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.MusteriAracs.Where(x => x.MusteriID == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(MusteriArac Model)
        {
            var data = _context.MusteriAracs.Where(x => x.MusteriID == Model.MusteriID).FirstOrDefault();
            if (data != null)
            {
                data.MusteriID = Model.MusteriID;
                data.MusteriAd = Model.MusteriAd;
                data.MusteriSoyad = Model.MusteriSoyad;
                data.MusteriTel = Model.MusteriTel;
                data.AracMarka = Model.AracMarka;
                data.AracModel = Model.AracModel;
                data.AracPlaka = Model.AracPlaka;
                data.AracGirisTarih = Model.AracGirisTarih;
                data.AracBitisTarih = Model.AracBitisTarih;
                data.DegisenParca = Model.DegisenParca;
                data.ToplamUcret = Model.ToplamUcret;
                data.Aciklama = Model.Aciklama;
                _context.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public ActionResult Detail(int id)
        {
            var data = _context.MusteriAracs.Where(x => x.MusteriID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var data = _context.MusteriAracs.Where(x => x.MusteriID == id).FirstOrDefault();
            _context.MusteriAracs.Remove(data);
            _context.SaveChanges();
            ViewBag.Messsage = "Kayıt Başarılı Bir Şekilde Silindi!";
            return RedirectToAction("index");
        }
    }
}