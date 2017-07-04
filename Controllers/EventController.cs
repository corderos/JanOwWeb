using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class EventController : Controller
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
            //var db = new Model1() ;
            List<Wydarzenie> EventList = new List<Wydarzenie>();
            foreach (Wydarzenie w in db.Wydarzenie)
            {
                EventList.Add(w);
            }

            return View(EventList);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Nazwa,Data,IloscMiejsc,Adres,IdProw,Cena,IdTem")] Wydarzenie wydarzenie)
        { var db = new Model1();
            if (ModelState.IsValid)
            {
                db.Wydarzenie.Add(wydarzenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wydarzenie);
        }

        public ActionResult Delete(int id)
        {
            var db = new Model1();
            Wydarzenie w = new Wydarzenie();
            foreach(Wydarzenie wyd in db.Wydarzenie)
            {
                if (wyd.Id == id) w = wyd;
            }
            db.Wydarzenie.Remove(w);
            db.SaveChanges();

            return RedirectToAction("index");
        }



        public ActionResult SignUp(int id)
        {

            int idK = Convert.ToInt32(Session["ID"]);
            var db = new Model1();
            Klient k = db.Klient.Find(idK);
            var update = db.Wydarzenie.Find(id);
            if (!k.Wydarzenie.Contains(update))
            {
                if (update != null && update.IloscMiejsc > 0)
                {
                    update.IloscMiejsc -= 1;
                    update.Klient.Add(k);
                    TryUpdateModel(update);
                    db.SaveChanges();
                }
            }
            else
            {
                TempData["message"] = "You're currently signed to that event";
            }
            return View();
        }
        public ActionResult Return()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Reports()
        {
            
            
            return View();
        }



        public ActionResult freeSpace()
        {
            var db = new Model1();
            List<String> tableX = new List<String>();
            List<int> tableY = new List<int>();
            var myChart = new Chart(width: 600, height: 400)
        .AddTitle("Ilość wolnych miejsc na wydarzeniach w miesiącu " + (DateTime.Now.Month + 1).ToString());
            foreach (Wydarzenie w in db.Wydarzenie)
            {
                if (w.Data.Year == DateTime.Now.Year && w.Data.Month == DateTime.Now.Month + 1)
                {

                    tableX.Add(w.Temat.ToString());
                    tableY.Add(w.IloscMiejsc);
                }
            }
            myChart.AddSeries(
                 name: "Employee",
            xValue: tableX,
            yValues: tableY);
            myChart.Write();

            return View("Reports");
        }


        public ActionResult freeTaken()
        {
            var db = new Model1();
            var db2 = new Model1();
            List<String> tableX = new List<String>();
            List<int> tableY = new List<int>();
            var myChart = new Chart(width: 600, height: 400)
        .AddTitle("Stosunek wolnych do zajętych miejsc w danym miesiącu");
            tableX.Add("Wolne");
            tableX.Add("Zajete");

            int free = 0;
            int taken = 0;
         
            foreach (Wydarzenie w in db.Wydarzenie)
            { 
                if (w.Data.Year == DateTime.Now.Year && w.Data.Month == DateTime.Now.Month + 1 )
                {

                    var Clients = (from Kli in db.Klient
                                   where Kli.Wydarzenie.Contains(w)
                                   select Kli);
                    
                    free += w.IloscMiejsc;
                    try {
                        taken++;
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                       
                    }
                    
                }
            }
            tableY.Add(free);
            tableY.Add(taken);
            myChart.AddSeries(
                 name: "Employee", chartType: "Pie",
            xValue: tableX,
            yValues: tableY);
            myChart.Write();

            return View("Reports");

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydarzenie wydarzenie = db.Wydarzenie.Find(id);
            if (Session["ID"] != null)
            {
                Klient klient = db.Klient.Find(Convert.ToInt32(Session["ID"]));

                if (klient.Wydarzenie.Contains(wydarzenie))
                {
                    ViewData["isSigned"] = true;
                }
                else
                {
                    ViewData["isSigned"] = false;
                }
            }

            if (wydarzenie == null)
            {
                return HttpNotFound();
            }
            return View(wydarzenie);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydarzenie wyd = db.Wydarzenie.Find(id);
            if (wyd == null)
            {
                return HttpNotFound();
            }
            return View(wyd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,Data,IloscMiejsc,Adres,Cena,Prowadzacy,Temat,Opis")] Wydarzenie wydarzenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wydarzenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wydarzenie);
        }

        public ActionResult Sign_Out(int id)
        {
            int idK = Convert.ToInt32(Session["ID"]);
            var db = new Model1();
            Klient k = db.Klient.Find(idK);
            Wydarzenie w = db.Wydarzenie.Find(id);
            w.Klient.Remove(k);
            w.IloscMiejsc++;
            TryUpdateModel(w);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Sign_SMB_Out(int id, int idWyd)
        {   
            Klient k = db.Klient.Find(id);
            Wydarzenie w = db.Wydarzenie.Find(idWyd);
            w.Klient.Remove(k);
            w.IloscMiejsc++;
            TryUpdateModel(w);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Browse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Wydarzenie wydarzenie = db.Wydarzenie.Find(id);

            if (wydarzenie == null)
            {
                return HttpNotFound();
            }
            List<Klient> klients = new List<Klient>();

            var clients = db.Klient.Where(l => l.Wydarzenie.Select(c => c.Id).Contains(wydarzenie.Id));
            ViewData["header"] = wydarzenie.Nazwa;
            ViewData["idWyd"] = wydarzenie.Id;
            return View(clients.ToList());
            }
    }
}