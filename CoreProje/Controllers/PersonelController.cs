using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreProje.Controllers
{
    public class PersonelController : Controller
    {
        Context context = new Context();

        [Authorize]
        public IActionResult Index()
        {
            var personel = context.Personels.Include(x=>x.Birim).ToList();

            return View(personel);
        }

        public IActionResult SIL(int id)
        {
            var personel = context.Personels.Find(id);
            context.Personels.Remove(personel);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult PersonelGetir()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PersonelGetir(int id)
        {
            //var personel = context.Personels.Find(id);

            //return View(personel);


            var personel = context.Personels.Find(id);
            List<SelectListItem> degerler = (from i in context.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.BirimAd,
                                                 Value = i.BirimID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("PersonelGetir",personel);
        }

        
        public IActionResult PersonelGuncelle(Personel p)
        {
            var personel = context.Personels.Find(p.PersonelID);
            personel.Ad = p.Ad;
            personel.Soyad = p.Soyad;
            personel.Sehir = p.Sehir;
            //urun.TBLKATEGORILER.KATEGORIAD = p1.TBLKATEGORILER.KATEGORIAD;
            var birim = context.Birims.Where(m => m.BirimID == p.Birim.BirimID).FirstOrDefault();
            personel.BirimID = birim.BirimID;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult YeniPersonel()
        {
            
            List<SelectListItem> personel = (from i in context.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.BirimAd,
                                                 Value = i.BirimID.ToString()
                                             }).ToList();
            ViewBag.dgr = personel;
            return View();

        }

        [HttpPost]
        public IActionResult YeniPersonel(Personel p)
        {
            var personel = context.Birims.Where(m => m.BirimID == p.Birim.BirimID).FirstOrDefault();
            p.Birim = personel;
            context.Personels.Add(p);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}