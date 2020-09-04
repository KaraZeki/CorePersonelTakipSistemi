using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreProje.Controllers
{
    public class BirimController : Controller
    {
        Context context = new Context();

        [Authorize]
        public IActionResult Index()
        {
            var birim = context.Birims.ToList();
            return View(birim);
        }


        public IActionResult SIL(int id)
        {
            var birim = context.Birims.Find(id);
            context.Birims.Remove(birim);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult BirimGetir()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BirimGetir(int id)
        {
            var birim = context.Birims.Find(id);
            return View(birim);

        }

        public IActionResult BirimGuncelle(Birim b)
        {
            var birim = context.Birims.Find(b.BirimID);

            birim.BirimAd = b.BirimAd;
            context.SaveChanges();
            return RedirectToAction("Index");

        }


        [Authorize]
        public IActionResult BirimEkle()
        {

            return View();

        }
        
        [HttpPost]
        public IActionResult BirimEkle(Birim b)
        {
            context.Birims.Add(b);
            context.SaveChanges();
            return View();

        }

        public IActionResult BirimDetay(int id)
        {
            var degerler = context.Personels.Where(m => m.BirimID == id).ToList();

            var brmAd = context.Birims.Where(m => m.BirimID == id).Select(y => y.BirimAd).FirstOrDefault();
            ViewBag.brmAd=brmAd;
            return View(degerler);
        }

    }
}