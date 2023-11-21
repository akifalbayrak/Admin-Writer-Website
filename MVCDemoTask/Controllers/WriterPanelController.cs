using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using PagedList.Mvc;

namespace MVCDemoTask.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel
        TitleManager tm = new TitleManager(new EfTitleDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        Context c = new Context();

        int writerIdInfo;
        [HttpGet]
        public ActionResult WriterProfile()
        {
            
            string p = (string)Session["WriterMail"];
            int id = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterId).FirstOrDefault();
            var writerValue = wm.GetById(id);
            return View(writerValue);
        }
        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllTitle","WriterPanel");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult MyTitle(string p)
        {
            p = (string)Session["WriterMail"];
            writerIdInfo = c.Writers.Where(x => x.WriterMail == p).Select(y=>y.WriterId).FirstOrDefault();
            var values = tm.GetListByWriter(writerIdInfo);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewTitle()
        {

            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewTitle(Title p)
        {
            string WriterMailInfo = (string)Session["WriterMail"];
            writerIdInfo = c.Writers.Where(x => x.WriterMail == WriterMailInfo).Select(y => y.WriterId).FirstOrDefault();

            p.TitleDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterId = writerIdInfo;
            p.TitleStatus = true;
            tm.TitleAdd(p);
            return RedirectToAction("MyTitle");
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
            ViewBag.vlc = valueCategory;
            var titleValue = tm.GetById(id);
            return View(titleValue);
        }
        [HttpPost]
        public ActionResult EditTitle(Title title)
        {
            tm.TitleUpdate(title);
            return RedirectToAction("MyTitle");
        }
        public ActionResult DeleteTitle(int id)
        {
            var titleValue = tm.GetById(id);
            titleValue.TitleStatus = false;
            tm.TitleDelete(titleValue);
            return RedirectToAction("MyTitle");
        }

        public ActionResult AllTitle(int page = 1)
        {
            var titles=tm.GetList().ToPagedList(page,4);
            return View(titles);
        }

        public ActionResult toDoList() 
        {
            return View();
        }
    }
}