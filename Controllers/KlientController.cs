using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class KlientController : Controller
    {

        private Model1 db = new Model1();
        // GET: Klient
        public ActionResult Index()
        {
            using(Model1 db = new Model1())
            {
                return View(db.Klient.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Klient account)
        {
            if (ModelState.IsValid)
            {
                using (Model1 db = new Model1())
                {
                    db.Klient.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.Imie + " " + account.Nazwisko + " Successfully registered.";
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Klient klient)
        {
            using (Model1 db = new Model1())
            {
                var usr = db.Klient.Where(u => u.Login == klient.Login && u.Haslo == klient.Haslo).FirstOrDefault();
                if(usr != null)
                {
                    Session["ID"] = usr.Id.ToString();
                    Session["Login"] = usr.Login.ToString();
                    Session["AccType"] = usr.Typ_konta.ToString();
                             
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Login lub hasło są niepoprawne");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if(Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = db.Klient.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = db.Klient.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,Typ_konta,Login,Haslo")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klient);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = db.Klient.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        // POST: aaa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klient klient = db.Klient.Find(id);
            db.Klient.Remove(klient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Manage()
        {
            int idK = Convert.ToInt32(Session["ID"]);
            Klient klient = db.Klient.Find(idK);
            return View(klient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "ID,Imie,Nazwisko,Typ_Konta,Login,Haslo")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klient).State = EntityState.Modified;
                db.SaveChanges();
                Session["Login"] = klient.Login.ToString();
                return RedirectToAction("ManagedCorrectly");
            }
            return View(klient);
        }

        public ActionResult ManagedCorrectly()
        {
            return View();
        }
    }
}