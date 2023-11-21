using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MVCDemoTask.Controllers
{
    public class AboutController : Controller
    {

        AboutManager abm = new AboutManager(new EfAboutDal());

        public ActionResult Index()
        {
            var aboutValues = abm.GetList();
            return View(aboutValues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            abm.AboutAdd(about);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAbout(int id)
        {
            var aboutValue = abm.GetById(id);
            abm.AboutDelete(aboutValue);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult EditAbout(int id)
        {
            var aboutValue = abm.GetById(id);
            return View(aboutValue);

        }

        [HttpPost]
        public ActionResult EditAbout(About about)
        {
            abm.AboutUpdate(about);
            return RedirectToAction("Index");
        }
    }
}