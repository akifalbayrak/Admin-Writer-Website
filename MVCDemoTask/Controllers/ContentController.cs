using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer;

namespace MVCDemoTask.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ContentGetAll(string p) 
        {
            var values = cm.GetList(p);
            if (p == null)
            {
                return View(cm.GetList(""));
            }
            return View(values);
        }

        public ActionResult ContentByTitle(int id)
        {
            var contentValues = cm.GetListByTitleId(id);
            return View(contentValues);
        }
    }
}