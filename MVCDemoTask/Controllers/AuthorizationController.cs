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
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        AdminManager am = new AdminManager(new EfAdminDal());

        public ActionResult Index()
        {
            var adminValues = am.GetList();
            return View(adminValues);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            am.AdminAdd(p);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            //List<SelectListItem> valueAdmin = (from x in am.GetList()
            //                                      select new SelectListItem
            //                                      {
            //                                          Text = x.AdminRole,
            //                                          Value = x.AdminId.ToString()
            //                                      }).ToList();
            //ViewBag.vlc = valueAdmin;
            //var titleValue = am.GetById(id);
            //return View(titleValue);

            var adminValue = am.GetById(id);
            return View(adminValue);
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            am.AdminUpdate(p);
            return RedirectToAction("Index");
        }
    }
}