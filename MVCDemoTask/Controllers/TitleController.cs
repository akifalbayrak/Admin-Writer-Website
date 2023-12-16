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
    public class TitleController : Controller
    {
        // GET: Title
        TitleManager tm = new TitleManager(new EfTitleDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var titleValues = tm.GetList();
            return View(titleValues);
        }

        public ActionResult TitleReport()
        {
            var titleValues = tm.GetList();
            return View(titleValues);
        }

        public CategoryManager GetCm()
        {
            return cm;
        }

        [HttpGet]
        public ActionResult AddTitle()
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            List<SelectListItem> valueWriter = (from x in wm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " " + x.WriterSurname,
                                                    Value = x.WriterId.ToString()
                                                }).ToList();
            ViewBag.vlc = valueCategory;
            ViewBag.vlw = valueWriter;
            return View();
        }
        [HttpPost]
        public ActionResult AddTitle(Title p)
        {

            p.TitleDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            tm.TitleAdd(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditTitle(int id)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            List<SelectListItem> valueTitleStatus = new List<SelectListItem>
            {
                new SelectListItem { Text = "True", Value = "true" },
                new SelectListItem { Text = "False", Value = "false" }
            };


            ViewBag.vlc = valueCategory;
            ViewBag.vld = valueTitleStatus;
            var titleValue = tm.GetById(id);
            return View(titleValue);
        }
        [HttpPost]
        public ActionResult EditTitle(Title title)
        {
            title.TitleDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            tm.TitleUpdate(title);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTitle(int id)
        {
            var titleValue=tm.GetById(id);
            titleValue.TitleStatus = false;
            tm.TitleDelete(titleValue);
            return RedirectToAction("Index");
        }
        public ActionResult ActiveTitle(int id)
        {
            var titleValue = tm.GetById(id);
            titleValue.TitleStatus = true;
            tm.TitleDelete(titleValue);
            return RedirectToAction("Index");
        }
        public ActionResult TitleByWriter(int id)
        {
            var contentValues = tm.GetListByWriter(id);
            return View(contentValues);
        }
    }
}