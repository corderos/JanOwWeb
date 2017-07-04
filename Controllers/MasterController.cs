using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListOfEvents()
        {
            EventController ev = new EventController();
            ev.Index();
            return RedirectToAction("Index","EventController");

        }
    }
}