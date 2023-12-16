using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MVCDemoTask.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent
        TitleManager tm = new TitleManager(new EfTitleDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        Context c = new Context();
        public ActionResult MyContent(String p)
        {
            p = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterId).FirstOrDefault();
            var contentValues = cm.GetListByWriterId(writerIdInfo);
            return View(contentValues);
        }
        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            string mail = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterId).FirstOrDefault();
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterId = writerIdInfo;
            p.ContentStatus = true;
            cm.ContentAdd(p);
            return RedirectToAction("MyContent");
        }

        public ActionResult ContentByWriter(int id)
        {
            var contentValues = tm.GetListByWriter(id);
            return View(contentValues);
        }
    }
}