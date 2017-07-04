using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Description()
        {
            return Content("JanOw jest prostym serwisem internetowym pozwalającym organizatorom na wypromowanie swoich wydarzeń.<br>Nie tylko można tu przeglądać dostępne wydarzenia, ale jako użytkownik, po zalogowaniu mamy możliwość deklaracji uczestnictwa w wydarzeniu. <br><b>Korzystaj! Prędko!</b><br>Ilość miejsc ograniczona.", "text/plain");
        }

        public ActionResult Newest()
        {
            var db = new Model1();
            List<Wydarzenie> EventList = new List<Wydarzenie>();
            foreach (Wydarzenie w in db.Wydarzenie)
            {
                EventList.Add(w);
            }
            Wydarzenie wyd = EventList.Last();

            return Json(wyd, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllEvents()
        {
            var db = new Model1();
            List<Wydarzenie> EventList = new List<Wydarzenie>();
            foreach (Wydarzenie w in db.Wydarzenie)
            {
                EventList.Add(w);
            }

            var viewModel = EventList.Select(x => new { Nazwa = x.Nazwa});

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventOfId(int id)
        {
            var db = new Model1();
            List<Wydarzenie> EventList = new List<Wydarzenie>();
            
            foreach (Wydarzenie w in db.Wydarzenie)
            {
                EventList.Add(w);
            }
            if(id > EventList.Count)
            {
                return Json(new { Nazwa = "Niestety nasz serwis nie posiada tylu wydarzeń" }, JsonRequestBehavior.AllowGet);
            }
            Wydarzenie wyd = EventList.ElementAt(id - 1);

            return Json(new { Nazwa = wyd.Nazwa, Temat = wyd.Temat, Opis = wyd.Opis }, JsonRequestBehavior.AllowGet);       
        }
    }
}